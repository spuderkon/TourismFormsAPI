namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class SurveyRevisionPut
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
