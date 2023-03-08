using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.infrastructure.Data;

namespace TTTService.infrastructure.InterfaceImpl
{
   public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected TttserviceContext _context;

        public  BaseRepository(TttserviceContext context) { _context = context; }
        public virtual async Task<int> Add(T entity)
        {
            int result = 0;
            try
            {
               _context.Set<T>().Add(entity);
                result =await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }

        public async Task<List<T>> GetAll()
        {
          List<T> result=new List<T>();
            try
            {
                result = await _context.Set<T>().ToListAsync();

            }catch(Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }

        public async Task<T> GetById(int id)
        {
          T result = null;
            try
            {
                result =await _context.Set<T>().FirstOrDefaultAsync(v => v.Id == id);

            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }

        public async Task<bool> Update(T entity)
        {
            int result = 0;
            try
            {
                 _context.Set<T>().Update(entity);
                result = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result > 0;
        }
    }
}
