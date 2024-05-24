using TourismFormsAPI.Models;

namespace TourismFormsAPI.ModelsDTO
{
    public class SurveyDTO : Survey
    {
        public SurveyDTO(Survey survey)
        {
            Id = survey.Id;
            FormId = survey.FormId;
            AppointmentDate = survey.AppointmentDate;
            StartDate = survey.StartDate;
            CompletionDate = survey.CompletionDate;
            EndDate = survey.EndDate;
            Completed = survey.Completed;
            MunicipalityName = survey.MunicipalityName;
            MunicipalityId = survey.MunicipalityId;
            CityName = survey.CityName;
            CityId = survey.CityId;
        }
    }
}
