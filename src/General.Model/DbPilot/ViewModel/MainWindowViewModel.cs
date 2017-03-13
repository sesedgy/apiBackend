using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Configuration = PoiskIT.Okenit2.General.Migrations.Configuration;

namespace GeneralModel.ViewModel
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private readonly Configuration _config;
        private DbMigrator _migrator;
        private const string LastVersion = " Последняя версия";

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
            
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                RaisePropertyChanged("ConnectionString");
            }
        }

        private bool _connectToDbBtnIsEnabled;
        public bool ConnectToDbBtnIsEnabled
        {
            get { return _connectToDbBtnIsEnabled; }
            set
            {
                _connectToDbBtnIsEnabled = value;
                RaisePropertyChanged("ConnectToDbBtnIsEnabled");
            }
        }

        private bool _updateDbToVersionBtnIsEnabled;
        public bool UpdateDbToVersionBtnIsEnabled
        {
            get { return _updateDbToVersionBtnIsEnabled; }
            set
            {
                _updateDbToVersionBtnIsEnabled = value;
                RaisePropertyChanged("UpdateDbToVersionBtnIsEnabled");
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

            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            ProgressBarVisibility = Visibility.Hidden;
            UpdateDbToVersionBtnIsEnabled = false;
            ConnectToDbBtnIsEnabled = true;
        }

        private void UpdateDb(string targetMigrationName)
        {
            new Task(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                UpdateDbToVersionBtnIsEnabled = false;
                ConnectToDbBtnIsEnabled = false;
                try
                {
                    if (MessageBox.Show("Вы действительно желаете обновить БД до версии: " + targetMigrationName,
                            "Обновление БД",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _migrator.Update(targetMigrationName);
                        UpdateGrid();
                        DbConnectAndDbUpdateStatus = "Обновление до версии: " + targetMigrationName +
                                                     " успешно завершено";
                    }
                }
                catch (Exception error)
                {
                    DbConnectAndDbUpdateStatus = "При обновлении возникла ошибка: " + error;
                }
                finally
                {
                    ProgressBarVisibility = Visibility.Hidden;
                    UpdateDbToVersionBtnIsEnabled = true;
                    ConnectToDbBtnIsEnabled = true;
                }
            }).Start();
        }

        private void ConnectToDbAndUpdateMigrationList()
        {

            new Task(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                UpdateDbToVersionBtnIsEnabled = false;            
                try
                {
                    _config.TargetDatabase = new DbConnectionInfo(ConnectionString, ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);
                    _migrator = new DbMigrator(_config);
                    UpdateGrid();
                    DbConnectAndDbUpdateStatus = "";
                    UpdateDbToVersionBtnIsEnabled = true;
                }
                catch (Exception exception)
                {
                    MigrationList = new List<Migration>();
                    DbConnectAndDbUpdateStatus = "При подключении к БД возникла ошибка: " + exception.Message;
                    UpdateDbToVersionBtnIsEnabled = false;
                }
                finally
                {
                    ProgressBarVisibility = Visibility.Hidden;
                }
            }).Start();
        }

        private void UpdateGrid()
        {
            var list = new List<Migration>();
            var dbMigrationsList = _migrator.GetDatabaseMigrations().ToList();
            var listLocalMigrations = _migrator.GetLocalMigrations().ToList();
            listLocalMigrations.Reverse();
            if(listLocalMigrations.Count == 0)
                DbConnectAndDbUpdateStatus = "Миграции отсутствуют";
            var isSuitableDb = dbMigrationsList.Count == 0;
            foreach (var item in listLocalMigrations)
            {
                var isChecked = dbMigrationsList.Any(dbitem => dbitem == item);
                if (isChecked)
                    isSuitableDb = true;               
                list.Add(new Migration() { Name = item, Check = isChecked });  
            }
            if (!isSuitableDb)
            {
                DbConnectAndDbUpdateStatus = "База данных имеет свой набор транзакций";
                return;
            }
            if (list.Count != 0)
                list[0].Name = list[0].Name + LastVersion;
            MigrationList = list;
        }

        #region connectToDbAndUpdateMigrationListCommand
        private ICommand _connectToDbAndUpdateMigrationListCommand;
        public ICommand ConnectToDbAndUpdateMigrationListCommand
        {
            get
            {
                if (_connectToDbAndUpdateMigrationListCommand == null)
                {
                    _connectToDbAndUpdateMigrationListCommand = new RelayCommand(ConnectToDbAndUpdateMigrationList);
                }
                return _connectToDbAndUpdateMigrationListCommand;
            }
        }
        #endregion

        #region updateDbToLastVersionCommand
        private ICommand _updateDbToLastVersionCommand;
        public ICommand UpdateDbToLastVersionCommand
        {
            get
            {
                if (_updateDbToLastVersionCommand == null)
                {
                    _updateDbToLastVersionCommand = new RelayCommand(() => UpdateDb(_migrator.GetLocalMigrations().Last()));
                }
                return _updateDbToLastVersionCommand;
            }
        }
        #endregion

        #region UpdateDbToVersionCommand
        private ICommand _updateDbToVersionCommand;
        public ICommand UpdateDbToVersionCommand
        {
            get
            {
                if (_updateDbToVersionCommand == null)
                {
                    _updateDbToVersionCommand = new RelayCommand(() => UpdateDb(SelectedMigration.Name.Contains(LastVersion)
                                ? SelectedMigration.Name.Replace(LastVersion, "")
                                : SelectedMigration.Name));
                }
                return _updateDbToVersionCommand;
            }
        }

        #endregion
    }
}
