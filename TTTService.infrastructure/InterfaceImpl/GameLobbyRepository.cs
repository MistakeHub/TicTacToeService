using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTTService.Domain.Entities;

namespace TTTService.infrastructure.InterfaceImpl
{
    public class GameLobbyRepository
    {

        private List<GameLobby> _gamelobbies;

        public GameLobbyRepository()
        {
            _gamelobbies= new List<GameLobby>();
        }

        public List<GameLobby> GetGameLobbies()
        {
            return _gamelobbies.ToList();
        }
        public bool CreateLobby(string title, int idhost, string? password=null)
        {
           
            try
            {
                var Isexists = _gamelobbies.Any(v => v.Title == title);
                if (Isexists) return false ;
                _gamelobbies.Add(new GameLobby { Title = title, Password = password, Idhost=idhost });
            }
            catch (Exception ex)
            {

                Debug.Write(ex.Message);
            }
            return true;
        }
        
        public int JoinToLobby(string title, string? password = null)
        {
            var gamelobby = _gamelobbies.FirstOrDefault(v => v.Title == title);
            if (gamelobby == null) return (int)GameLobbyStatuse.NotFound;
            if (gamelobby.Password != password) return (int)GameLobbyStatuse.WrongPassword;
           RemoveLobby(gamelobby.Title, gamelobby.Idhost);
            return (int)GameLobbyStatuse.Success;
        }

        public int RemoveLobby(string title, int iduser)
        {
            var gamelobby = _gamelobbies.FirstOrDefault(v => v.Title == title);
            if (gamelobby == null) return (int)GameLobbyStatuse.NotFound;
            if (gamelobby.Idhost != iduser) return (int)GameLobbyStatuse.NotAllowd;
           _gamelobbies.Remove(gamelobby);
            return (int)GameLobbyStatuse.Success;
        }




    }
}
