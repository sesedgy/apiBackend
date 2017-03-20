using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GeneralModel.Model;
using Microsoft.Win32;
using OfficeOpenXml;
using MessageBox = System.Windows.MessageBox;
using Npgsql;
using Configuration = PoiskIT.Okenit2.General.Migrations.Configuration;

namespace GeneralModel.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Configuration _config;
        private DbMigrator _migrator;
        private const string LastVersion = " Последняя версия";
        private List<Table> _tablesList = new List<Table>();

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
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

        private string _selectedFilePath;
        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set
            {
                _selectedFilePath = value;
                RaisePropertyChanged("SelectedFilePath");
            }
        }

        private bool _isTableChecked;
        public bool IsTableChecked
        {
            get { return _isTableChecked; }
            set
            {
                _isTableChecked = value;
                RaisePropertyChanged("IsTableChecked");
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

        private bool _buttonsOnTabsIsEnabled;
        public bool ButtonsOnTabsIsEnabled
        {
            get { return _buttonsOnTabsIsEnabled; }
            set
            {
                _buttonsOnTabsIsEnabled = value;
                RaisePropertyChanged("ButtonsOnTabsIsEnabled");
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

        private string _tablesUpdateStatus;
        public string TablesUpdateStatus
        {
            get { return _tablesUpdateStatus; }
            set
            {
                _tablesUpdateStatus = value;
                RaisePropertyChanged("TablesUpdateStatus");
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

        //Список таблиц доступных для обновления
        private List<Table> _tableList;
        public List<Table> TableList
        {
            get { return _tableList; }
            set
            {
                _tableList = value;
                RaisePropertyChanged("TableList");
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            _config = new Configuration();
            _migrator = null;
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            ProgressBarVisibility = Visibility.Hidden;
            ButtonsOnTabsIsEnabled = false;
            ConnectToDbBtnIsEnabled = true;
        }

        #region Migrations (First Tab)
        private void UpdateDb(string targetMigrationName)
        {
            new Task(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                ButtonsOnTabsIsEnabled = false;
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
                    ButtonsOnTabsIsEnabled = true;
                    ConnectToDbBtnIsEnabled = true;
                }
            }).Start();
        }

        private void ConnectToDbAndUpdateMigrationList()
        {

            new Task(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                ButtonsOnTabsIsEnabled = false;
                try
                {
                    _config.TargetDatabase = new DbConnectionInfo(ConnectionString, ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName);
                    _migrator = new DbMigrator(_config);
                    UpdateGrid();
                    DbConnectAndDbUpdateStatus = "";
                    ButtonsOnTabsIsEnabled = true;
                }
                catch (Exception exception)
                {
                    MigrationList = new List<Migration>();
                    DbConnectAndDbUpdateStatus = "При подключении к БД возникла ошибка: " + exception.Message;
                    TablesUpdateStatus = "При подключении к БД возникла ошибка: " + exception.Message;
                    ButtonsOnTabsIsEnabled = false;
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
            if (listLocalMigrations.Count == 0)
                DbConnectAndDbUpdateStatus = "Миграции отсутствуют";
            var isSuitableDb = dbMigrationsList.Count == 0;
            foreach (var item in listLocalMigrations)
            {
                var isChecked = dbMigrationsList.Any(dbitem => dbitem == item);
                if (isChecked)
                    isSuitableDb = true;
                list.Add(new Migration() {Name = item, Check = isChecked});
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

        #region ConnectToDbAndUpdateMigrationListCommand
        private ICommand _connectToDbAndUpdateMigrationListCommand;
        public ICommand ConnectToDbAndUpdateMigrationListCommand
        {
            get
            {
                return _connectToDbAndUpdateMigrationListCommand ??
                       (_connectToDbAndUpdateMigrationListCommand = new RelayCommand(ConnectToDbAndUpdateMigrationList));
            }
        }
        #endregion

        #region UpdateDbToLastVersionCommand
        private ICommand _updateDbToLastVersionCommand;
        public ICommand UpdateDbToLastVersionCommand
        {
            get
            {
                return _updateDbToLastVersionCommand ??
                       (_updateDbToLastVersionCommand =
                           new RelayCommand(() => UpdateDb(_migrator.GetLocalMigrations().Last())));
            }
        }
        #endregion

        #region UpdateDbToVersionCommand
        private ICommand _updateDbToVersionCommand;
        public ICommand UpdateDbToVersionCommand
        {
            get
            {
                return _updateDbToVersionCommand ??
                       (_updateDbToVersionCommand =
                           new RelayCommand(() => UpdateDb(SelectedMigration.Name.Contains(LastVersion)
                               ? SelectedMigration.Name.Replace(LastVersion, "")
                               : SelectedMigration.Name)));
            }
        }
        #endregion

        #endregion

        #region AddDataInDbMethods(Second Tab)
        private void ConnectToFileAndUpdateTableList(string path)
        {
            var package = new ExcelPackage(new FileInfo(path));
            var tablesList = new List<Table>();

            foreach (var table in package.Workbook.Worksheets)
            {
                try
                {

                    var workSheet = package.Workbook.Worksheets[table.Index];
                    var tableName = workSheet.Cells[1, 2].Value.ToString();
                    tablesList.Add(new Table()
                    {
                        Check = false,
                        Name = table.Name,
                        FileName = path,
                        Number = table.Index,
                        TableName = tableName
                    });
                }
                catch (Exception)
                {
                    TablesUpdateStatus = "Таблица " + table.Name + " в файле " + path + " не соответствует формату";
                    throw;
                }
            }

            _tablesList.AddRange(tablesList);
        }

        private void CheckedOrUncheckedTableList()
        {
            if (TableList != null)
            {
                var list = new List<Table>(TableList);
                if (IsTableChecked)
                {
                    foreach (var table in list)
                    {
                        table.Check = true;
                    }
                }
                else
                {
                    foreach (var table in list)
                    {
                        table.Check = false;
                    }
                }
                TableList = list;
  
            }
        }

        #region CheckAllTablesInTableListCommand
        private ICommand _checkAllTablesInTableListCommand;
        public ICommand CheckAllTablesInTableListCommand
        {
            get
            {
                return _checkAllTablesInTableListCommand ??
                       (_checkAllTablesInTableListCommand =
                           new RelayCommand(CheckedOrUncheckedTableList));
            }
        }

        #endregion

        #region UpdateTablesCommand

        private ICommand _updateTablesCommand;

        public ICommand UpdateTablesCommand
        {
            get { return _updateTablesCommand ?? (_updateTablesCommand = new RelayCommand(UpdateTables)); }
        }

        private void UpdateTables()
        {
            new Task(() =>
            {
                ProgressBarVisibility = Visibility.Visible;
                ButtonsOnTabsIsEnabled = false;
                List<Table> sortedTablesList;
                try
                {
                    sortedTablesList = CreateSortedTablesFromDbList();
                }
                catch (Exception exception)
                {
                    TablesUpdateStatus = "При подключении к БД возникла ошибка: " + exception.Message;
                    ButtonsOnTabsIsEnabled = true;
                    ProgressBarVisibility = Visibility.Hidden;
                    return;
                }

                //Накатывание изменений на таблицы
                foreach (var table in sortedTablesList)
                {
                    if (table.Check)
                    {
                        ExcelWorksheet workSheet;
                        try
                        {
                            var package = new ExcelPackage(new FileInfo(table.FileName));
                            workSheet = package.Workbook.Worksheets[table.Number];
                        }
                        catch(Exception)
                        {
                            TablesUpdateStatus = "Таблица: " + table.TableName + " в файле: " + table.FileName +
                                                 "открыта в другой программе";
                            ButtonsOnTabsIsEnabled = true;
                            ProgressBarVisibility = Visibility.Hidden;
                            return;
                        }

                        string queryToInsert = "INSERT INTO \"" + table.TableName + "\" VALUES ";
                        for (int j = 5; j <= workSheet.Dimension.End.Row; j++)
                        {
                            queryToInsert += "(";
                            for (int i = workSheet.Dimension.Start.Column; i <= workSheet.Dimension.End.Column; i++)
                            {
                                switch (workSheet.Cells[3, i].Value.ToString())
                                {
                                    case "integer":
                                        queryToInsert += workSheet.Cells[j, i].Value.ToString();
                                        break;
                                    case "double":
                                        queryToInsert += workSheet.Cells[j, i].Value.ToString().Replace(",", ".");
                                        break;
                                    //TODO Возможна ошибка при пустой строке
                                    case "string":
                                        queryToInsert += "'" + workSheet.Cells[j, i].Value + "'";
                                        break;
                                    case "":
                                        queryToInsert += "NULL";
                                        break;
                                }
                                if (i != workSheet.Dimension.End.Column)
                                {
                                    queryToInsert += ", ";
                                }
                            }
                            queryToInsert += "),";
                        }
                        queryToInsert = queryToInsert.Remove(queryToInsert.Length - 1);
                        try
                        {
                            using (var connection = new NpgsqlConnection(ConnectionString))
                            {
                                connection.Open();
                                var command = new NpgsqlCommand(queryToInsert, connection);
                                command.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        catch (Exception exception)
                        {
                            TablesUpdateStatus = "При обновлении таблицы: " + table.TableName + " возникла ошибка: " +
                                                 exception.Message;
                            ButtonsOnTabsIsEnabled = true;
                            ProgressBarVisibility = Visibility.Hidden;
                            return;
                        }
                    }
                }
                TablesUpdateStatus = "Загрузка данных успешно завершенна";
                ButtonsOnTabsIsEnabled = true;
                ProgressBarVisibility = Visibility.Hidden;
            }).Start();
        }

        private List<Table> CreateSortedTablesFromDbList()
        {
            var listDependencies = new List<Dependency>();
            var sortedListDependenciesFull = new List<Dependency>();
            var sortedTablesList = new List<Table>();
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                var command =
                    new NpgsqlCommand(
                        "SELECT n1.nspname AS primary_key_ns, c1.relname AS primary_key_table, n2.nspname AS foreign_key_ns, c2.relname AS foreign_key_table " +
                        "FROM pg_catalog.pg_constraint c " +
                        "JOIN ONLY pg_catalog.pg_class c1 ON c1.oid = c.confrelid " +
                        "JOIN ONLY pg_catalog.pg_class c2 ON c2.oid = c.conrelid " +
                        "JOIN ONLY pg_catalog.pg_namespace n1 ON n1.oid = c1.relnamespace " +
                        "JOIN ONLY pg_catalog.pg_namespace n2 ON n2.oid = c2.relnamespace " +
                        "WHERE c1.relkind = \'r\' AND c.contype = \'f\';", connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                //Заполнение списка зависимостями полей друг от друга 
                while (reader.Read())
                {
                    //foreach (var table in TableList)
                    //{
                    //    if (table.Name == reader.GetString(3))
                    //    {
                            listDependencies.Add(new Dependency()
                            {
                                ChildName = reader.GetString(3),
                                ParentName = reader.GetString(1)
                            });
                    //    }
                    //}
                }
            }

            //listDependencies.Add(new Dependency() { ChildName = "DeviceType", ParentName = "test3" });
            //listDependencies.Add(new Dependency() { ChildName = "Device", ParentName = "DeviceType" });
            //listDependencies.Add(new Dependency() { ChildName = "users", ParentName = "DeviceType" });
            //listDependencies.Add(new Dependency() { ChildName = "users", ParentName = "DeviceType" });
            //listDependencies.Add(new Dependency() { ChildName = "test3", ParentName = "abc1" });

            var secondListDependencies = listDependencies;
            //Выбираем все элементы без родителя
            for (var i = 0; i < listDependencies.Count; i++)
            {
                if (!(listDependencies.Where(dep => dep.ChildName == listDependencies[i].ParentName)).Any())
                {
                    listDependencies[i].Level = 0;
                    sortedListDependenciesFull.Add(listDependencies[i]);
                    secondListDependencies.Remove(listDependencies[i]);
                }
            }
            //Заливаем в список все названия таблиц не имеющих родителя из повторяющихся оставляем только последний
            foreach (var dependency in sortedListDependenciesFull)
            {
                var tableFromList = (TableList.Where(dep => dep.TableName == dependency.ParentName)).SingleOrDefault();
                if (tableFromList != null)
                {
                    var depend =
                        (sortedTablesList.Where(dep => dep.TableName == dependency.ParentName))
                            .SingleOrDefault();
                    if (depend != null)
                    {
                        sortedTablesList.Remove(tableFromList);
                    }
                    sortedTablesList.Add(tableFromList);

                }
            }

            //Выбираем элементы по нарастающему индексу зависимости, чем дальше элемент от родительских, тем индекс больше
            int maxDependencyLevel = 0;
            while (secondListDependencies.Count != 0)
            {
                maxDependencyLevel++;
                var thirdListDependencies = new List<Dependency>(secondListDependencies);
                var oldSortedList = new List<Dependency>(sortedListDependenciesFull);
                foreach (var dependency in thirdListDependencies)
                {
                    if ((oldSortedList.Where(dep => dep.ChildName == dependency.ParentName)).Any())
                    {
                        dependency.Level = maxDependencyLevel;
                        sortedListDependenciesFull.Add(dependency);
                        secondListDependencies.Remove(dependency);
                    }
                }
            }
            //Заливаем в отсортированный список
            foreach (var dependency in sortedListDependenciesFull)
            {
                var tableFromList = (TableList.Where(dep => dep.TableName == dependency.ChildName)).SingleOrDefault();
                if (tableFromList != null)
                {

                    var depend =
                        (sortedTablesList.Where(dep => dep.TableName == dependency.ChildName)).SingleOrDefault
                            ();
                    if (depend != null)
                    {
                        sortedTablesList.Remove(tableFromList);
                    }
                    sortedTablesList.Add(tableFromList);
                }
            }

            return sortedTablesList;
        }

        #endregion

        #region SelectFileCommand
        private ICommand _selectFileCommand;
        public ICommand SelectFileCommand
        {
            get
            {
                if (_selectFileCommand == null)
                {
                    _selectFileCommand = new RelayCommand(OpenFileDialog);
                }
                return _selectFileCommand;
            }
        }

        private void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                CheckFileExists = true,
                Multiselect = true
            };
            if (fileDialog.ShowDialog() == true)
            {
                new Task(() =>
                {
                    _tablesList = new List<Table>();
                    ProgressBarVisibility = Visibility.Visible;
                    ButtonsOnTabsIsEnabled = false;
                    SelectedFilePath = "Выбранные файлы: \n";
                    try
                    {
                        foreach (string name in fileDialog.FileNames)
                        {
                            ConnectToFileAndUpdateTableList(name);
                            SelectedFilePath += name + "\n";
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    finally
                    {
                        TableList = _tablesList;
                        ProgressBarVisibility = Visibility.Hidden;
                        ButtonsOnTabsIsEnabled = true;
                    }
                }).Start();
            }
        }
        #endregion

        #endregion
    }
}
