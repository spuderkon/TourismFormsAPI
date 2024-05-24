using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class FillMethod
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Hint { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
