using ManageInventory.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManageInventory.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Unit _SelectedItem;
        public Unit SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                var displayList = DataProvider.Ins.DB.Units.Where(p => p.DisplayName == DisplayName).ToList();
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var unit = new Unit()
                {
                    DisplayName = DisplayName,
                };
                DataProvider.Ins.DB.Units.Add(unit);

                DataProvider.Ins.DB.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Units.Where(p => p.DisplayName == DisplayName).ToList();
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var unit = DataProvider.Ins.DB.Units.Where(p => p.Id == SelectedItem.Id).First();
                unit.DisplayName = DisplayName;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.DisplayName = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var unit = DataProvider.Ins.DB.Units.Where(p => p.Id == SelectedItem.Id).First();
                var objList = DataProvider.Ins.DB.Objects.Where(p => p.IdUnit == unit.Id).ToList();
                if (objList != null && objList.Count() > 0)
                {
                    foreach (var obj in objList)
                    {
                        var input = DataProvider.Ins.DB.InputInfos.Where(p => p.IdObject == obj.Id).ToList();
                        if (input != null && input.Count() > 0)
                            foreach (var inputInfo in input)
                                DataProvider.Ins.DB.InputInfos.Remove(inputInfo);
                        var output = DataProvider.Ins.DB.OutputInfos.Where(p => p.IdObject == obj.Id).ToList();
                        if (output != null && output.Count() > 0)
                            foreach (var outputInfo in output)
                                DataProvider.Ins.DB.OutputInfos.Remove(outputInfo);
                        DataProvider.Ins.DB.Objects.Remove(obj);
                    }
                }
                DataProvider.Ins.DB.Units.Remove(unit);
                DataProvider.Ins.DB.SaveChanges();
                List.Remove(SelectedItem);
            });
        }
    }
}
