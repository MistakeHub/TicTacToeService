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
using TTTService.infrastructure.InterfaceImpl;

namespace TTTService.infrastructure.InterfaceImpl
{
   public class UserRepository : BaseRepository<User> , IUserRepository
    {
        public UserRepository(TttserviceContext context) : base(context)
        {
        }

        public async Task<bool> IsUserExists(string login)
        {
           bool result = false;
            try
            {
                result=await _context.Users.AnyAsync(u => u.Login == login);

            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }
        public async Task<User> CheckPasswordAndUsser(string login, string password)
        {
            User result = null ;
            try
            {
                result = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password==password);

            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }


    }
}
