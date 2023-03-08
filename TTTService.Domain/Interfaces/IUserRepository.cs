using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTTService.Domain.Entities;

namespace TTTService.Domain.Interfaces
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public  Task<bool> IsUserExists(string login);
        public Task<User> CheckPasswordAndUsser(string login, string password);
    }
}
