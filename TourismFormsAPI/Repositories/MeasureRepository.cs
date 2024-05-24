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
        public List<Measure> GetAll()
        {
           return _context.Measures.ToList();
        }
        #endregion

        #region POST
        public void Delete(int id)
        {
            var itemToDelete = _context.Measures.FirstOrDefault(m => m.Id == id);
            if (itemToDelete is not null)
            {
                _context.Measures.Remove(itemToDelete);
                _context.SaveChanges();
            }
            else
                throw new Exception();
        }
        #endregion

        #region PUT
        public Measure Create(string name)
        {
            var itemToCreate = new Measure()
            {
                Name = name,
            };
            _context.Measures.Add(itemToCreate);
            _context.SaveChanges();
            return itemToCreate;
        }
        #endregion
    }
}
