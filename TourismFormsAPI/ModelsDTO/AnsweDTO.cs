using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class AnsweDTO : Answer
    {
        public AnsweDTO(Answer answer)
        {
            Id = answer.Id;
            SurveyId = answer.SurveyId;
            Text = answer.Text;
            Score = answer.Score;
            QuestionId = answer.QuestionId;
        }
    }
}
