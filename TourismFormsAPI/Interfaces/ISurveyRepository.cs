using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Interfaces
{
    public interface ISurveyRepository
    {
        public List<Survey> GetAll();
        public Survey? GetById(int id);
        public Task CreateArray(SurveyPost[] body);
        public Task<byte[]> GetExcel(int id);
        public void Delete(int id);
        public Task Extend(int id, DateTime newCompletionDate);
        public Task Revision(SurveyRevisionPut body);
        public Task SubmitForEvaluation(int id);
        public List<Survey> GetMyAll(int municipalityId);
        public Form? GetMyById(int id, int municipalityId);
        public Survey UpdateMy(int municipalityId, SurveyPut body);

    }
}
