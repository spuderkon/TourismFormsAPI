using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class CriteriaRepository : ICriteriaRepository
    {
        private readonly TourismContext _context;

        public CriteriaRepository(TourismContext context)
        {
            _context = context;
        }

        public List<Criteria> GetAll()
        {
            return _context.Criteria.ToList();
        }

        public List<Criteria> CreateArray(CriteriaPost[] body)
        {
            try
            {
                var criterias = new List<Criteria>();
                foreach (var item in body)
                {
                    var itemToCreate = new Criteria()
                    {
                       Name = item.Name,
                       FormId = item.FormId,
                       Sequence = item.Sequence,
                    };
                    _context.Criteria.Add(itemToCreate);
                    criterias.Add(itemToCreate);
                }
                _context.SaveChanges();
                return criterias;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task UpdateArray(CriteriaPut[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    var itemToUpdate = _context.Criteria.FirstOrDefault(a => a.Id == item.Id);
                    
                    if (itemToUpdate is not null)
                    {
                        itemToUpdate.Name = item.Name;
                        itemToUpdate.FormId = item.FormId;
                        itemToUpdate.Sequence = item.Sequence;
                        _context.Criteria.Update(itemToUpdate);
                    }
                }
                
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
