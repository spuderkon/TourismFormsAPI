using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Question
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Hint { get; set; }
    public int? CriteriaId { get; set; }
    public int? Sequence { get; set; }
    public string? Formula { get; set; }
    public int? MeasureId { get; set; }
    public bool? Hidden { get; set; }
    public int? FillMethodId { get; set; }
    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    public virtual Criteria? Criteria { get; set; }
    public virtual FillMethod? FillMethod { get; set; }
    public virtual Measure? Measure { get; set; }
}
