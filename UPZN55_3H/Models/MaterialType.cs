using System;
using System.Collections.Generic;

namespace UPZN55_3H.Models;

public partial class MaterialType
{
    public byte MaterialTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; } = new List<Material>();
}
