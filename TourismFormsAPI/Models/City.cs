using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MunicipalityId { get; set; }

    public virtual Municipality Municipality { get; set; } = null!;

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
