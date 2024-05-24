using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class FormDTO : Form
    {
        public FormDTO(Form form)
        {
            Id = form.Id;
            Name = form.Name;
            CreationDate = form.CreationDate;
            ModifiedDate = form.ModifiedDate;
        }
    }
}
