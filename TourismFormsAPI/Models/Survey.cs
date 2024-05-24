using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Survey
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime CompletionDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool Completed { get; set; }

    public string MunicipalityName { get; set; } = null!;

    public int MunicipalityId { get; set; }

    public string CityName { get; set; } = null!;

    public int CityId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual City City { get; set; } = null!;

    public virtual Form Form { get; set; } = null!;

    public virtual Municipality Municipality { get; set; } = null!;
}
