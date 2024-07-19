using ManageInventory.Models;
using ManageInventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ManageInventory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList; set { _InventoryList = value; OnPropertyChanged(); } }
        private int _totalSumInput;
        public int TotalSumInput
        {
            get => _totalSumInput;
            set { _totalSumInput = value; OnPropertyChanged(); }
        }

        private int _totalSumOutput;
        public int TotalSumOutput
        {
            get => _totalSumOutput;
            set { _totalSumOutput = value; OnPropertyChanged(); }
        }

        private int _totalInventoryCount;
        public int TotalInventoryCount
        {
            get => _totalInventoryCount;
            set { _totalInventoryCount = value; OnPropertyChanged(); }
        }
        public bool IsLoaded { get; set; } = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand ObjectCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM!.IsLogin)
                {
                    p.Show();
                    LoadInventoryData();
                }
                else
                {
                    p.Close();
                }
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new UnitWindow());
            });

            SupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new SupplierWindow());
            });

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new CustomerWindow());
            });

            ObjectCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new ObjectWindow());
            });

            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new InputWindow());
            });

            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenWindow(new OutputWindow());
            });
        }

        private void OpenWindow(Window window)
        {
            // Get the current main window
            var mainWindow = Application.Current.MainWindow;

            // Hide the main window
            mainWindow.Hide();

            // Show the new window
            window.ShowDialog();

            // Reload data when the window is closed
            LoadInventoryData();

            // Show the main window again
            mainWindow.Show();
        }

        void LoadInventoryData()
        {
            InventoryList = new ObservableCollection<Inventory>();

            var objectList = DataProvider.Ins.DB.Objects.ToList();
            int i = 1;
            int totalInput = 0;
            int totalOutput = 0;

            foreach (var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.InputInfos.Where(p => p.IdObject == item.Id).ToList();
                var outputList = DataProvider.Ins.DB.OutputInfos.Where(p => p.IdObject == item.Id).ToList();

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null)
                {
                    sumInput = inputList.Sum(p => p.Count).Value;
                }

                if (outputList != null)
                {
                    sumOutput = outputList.Sum(p => p.Count).Value;
                }

                totalInput += sumInput;
                totalOutput += sumOutput;

                Inventory inventory = new Inventory
                {
                    No = i++,
                    Count = sumInput - sumOutput,
                    Object = item
                };

                InventoryList.Add(inventory);
            }

            TotalSumInput = totalInput;
            TotalSumOutput = totalOutput;
            TotalInventoryCount = totalInput - totalOutput;

        }
    }
}
