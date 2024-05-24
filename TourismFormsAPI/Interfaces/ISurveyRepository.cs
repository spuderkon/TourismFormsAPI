using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface ISurveyRepository
    {
        public List<Survey> GetAll();
        public Survey? GetById(int id);
        public Survey Create(SurveyPost body);
        public Survey GetExcel(int id);
        public void Delete(int id);

        public List<Survey> GetMyAll(int municipalityId);
        public Survey? GetMyById(int id, int municipalityId);
        public Survey UpdateMy(int id, int municipalityId, SurveyPut body);

    }
}
