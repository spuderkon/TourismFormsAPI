namespace TourismFormsAPI.ModelsDTO.Requests
{
    public class AnswerPost
    {
        public int SurveyId { get; set; }

        public string Text { get; set; } = null!;

        public int QuestionId { get; set; }
    }
}
