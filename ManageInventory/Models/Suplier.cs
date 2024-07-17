using ManageInventory.ViewModel;
using System;
using System.Collections.Generic;

namespace ManageInventory.Models;

public partial class Suplier : BaseViewModel
{
    private int _Id { get; set; }
    public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

    private string _DisplayName;
    public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

    string _Address;
    public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

    string _Phone;
    public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }

    string _Email;
    public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

    string? _MoreInfo;
    public string? MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }

    Nullable<System.DateTime> _ContractDate;
    public Nullable<System.DateTime> ContractDate { get => _ContractDate; set { _ContractDate = value; OnPropertyChanged(); } }

    public virtual ICollection<Object> Objects { get; set; } = new List<Object>();
}
