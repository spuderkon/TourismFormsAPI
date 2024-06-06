using Microsoft.EntityFrameworkCore;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class MeasureRepository : IMeasureRepository
    {
        private readonly TourismContext _context;

        public MeasureRepository(TourismContext context)
        {
            _context = context;
        }
        #region GET
        public async Task<List<Measure>> GetAll()
        {
           return await _context.Measures.ToListAsync();
        }
        #endregion

        #region POST
        public async Task<Measure> Create(MeasurePost body)
        {
            try
            {
                var itemToCreate = new Measure()
                {
                    Name = body.Name,
                };
                await _context.Measures.AddAsync(itemToCreate);
                await _context.SaveChangesAsync();
                
                return itemToCreate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PUT
        public Task Update(MeasurePut body)
        {
            try
            {
                var itemToUpdate = _context.Measures.FirstOrDefault(m => m.Id == body.Id);
                if (itemToUpdate is not null)
                {
                    itemToUpdate.Name = body.Name;
                    _context.Measures.Update(itemToUpdate);
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

        #region DELETE
        public Task Delete(int id)
        {
            var itemToDelete = _context.Measures.FirstOrDefault(m => m.Id == id);
            if (itemToDelete is not null)
            {
                _context.Measures.Remove(itemToDelete);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            else
                return Task.FromException(new Exception("NotFound"));
        }
        #endregion
    }
}
