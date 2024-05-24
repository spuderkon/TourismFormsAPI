using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class FillMethodDTO : FillMethod
    {
        public FillMethodDTO(FillMethod fillMethod)
        {
            Id = fillMethod.Id;
            Name = fillMethod.Name;
            Hint = fillMethod.Hint;
        }
    }
}
