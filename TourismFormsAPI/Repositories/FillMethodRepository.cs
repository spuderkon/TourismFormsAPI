using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class FillMethodRepository : IFillMethodRepository
    {
        private readonly TourismContext _context;

        public FillMethodRepository(TourismContext context)
        {
            _context = context;
        }

        #region GET
        public async Task<IEnumerable<FillMethod>> GetAll()
        {
            return await _context.FillMethods.ToListAsync();
        }
        #endregion

        #region POST
        public Task Create(FillMethodPost body)
        {
            try
            {
                var itemToCreate = new FillMethod
                {
                    Name = body.Name,
                };
                _context.FillMethods.Add(itemToCreate);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PUT
        public Task Update(FillMethod body)
        {
            try
            {
                var itemToUpdate = _context.FillMethods.FirstOrDefault(f => f.Id == body.Id);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Name = body.Name;
                    _context.FillMethods.Update(itemToUpdate);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                {
                    throw new Exception("Not Found");
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DELETE
        public Task Delete(int id)
        {
            try
            {
                var itemToUpdate = _context.FillMethods.FirstOrDefault(f => f.Id == id);
                if (itemToUpdate is not null)
                {
                    _context.FillMethods.Remove(itemToUpdate);
                    _context.SaveChanges();
                    return Task.CompletedTask;
                }
                else
                {
                    throw new Exception("Not Found");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
