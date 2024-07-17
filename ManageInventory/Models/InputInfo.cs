using ManageInventory.ViewModel;
using System;
using System.Collections.Generic;

namespace ManageInventory.Models;

public partial class InputInfo : BaseViewModel
{
    public string Id { get; set; } = null!;

    public string IdObject { get; set; } = null!;

    public string IdInput { get; set; } = null!;

    private int? _Count;
    public int? Count { get => _Count; set { _Count = value; OnPropertyChanged(); } }

    private double? _InputPrice;
    public double? InputPrice { get => _InputPrice; set { _InputPrice = value; OnPropertyChanged(); } }

    private double? _OutputPrice;
    public double? OutputPrice { get => _OutputPrice; set { _OutputPrice = value; OnPropertyChanged(); } }

    private string? _Status;
    public string? Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

    public virtual Input IdInputNavigation { get; set; } = null!;

    public virtual Object IdObjectNavigation { get; set; } = null!;
}
