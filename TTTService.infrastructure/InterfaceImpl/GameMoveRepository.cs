using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mysqlx.Resultset;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
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
    public class GameMoveRepository : BaseRepository<Move>, IGameMoveRepository
    {
        private const int Row = 3;
        private const int Column = 3;
        public GameMoveRepository(TttserviceContext context) : base(context)
        {
        }

        public async Task<List<Move>> GetAllMovesByGameID(int id)
        {
            List<Move> result = new List<Move>();
            try
            {
                result = await _context.Moves.Where(v => v.Idgame == id).ToListAsync();

            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }
        public override async Task<int> Add(Move entity)
        {
          
            int result = 0;
            try
            {
                Game game = _context.Games.FirstOrDefault(d => d.Id == entity.Idgame);
                var moves = _context.Moves.Where(v=>v.Idgame==entity.Idgame).ToList();
                bool Isparticipant = game.Iduser1 == entity.Iduser || game.Iduser2 == entity.Iduser;
                bool isItturn = game.Idcurrentturn == entity.Iduser;
                bool Ismatch = moves.Any(v => v.Idgame == entity.Idgame && v.Row == entity.Row && v.Column == entity.Column);
                bool Isabltetomove = entity.Column <= Column && entity.Row <= Row;
                bool isneedtocheck = true;
                int countmoves = moves.Count();
                if (Ismatch || !Isabltetomove) result = (int)MoveStatuses.WrongInput;
                if (countmoves >= (Row * Column)) result = (int)MoveStatuses.NomoreMoves;
                if (!Isparticipant || game.Isfinished) result = (int)MoveStatuses.NotAllowd;
                if (!isItturn) result = (int)MoveStatuses.NotItTurn;
                if (!Ismatch && Isabltetomove && countmoves <(Row*Column) && isItturn && !game.Isfinished)
                {

                    _context.Moves.Add(entity);
                    await _context.SaveChangesAsync();
                    int inrow = 1;
                    if (entity.Row == 2 && entity.Column == 2 && entity.Row == 1 && entity.Column == 3 && entity.Row == 3 && entity.Column == 1 && entity.Row == 1 && entity.Column == 1 && entity.Row == 3 && entity.Column == 3)
                    {

                        for (int i = 1; i <= Row; i++)
                        {
                            if (!moves.Any(v => v.Column == i && v.Row == i && v.Iduser == v.Iduser) || !moves.Any(v => v.Column == 1 && v.Row == i && v.Iduser == v.Iduser)) inrow++;
                            inrow++;
                        }

                        if (inrow == 3)
                        {
                            await Setwin(entity.Idgame, entity.Iduser);
                            isneedtocheck = false;
                          
                        }
                    }
                    if (isneedtocheck)
                    {
                        for (int i = 1; i <= Column; i++)
                        {
                            if (moves.Any(v => v.Column == i && v.Row == entity.Row && v.Iduser == v.Iduser)) break;
                            inrow++;
                        }
                        if (inrow == 3)
                        {
                            await Setwin(entity.Idgame, entity.Iduser);
                            isneedtocheck = false;
                          
                        }
                    }
                    inrow = 1;
                    if (isneedtocheck)
                    {
                        for (int i = 1; i <= Row; i++)
                        {
                            if (!moves.Any(v => v.Column == entity.Column && v.Row == i && v.Iduser == v.Iduser)) break;
                            inrow++;
                        }
                        if (inrow == 3)
                        {
                            await Setwin(entity.Idgame, entity.Iduser);
                            isneedtocheck = false;
                          
                        }

                    }

                    game.Idcurrentturn=game.Iduser1 == entity.Iduser? game.Iduser2: game.Iduser1;
                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();
                    result = (int)MoveStatuses.Success;
                }
                 

            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return result;
        }

        private async Task<bool> Setwin(int idgame, int idwinner)
        {
            Game game = null;

            int result = 0;
            try
            {

                game = _context.Games.Include(v=>v.Iduser1Navigation).Include(v=>v.Iduser2Navigation).FirstOrDefault(v => v.Id == idgame);

                game.Isfinished = true;
                game.Idwinner = idwinner;
                switch (game.Idwinner == game.Iduser1)
                {
                    case true:
                        {
                            game.Iduser1Navigation.Totalwins+=1;
                            game.Iduser2Navigation.Totallosses += 1;
                            break;
                        }
                    case false:
                        {
                            game.Iduser2Navigation.Totalwins += 1; ;
                            game.Iduser1Navigation.Totallosses += 1; ;
                            break;
                        }
                }
                game.Iduser1Navigation.Totalgames += 1; ;
                game.Iduser2Navigation.Totalgames += 1; ;

                _context.Games.Update(game);
                _context.Users.UpdateRange(game.Iduser1Navigation, game.Iduser2Navigation);
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
