using EnerGym.Data.Models.Configurations;
using EnerGym.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Formats.Tar;

namespace EnerGym.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly EnerGymDbContext context;
        private readonly DbSet<TType> dbSet;

        public Repository(EnerGymDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TType>();
        }

        public TType GetById(TId id)
        {
            TType entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet
                .FindAsync(id);

            return entity;
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }

        public IEnumerable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public void Add(TType item)
        {
            this.dbSet.Add(item);
            this.context.SaveChanges();
        }

        public async Task AddAsync(TType item)
        {
            await this.dbSet.AddAsync(item);
            await this.context.SaveChangesAsync();
        }

        public bool Delete(TId id)
        {
            TType entity = this.GetById(id);

            if (entity == null)
            {
                return false;
            }

            this.dbSet.Remove(entity);
            this.context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = await this.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            this.dbSet.Remove(entity);
            await this.context.SaveChangesAsync();

            return true;
        }

        public bool Update(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                this.context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                await this.context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
