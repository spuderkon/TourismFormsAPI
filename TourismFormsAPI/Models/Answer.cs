using System;
using System.Collections.Generic;

namespace TourismFormsAPI.Models;

public partial class Answer
{
    public int Id { get; set; }

    public int SurveyId { get; set; }

    public string Text { get; set; } = null!;

    public double Score { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Survey Survey { get; set; } = null!;
}
