using EventOrganizator.Application.Repositories;
using EventOrganizator.Domain.Entities.Common;
using EventOrganizator.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventOrganizator.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly EventOrganizatorDbContext _eventOrganizatorDbContext;
        public GenericRepository(EventOrganizatorDbContext eventOrganizatorDbContext)
        {
            _eventOrganizatorDbContext = eventOrganizatorDbContext;
        }

        public DbSet<TEntity> Table => _eventOrganizatorDbContext.Set<TEntity>();

        public async Task<TEntity> Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsDeleted = false;

            await _eventOrganizatorDbContext.AddAsync(entity);
            await _eventOrganizatorDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> AddRange(List<TEntity> entity)
        {
            await _eventOrganizatorDbContext.AddRangeAsync(entity);
            await _eventOrganizatorDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            entity.IsDeleted = true;

            _ = _eventOrganizatorDbContext.Update(entity);
            await _eventOrganizatorDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<int> HardDelete(TEntity entity)
        {
            _ = _eventOrganizatorDbContext.Remove(entity);
            return await _eventOrganizatorDbContext.SaveChangesAsync();
        }
        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _eventOrganizatorDbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return await (filter == null ? _eventOrganizatorDbContext.Set<TEntity>().Where(e => e.IsDeleted == false).ToListAsync() : _eventOrganizatorDbContext.Set<TEntity>().Where(filter).Where(e => e.IsDeleted == false).ToListAsync());
        }



        public async Task<TEntity> Update(TEntity entity)
        {
            entity.UpdatedDate = DateTime.Now;

            _ = _eventOrganizatorDbContext.Update(entity);
            await _eventOrganizatorDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> UpdateRange(List<TEntity> entity)
        {
            _eventOrganizatorDbContext.UpdateRange(entity);
            await _eventOrganizatorDbContext.SaveChangesAsync();
            return entity;
        }
    }
}
