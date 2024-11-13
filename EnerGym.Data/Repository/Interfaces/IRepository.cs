namespace EnerGym.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);
        Task<TType> GetByIdAsync(TId id);
        ICollection<TType> GetAll();
        Task<ICollection<TType>> GetAllAsync();
        IQueryable<TType> GetAllAttached();
        void Add(TType item);
        Task AddAsync(TType item);
        bool Delete(TId id);
        Task<bool> DeleteAsync(TId id);
        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
    }
}
