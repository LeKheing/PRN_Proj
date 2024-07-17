using ManageInventory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManageInventory.ViewModel
{
    public class ObjectViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Object> _List;
        public ObservableCollection<Models.Object> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Models.Unit> _Unit;
        public ObservableCollection<Models.Unit> Unit { get => _Unit; set { _Unit = value; OnPropertyChanged(); } }

        private ObservableCollection<Models.Suplier> _Suplier;
        public ObservableCollection<Models.Suplier> Suplier { get => _Suplier; set { _Suplier = value; OnPropertyChanged(); } }

        private Models.Object _SelectedItem;
        public Models.Object SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    DisplayName = SelectedItem.DisplayName;
                    SelectedUnit = SelectedItem.IdUnitNavigation;
                    SelectedSuplier = SelectedItem.IdSuplierNavigation;
                }
            }
        }

        private Models.Unit _SelectedUnit;
        public Models.Unit SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private Models.Suplier _SelectedSuplier;
        public Models.Suplier SelectedSuplier
        {
            get => _SelectedSuplier;
            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();
            }
        }
        private string _Id;
        public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }


        private string _QRCode;
        public string QRCode { get => _QRCode; set { _QRCode = value; OnPropertyChanged(); } }


        private string _BarCode;
        public string BarCode { get => _BarCode; set { _BarCode = value; OnPropertyChanged(); } }


        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }


        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }


        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ObjectViewModel()
        {
            List = new ObservableCollection<Models.Object>(DataProvider.Ins.DB.Objects.Include(o => o.IdUnitNavigation).Include(o => o.IdSuplierNavigation));
            Unit = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
            Suplier = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers.ToList());
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedSuplier == null || SelectedUnit == null)
                    return false;
                return true;

            }, (p) =>
            {
                var Object = new Models.Object() { Id = (List.Count() + 1).ToString(), DisplayName = DisplayName, IdSuplier = SelectedSuplier.Id, IdUnit = SelectedUnit.Id};

                DataProvider.Ins.DB.Objects.Add(Object);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Object);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSuplier == null || SelectedUnit == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;

            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                Object.DisplayName = DisplayName;
                Object.IdSuplier = SelectedSuplier.Id;
                Object.IdUnit = SelectedUnit.Id;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;

            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.Objects.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var input = DataProvider.Ins.DB.InputInfos.Where(x => x.IdObject == Object.Id).SingleOrDefault();
                var output = DataProvider.Ins.DB.OutputInfos.Where(x => x.IdObject == Object.Id).SingleOrDefault();

                if (input != null)
                    DataProvider.Ins.DB.InputInfos.Remove(input);
                if (output != null)
                    DataProvider.Ins.DB.OutputInfos.Remove(output);

                DataProvider.Ins.DB.Objects.Remove(Object);
                DataProvider.Ins.DB.SaveChanges();
                SelectedItem.DisplayName = DisplayName;
            });
        }
    }
}
