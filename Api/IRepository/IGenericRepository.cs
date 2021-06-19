using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace Api.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);

        //call for when we want to allow the client to use query params.. like pagesize pagenumber
        Task<IPagedList<T>> GetAllPagedList(
          RequestParams requestParams = null,
          List<string> includes = null       
      );


        bool Exists(string username);
        bool Exists(int key);
    }
}
