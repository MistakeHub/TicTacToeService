using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Requests.MoveRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;
using TTTService.infrastructure;

namespace TTTService.HttpApi.Commands.Handlers.MoveRequestHandlers
{
    public class AddMoveCommandRequestHandler : IRequestHandler<AddMoveCommand, ReturnResult>
    {
        private IGameMoveRepository _repository;

        public AddMoveCommandRequestHandler(IGameMoveRepository repository) { _repository= repository; }
        public async Task<ReturnResult> Handle(AddMoveCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();


            int resultadd = await _repository.Add(new Move { Idgame = request.Idgame, Row = request.Row, Iduser = request.Iduser, Column = request.Column });

            switch (resultadd) {
                case (int)MoveStatuses.Success: {
                result.Data = $"Шаг сделан";
                result.StatuseCode = (int)Statuse.Success;
                break; 
                }
                case (int)MoveStatuses.WrongInput:
                    {
                        result.Data = $"Вы не можете идти на эту клетку";
                        result.StatuseCode = (int)Statuse.BadGateway;
                        break;
                    }
                case (int)MoveStatuses.NomoreMoves:
                    {
                        result.Data = $"Ходов больше нет";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
                case (int)MoveStatuses.NotAllowd:
                    {
                        result.Data = $"Вы не участник игры";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
                case (int)MoveStatuses.NotItTurn:
                    {
                        result.Data = $"Не ваша очередь ходить";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
                default:
                    {
                        result.Data = $"Невозможно сделать ход";
                        result.StatuseCode = (int)Statuse.Conflicted;
                        break;
                    }
            }

            return result;
        }
    }
}
