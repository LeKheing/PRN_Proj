using ManageInventory.ViewModel;
using System;
using System.Collections.Generic;

namespace ManageInventory.Models;

public partial class OutputInfo : BaseViewModel
{
    public string Id { get; set; } = null!;

    public string IdObject { get; set; } = null!;

    public string IdOutput { get; set; } = null!;

    private int _IdCustomer;
    public int IdCustomer { get => _IdCustomer; set { _IdCustomer = value; OnPropertyChanged(); } }

    private int? _Count;
    public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

    private string _Status;
    public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Object IdObjectNavigation { get; set; } = null!;

    public virtual Output IdOutputNavigation { get; set; } = null!;
}
