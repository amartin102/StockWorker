using Microsoft.EntityFrameworkCore;
using Repository.Coconseconsentext;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class Repository<TEntity, TContext>: IRepository<TEntity>
        where TEntity : class
        where TContext : StockDb
    {
        protected readonly TContext _context;
        public Repository(TContext context)
        {
            this._context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {                    
                    _context.Set<TEntity>().Add(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                return entity;
            }
            catch (Exception ex)
            {
                return entity;               
            }

        }

        public async Task<List<TEntity>> AddRangeAsync(List<TEntity> entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<TEntity>().AddRangeAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                return entity;
            }
            catch (Exception ex)
            {
                return entity;
            }

        }

        //Inactivar uno a uno
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);

                if (entity != null)
                {
                    var propiedad = entity.GetType().GetProperty("Active");

                    if (propiedad != null)
                    {
                        propiedad.SetValue(entity, false);
                        _context.Update(entity);           
                        _context.SaveChangesAsync();            
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex )
            {
                return false;
                throw;
            }
           
        }

        //Inactivar todos los registros relacionados de una entidad
        public async Task<bool> DeleteAllAsync(int id)
        {
            try
            {               
                var propertyActive = typeof(TEntity).GetProperty("Active");

                if (propertyActive != null)
                {
                    var entities = await _context.Set<TEntity>()
                        .Where(e => EF.Property<int>(e, "IdOrder") == id)
                        .ToListAsync();

                    entities.ForEach(e => propertyActive.SetValue(e, false));

                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

       
        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex )
            {

                throw;
            }
            
        }

        public async Task<TEntity> GetByIdAsync(int id, bool entityType, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            try
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();
                               
                if (includeFunc != null)
                {
                    query = includeFunc(query);
                }

                if(entityType)
                {
                    query = query.Where(e => EF.Property<int>(e, "IdRecipe") == id);
                }
                else
                {
                    query = query.Where(e => EF.Property<int>(e, "IdIngredient") == id);
                }

                return await query.FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                throw;
            }            
        }

        public async Task<TEntity> GetByIdAsync(int id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
        {
            try
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                if (includeFunc != null)
                {
                    query = includeFunc(query);
                }

                return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "IdOrder") == id);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    var id = entity.GetType().GetProperty("IdIngredient").GetValue(entity);
                    var _entity = await _context.Set<TEntity>().FindAsync(id);

                    if (_entity != null)
                    {
                        _context.Entry(_entity).CurrentValues.SetValues(entity);
                        await _context.SaveChangesAsync();
                        return true;
                    }                   
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public async Task<bool> UpdateAsync(List<TEntity> entities)
        {
            try
            {
                if (entities != null && entities.Any())
                {
                    foreach (var entity in entities)
                    {
                        var idProperty = entity.GetType().GetProperty("IdIngredient");
                        if (idProperty == null) continue;

                        var id = idProperty.GetValue(entity);
                        var _entity = await _context.Set<TEntity>().FindAsync(id);

                        if (_entity != null)
                        {
                            _context.Entry(_entity).CurrentValues.SetValues(entity);
                        }
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // log exception si es necesario
                return false;
                // throw; // Esto nunca se ejecuta porque el return viene antes
            }
        }

    }
}
