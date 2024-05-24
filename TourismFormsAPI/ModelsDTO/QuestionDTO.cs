using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class QuestionDTO : Question
    {
        public QuestionDTO(Question question)
        {
            Id = question.Id;
            Name = question.Name;
            Hint = question.Hint;
            CriteriaId = question.CriteriaId;
            Sequence = question.Sequence;
            Formula = question.Formula;
            MeasureId = question.MeasureId;
            Hidden = question.Hidden;
            FillMethodId = question.FillMethodId;
        }
    }
}
