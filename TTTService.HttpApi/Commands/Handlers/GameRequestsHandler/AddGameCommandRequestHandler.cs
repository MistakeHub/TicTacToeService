using MediatR;
using TTTService.Domain.Entities;
using TTTService.Domain.Interfaces;
using TTTService.HttpApi.Commands.Requests.GameRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.Statuses;

namespace TTTService.HttpApi.Commands.Handlers.GameRequestsHandler
{
    public class AddGameCommandRequestHandler : IRequestHandler<AddGameCommand, ReturnResult>
    {
        private IBaseRepository<Game> _repository;

        public AddGameCommandRequestHandler(IBaseRepository<Game> repository) { _repository= repository; }
        public async Task<ReturnResult> Handle(AddGameCommand request, CancellationToken cancellationToken)
        {
            ReturnResult result = new ReturnResult();
            Random rand = new Random();
            int[] userids = new int[] { request.Iduser1, request.Iduser2 };
                int resultadd = await _repository.Add(new Game { Iduser1 = request.Iduser1, Iduser2 = request.Iduser2, Idcurrentturn=userids[ rand.Next(0,2)]  });

                if (resultadd==1)
                {
                    result.Data = $"Игра началась";
                    result.StatuseCode = (int)Statuse.Success;
                }
            else
            {
                result.Data = $"Невозможно добавить лобби";
                result.StatuseCode = (int)Statuse.Conflicted;
            }

            return result;
        }
    }
}
