using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTTService.Domain.Entities;

namespace TTTService.Domain.Interfaces
{
    public interface IGameMoveRepository:IBaseRepository<Move>
    {
        public Task<List<Move>> GetAllMovesByGameID(int id);
    }
}
