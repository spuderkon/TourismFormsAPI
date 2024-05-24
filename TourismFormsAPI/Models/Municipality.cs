using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Municipality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RegionId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Region Region { get; set; } = null!;

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
