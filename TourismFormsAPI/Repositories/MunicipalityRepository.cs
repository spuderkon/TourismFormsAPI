using System.Net;

using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly TourismContext _context;

        public MunicipalityRepository(TourismContext context)
        {
            _context = context;
        }

        #region GET
        public List<Municipality> GetAll()
        {
            return _context.Municipalities.ToList();
        }

        public Municipality? GetById(int id)
        {
            var municipality = _context.Municipalities.FirstOrDefault(x => x.Id == id);
            if (municipality is not null)
                return municipality;

            throw new Exception();
        }
        #endregion

        #region POST
        public Municipality Create(MunicipalityPost body)
        {
            Municipality itemToCreate = new Municipality()
            {
                Name = body.Name,
                RegionId = body.RegionId,
                Login = body.Login,
                Password = body.Password,
                Email = body.Email,
                IsAdmin = body.IsAdmin,
            };
            _context.Municipalities.Add(itemToCreate);
            _context.SaveChanges();
            return itemToCreate;
        }
        #endregion

        #region PUT
        public Municipality Update(int id,MunicipalityUpdate body)
        {
            var itemToUpdate = _context.Municipalities.FirstOrDefault(m => m.Id == id);
            if (itemToUpdate is not null)
            {
                itemToUpdate.Name = body.Name;
                itemToUpdate.RegionId = body.RegionId;
                itemToUpdate.Email = body.Email;
                itemToUpdate.IsAdmin = body.IsAdmin;

                _context.Municipalities.Update(itemToUpdate);
                _context.SaveChanges();
                return itemToUpdate;
            }
            throw new Exception();
        }
        #endregion

        #region DELETE
        public void Delete(int id)
        {
            var itemToDelete = _context.Municipalities.FirstOrDefault(m => m.Id == id);
            if (itemToDelete is not null)
            {
                _context.Municipalities.Remove(itemToDelete);
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
