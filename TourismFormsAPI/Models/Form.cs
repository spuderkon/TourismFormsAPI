using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Form
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public virtual ICollection<Criteria> Criterias { get; set; } = new List<Criteria>();
    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();
}
