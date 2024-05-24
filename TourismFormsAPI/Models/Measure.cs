using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Measure
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
