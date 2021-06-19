using Api.IRepository;
using Api.Models;
using Data;
using IEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Api.Repository
{
    // change class to IEntities
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly WmContext _context;
        private readonly DbSet<T> _db;


        public GenericRepository(WmContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        //Deleting a single object from the database via its ID
        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);
        }

        //Deleting a range of objects from the database
        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        //get a single object
        //includes ->> list of strings that represent the nested components
        //expression https://www.tutorialsteacher.com/linq/linq-expression
        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        //get all objects from table
        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query =  orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        //update object from table
        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        //Inserting a new entity into the database
        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity); 
        }
       
        //Inserting a list of entities into the database
        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        //paging 

        public async Task<IPagedList<T>> GetAllPagedList(RequestParams requestParams, List<string> includes = null)
        {
            IQueryable<T> query = _db;
      

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }        

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        //checking if user exists
        public bool Exists(string username)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));

            return _context.Users.Any(u => u.Email.Equals(username));
        }

        //not implemented yet
        //checking if entity exists where key int
        public bool Exists(int key)
        {
            throw new NotImplementedException();
        }
    }
}
