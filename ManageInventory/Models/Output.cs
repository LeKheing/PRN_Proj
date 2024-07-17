using System;
using System.Collections.Generic;

namespace ManageInventory.Models;

public partial class Output
{
    public string Id { get; set; } = null!;

    public DateTime? DateOutput { get; set; }

    public virtual ICollection<OutputInfo> OutputInfos { get; set; } = new List<OutputInfo>();
}
