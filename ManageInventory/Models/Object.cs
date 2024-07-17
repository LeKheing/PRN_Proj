using ManageInventory.ViewModel;
using System;
using System.Collections.Generic;

namespace ManageInventory.Models;

public partial class Object : BaseViewModel
{
    private string _Id;
    public string Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
    private int _IdUnit;
    public int IdUnit { get => _IdUnit; set { _IdUnit = value; OnPropertyChanged(); } }
    private int _IdSuplier;
    public int IdSuplier{ get => _IdSuplier; set { _IdSuplier = value; OnPropertyChanged(); } }

    public virtual ICollection<InputInfo> InputInfos { get; set; }

    private Suplier _IdSuplierNavigation;
    public virtual Suplier IdSuplierNavigation { get => _IdSuplierNavigation; set { _IdSuplierNavigation = value; OnPropertyChanged(); } }

    private Unit _IdUnitNavigation;
    public virtual Unit IdUnitNavigation { get => _IdUnitNavigation; set { _IdUnitNavigation = value; OnPropertyChanged(); } }

    public virtual ICollection<OutputInfo> OutputInfos { get; set; }
}
