using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Criteria
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int FormId { get; set; }
    public int Sequence { get; set; }
    public virtual Form Form { get; set; } = null!;
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
