using System;
using System.Collections.Generic;
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
    class MainWindowViewModel:ViewModelBase
    {
        #region Variables
        private readonly Configuration _config;
        private DbMigrator _migrator;
        private Thread _threadDbConnection;

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                RaisePropertyChanged(() => ConnectionString);
            }
        }

        private bool _btnIsDisabled;
        public bool BtnIsDisabled
        {
            get { return _btnIsDisabled; }
            set
            {
                _btnIsDisabled = value;
                RaisePropertyChanged("BtnIsDisabled");
            }
        }

        private Visibility _progressBarVisible;
        public Visibility ProgressBarVisible
        {
            get { return _progressBarVisible; }
            set
            {
                _progressBarVisible = value;
                RaisePropertyChanged("ProgressBarVisible");
            }
        }

        private Migration _selectedMigration;
        public Migration SelectedMigration
        {
            get { return _selectedMigration; }
            set
            {
                _selectedMigration = value;
                RaisePropertyChanged(() => SelectedMigration);
            }
        }

        private string _resultUpdate;
        public string ResultUpdate
        {
            get { return _resultUpdate; }
            set
            {
                _resultUpdate = value;
                RaisePropertyChanged(() => ResultUpdate);
            }
        }

        private List<Migration> _list;
        public List<Migration> List
        {
            get { return _list; }
            set
            {
                _list = value;
                RaisePropertyChanged(() => List);
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
            ProgressBarVisible = Visibility.Hidden;
            BtnIsDisabled = true;
        }

        private void UpdateDb(object newVersionName)
        {
            ProgressBarVisible = Visibility.Visible;
            BtnIsDisabled = false;
            try
            {
                if (
                    MessageBox.Show("Вы действительно желаете обновить БД до версии: " + newVersionName, "Обновление БД",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _migrator.Update((string) newVersionName);
                    ResultUpdate = "Обновление до версии: " + newVersionName + " успешно завершено";
                    UpdateGrid();
                }
            }
            catch (Exception error)
            {
                ResultUpdate = "При обновлении возникла ошибка: " + error;
            }
            finally
            {
                ProgressBarVisible = Visibility.Hidden;
                BtnIsDisabled = true;
                _threadDbConnection.Abort();
            }
        }

        private void ConnectToDb()
        {
            ProgressBarVisible = Visibility.Visible;
            BtnIsDisabled = false;        
            try
            {
                //Проверка валидности ConnectionString
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                _config.TargetDatabase = new DbConnectionInfo(ConnectionString, "System.Data.SqlClient");
                _migrator = new DbMigrator(_config);
                UpdateGrid();
                ResultUpdate = "";
            }
            catch (Exception exception)
            {
                List = new List<Migration>();
                ResultUpdate = "При подключении к БД возникла ошибка: " + exception.Message;
            }
            finally
            {
                ProgressBarVisible = Visibility.Hidden;
                BtnIsDisabled = true;
                _threadDbConnection.Abort();
            }

        }

        private void UpdateGrid()
        {
            var list = new List<Migration>();
            var listLocalMigrations = _migrator.GetLocalMigrations().ToList();
            for (var i = _migrator.GetLocalMigrations().Count() - 1; i >= 0; i--)
            {
                var isCheck = _migrator.GetDatabaseMigrations().Any(dbitem => dbitem == listLocalMigrations[i]);
                list.Add(new Migration() { Name = listLocalMigrations[i], Check = isCheck });
            }
            if (list.Count != 0)
            {
                list[0].Name = list[0].Name + " Последняя версия";
            }
            else
            {
                ResultUpdate = "Миграции отсутствуют";
            }
            List = list;
        }

        #region ConnectToDbCommand
        public RelayCommand ConnectToDbCommand { get; private set; }

        private void ConnectToDbBtn()
        {
            _threadDbConnection = new Thread(new ThreadStart(ConnectToDb));
            _threadDbConnection.Start();
        }
        #endregion

        #region UpdateDbToLastVersionCommand
        public RelayCommand UpdateDbToLastVersionCommand { get; private set; }

        private void UpdateDbToLastVersion()
        {
            _threadDbConnection = new Thread(new ThreadStart(ConnectToDb));
            _threadDbConnection = new Thread(new ParameterizedThreadStart(UpdateDb));
            _threadDbConnection.Start(_migrator.GetLocalMigrations().Last());
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
                    _threadDbConnection = new Thread(new ThreadStart(ConnectToDb));
                    _threadDbConnection = new Thread(new ParameterizedThreadStart(UpdateDb));
                    _threadDbConnection.Start(SelectedMigration.Name.Contains(" Последняя версия")
                        ? SelectedMigration.Name.Replace(" Последняя версия", "")
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
