using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTService.Domain.Entities
{
    public class GameLobby
    {
        public string Title { get; set; }
        public string? Password { get; set; }

        public int Idhost { get; set; }


    }
}
