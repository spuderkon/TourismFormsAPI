namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class AnswerPut
    {
        public int? Id { get; set; }

        public int SurveyId { get; set; }

        public string? Text { get; set; }

        public double Score { get; set; }

        public int QuestionId { get; set; }
    }
}
