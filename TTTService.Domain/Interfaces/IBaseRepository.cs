using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTService.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        public  Task<int> Add(T entity);

        public Task<bool> Update(T entity);

        public Task<T> GetById(int id);

        public Task<List<T>> GetAll();

    }
}
