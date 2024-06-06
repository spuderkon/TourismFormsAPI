using System.Net;

using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;
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
            return LoadData(_context.Municipalities.Include(m => m.Region)).ToList();
        }

        public Municipality? GetById(int id)
        {
            var municipality = _context.Municipalities.FirstOrDefault(x => x.Id == id);
            if (municipality is not null)
                return municipality;

            throw new Exception();
        }

        private IQueryable<Municipality> LoadData(IQueryable<Municipality> items)
        {
            return items
                .Select(x => new MunicipalityDTO(x)
                {
                    Region = new RegionDTO(x.Region)
                });
        }
        #endregion

        #region POST
        public async Task<Municipality> Create(MunicipalityPost body)
        {
            try
            {
                Municipality itemToCreate = new Municipality()
                {
                    Name = body.Name,
                    RegionId = body.RegionId,
                    Email = body.Email,
                    IsAdmin = body.IsAdmin,
                };
                await _context.Municipalities.AddAsync(itemToCreate);
                await _context.SaveChangesAsync();
                itemToCreate = _context.Municipalities.Include(m => m.Region).First(m => m.Id == itemToCreate.Id);
                return itemToCreate;
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PUT
        public Task Update(MunicipalityPut body)
        {
            try
            {
                var itemToUpdate = _context.Municipalities.FirstOrDefault(m => m.Id == body.Id);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Name = body.Name;
                    itemToUpdate.RegionId = body.RegionId;
                    itemToUpdate.Email = body.Email;
                    itemToUpdate.IsAdmin = body.IsAdmin;

                    _context.Municipalities.Update(itemToUpdate);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                return Task.FromException(new Exception("NotFound"));
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
        #endregion

        #region DELETE
        public Task Delete(int id)
        {
            try
            {
                var itemToDelete = _context.Municipalities.FirstOrDefault(m => m.Id == id);
                if (itemToDelete is not null)
                {
                    _context.Municipalities.Remove(itemToDelete);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                    return Task.FromException(new Exception("NotFound"));
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }
        #endregion
    }
}
