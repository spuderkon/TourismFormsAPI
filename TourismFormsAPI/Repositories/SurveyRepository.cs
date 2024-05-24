using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly TourismContext _context;

        public SurveyRepository(TourismContext context)
        {
            _context = context;
        }

        #region GET
        public List<Survey> GetAll()
        {
            return _context.Surveys.ToList();
        }
        public Survey? GetById(int id)
        {
            return _context.Surveys.FirstOrDefault(s => s.Id == id);
        }
        public Survey GetExcel(int id)
        {
            throw new NotImplementedException();
        }
        public List<Survey> GetMyAll(int municipalityId)
        {
            return _context.Surveys.Include(s => s.Form).Where(s => s.MunicipalityId == municipalityId).ToList();
        }
        public Survey? GetMyById(int id, int municipalityId)
        {
            var survey = _context.Surveys.FirstOrDefault(s => s.Id == id && s.MunicipalityId == municipalityId);
            if (survey is not null)
                return survey;

            throw new Exception();
        }
        #endregion

        #region POST
        public Survey Create(SurveyPost body)
        {
            Survey itemToCreate = new Survey()
            {
                FormId = body.FormId,
                AppointmentDate = body.AppointmentDate,
                StartDate = body.StartDate,
                CompletionDate = body.CompletionDate,
                EndDate = body.EndDate,
                Completed = body.Completed,
                MunicipalityName = _context.Municipalities.FirstOrDefault(m => m.Id == body.MunicipalityId).Name,
                MunicipalityId = body.MunicipalityId,
                CityName = _context.Cities.FirstOrDefault(m => m.Id == body.CityId).Name,
                CityId = body.CityId,
            };
            _context.Surveys.Add(itemToCreate);
            _context.SaveChanges();
            return itemToCreate;
        }
        #endregion

        #region PUT
        public Survey UpdateMy(int id, int municipalityId, SurveyPut body)
        {
            var itemToUpdate = _context.Surveys.FirstOrDefault(s => s.Id == id && s.MunicipalityId == municipalityId);
            if (itemToUpdate is not null)
            {
                itemToUpdate.FormId = body.FormId;
                itemToUpdate.AppointmentDate = body.AppointmentDate;
                itemToUpdate.StartDate = body.StartDate;
                itemToUpdate.CompletionDate = body.CompletionDate;
                itemToUpdate.EndDate = body.EndDate;
                itemToUpdate.Completed = body.Completed;
                itemToUpdate.MunicipalityName = body.MunicipalityName;
                itemToUpdate.MunicipalityId = body.MunicipalityId;
                itemToUpdate.CityName = body.CityName;
                itemToUpdate.CityId = body.CityId;

                _context.Surveys.Update(itemToUpdate);
                _context.SaveChanges();
                return itemToUpdate;
            }
            throw new Exception();
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var itemToDelete = _context.Surveys.FirstOrDefault(m => m.Id == id);
            if (itemToDelete is not null)
            {
                _context.Surveys.Remove(itemToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }
        #endregion

    }
}
