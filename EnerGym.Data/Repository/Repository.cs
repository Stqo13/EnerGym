﻿using EnerGym.Data.Models.Configurations;
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
            var entity = this.dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            var entity = await this.dbSet
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
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TId id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TType item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TType item)
        {
            throw new NotImplementedException();
        }
    }
}
