using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();
}
