namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class QuestionPost
    {
        public string? Name { get; set; }
        public string? Hint { get; set; }
        public int? CriteriaId { get; set; }
        public int? Sequence { get; set; }
        public string? Formula { get; set; }
        public int? MeasureId { get; set; }
        public bool? Hidden { get; set; }
        public int? FillMethodId { get; set; }
    }
}
