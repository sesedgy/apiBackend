using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Configuration = PoiskIT.Okenit2.General.Migrations.Configuration;

namespace GeneralModel.ViewModel
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        #region Properties
        private readonly Configuration _config;
        private DbMigrator _migrator;
        private Thread _secondThread;
        private const string LastVersion = " Последняя версия";
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                RaisePropertyChanged("ConnectionStringTb");
            }
        }

        private string _providerName;
        public string ProviderName
        {
            get { return _providerName; }
            set
            {
                _providerName = value;
                RaisePropertyChanged("ProviderName");
            }
        }

        private bool _updateDbToLastVersionBtnIsDisabled;
        public bool UpdateDbToLastVersionBtnIsDisabled
        {
            get { return _updateDbToLastVersionBtnIsDisabled; }
            set
            {
                _updateDbToLastVersionBtnIsDisabled = value;
                RaisePropertyChanged("UpdateDbToLastVersionBtnIsDisabled");
            }
        }

        private Visibility _progressBarVisibility;
        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                RaisePropertyChanged("ProgressBarVisibility");
            }
        }

        //Выбранная строка с определенной миграцией
        private Migration _selectedMigration;
        public Migration SelectedMigration
        {
            get { return _selectedMigration; }
            set
            {
                _selectedMigration = value;
                RaisePropertyChanged("SelectedMigration");
            }
        }

        //Статус действий с БД
        private string _dbConnectAndDbUpdateStatus;
        public string DbConnectAndDbUpdateStatus
        {
            get { return _dbConnectAndDbUpdateStatus; }
            set
            {
                _dbConnectAndDbUpdateStatus = value;
                RaisePropertyChanged("DbConnectAndDbUpdateStatus");
            }
        }

        //Список доступных миграций
        private List<Migration> _migrationList;
        public List<Migration> MigrationList
        {
            get { return _migrationList; }
            set
            {
                _migrationList = value;
                RaisePropertyChanged("MigrationList");
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            _config = new Configuration();
            _migrator = null;

            ConnectToDbCommand = new RelayCommand(ConnectToDbBtn);
            UpdateDbToLastVersionCommand = new RelayCommand(UpdateDbToLastVersion);
            UpdateDbToVersionCommand = new RelayCommand(UpdateDbToVersion);

            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            ProviderName = ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;
            ProgressBarVisibility = Visibility.Hidden;
            UpdateDbToLastVersionBtnIsDisabled = true;
        }

        private void UpdateDb(object targetMigrationName)
        {
            ProgressBarVisibility = Visibility.Visible;
            UpdateDbToLastVersionBtnIsDisabled = false;
            try
            {
                if (
                    MessageBox.Show("Вы действительно желаете обновить БД до версии: " + targetMigrationName, "Обновление БД",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var updateDbTask = new Task(() => _migrator.Update((string) targetMigrationName));
                    updateDbTask.Start();
                    updateDbTask.Wait();                    
                    UpdateGrid();
                    DbConnectAndDbUpdateStatus = "Обновление до версии: " + targetMigrationName + " успешно завершено";
                }
            }
            catch (Exception error)
            {
                DbConnectAndDbUpdateStatus = "При обновлении возникла ошибка: " + error;
            }
            finally
            {
                ProgressBarVisibility = Visibility.Hidden;
                UpdateDbToLastVersionBtnIsDisabled = true;
            }
        }

        private void ConnectToDb()
        {
            ProgressBarVisibility = Visibility.Visible;
            UpdateDbToLastVersionBtnIsDisabled = false;        
            try
            {
                //Проверка валидности ConnectionString
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                _config.TargetDatabase = new DbConnectionInfo(ConnectionString, ProviderName);
                _migrator = new DbMigrator(_config);
                UpdateGrid();
                DbConnectAndDbUpdateStatus = "";
            }
            catch (Exception exception)
            {
                MigrationList = new List<Migration>();
                DbConnectAndDbUpdateStatus = "При подключении к БД возникла ошибка: " + exception.Message;
            }
            finally
            {
                ProgressBarVisibility = Visibility.Hidden;
                UpdateDbToLastVersionBtnIsDisabled = true;
            }

        }

        private void UpdateGrid()
        {
            var list = new List<Migration>();
            var dbMigrationsList = _migrator.GetDatabaseMigrations().ToList();
            var listLocalMigrations = _migrator.GetLocalMigrations().ToList();
            listLocalMigrations.Reverse();
            if(listLocalMigrations.Count == 0) { DbConnectAndDbUpdateStatus = "Миграции отсутствуют"; }
            var isSuitableDb = false;
            foreach (var item in listLocalMigrations)
            {
                var isCheck = dbMigrationsList.Any(dbitem => dbitem == item);
                if (isCheck || (dbMigrationsList.Count == 0)) { isSuitableDb = true; }
                list.Add(new Migration() { Name = item, Check = isCheck });  
            }
            if (!isSuitableDb)
            {
                DbConnectAndDbUpdateStatus = "База данных имеет свой набор транзакций";
                return;
            }
            if (list.Count != 0) { list[0].Name = list[0].Name + LastVersion; }
            MigrationList = list;
        }

        #region ConnectToDbCommand
        public RelayCommand ConnectToDbCommand { get; private set; }

        private void ConnectToDbBtn()
        {
            _secondThread = new Thread(new ThreadStart(ConnectToDb));
            _secondThread.Start();
        }
        #endregion

        #region UpdateDbToLastVersionCommand
        public RelayCommand UpdateDbToLastVersionCommand { get; private set; }

        private void UpdateDbToLastVersion()
        {
            _secondThread = new Thread(new ParameterizedThreadStart(UpdateDb));
            _secondThread.Start(_migrator.GetLocalMigrations().Last());
        }
        #endregion

        #region UpdateDbToVersionCommand
        public RelayCommand UpdateDbToVersionCommand { get; private set; }

        private void UpdateDbToVersion()
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedMigration.Name))
                {
                    _secondThread = new Thread(new ParameterizedThreadStart(UpdateDb));
                    _secondThread.Start(SelectedMigration.Name.Contains(LastVersion)
                        ? SelectedMigration.Name.Replace(LastVersion, "")
                        : SelectedMigration.Name);
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
