using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TTTService.Domain.Entities;
using TTTService.HttpApi.Commands.Queries.GameLobbiesQueries;
using TTTService.HttpApi.Commands.Queries.GameQueries;
using TTTService.HttpApi.Commands.Queries.MoveQueries;
using TTTService.HttpApi.Commands.Requests.GameLobbyRequests;
using TTTService.HttpApi.Commands.Requests.GameRequests;
using TTTService.HttpApi.Commands.Requests.MoveRequests;
using TTTService.HttpApi.Commands.Requests.UserRequests;
using TTTService.HttpApi.Helpers;
using TTTService.HttpApi.Helpers.JWThelper;
using TTTService.HttpApi.Helpers.Statuses;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TTTService.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
       
        private IMediator _mediator;
       

        public GameController(IMediator mediator) { _mediator = mediator;}
        [Authorize]
        [HttpGet("getalllobbies")]
        public IActionResult GetAllLobbies()
        {
            var command = _mediator.Send(new GetAllGameLobbiesCommand()).Result;
            return new ObjectResult(command.Data) { StatusCode =command.StatuseCode };
        }
        [Authorize]
        [HttpGet("getmovesbyid/{idgame}")]
        public IActionResult GetMovesById(int idgame)
        {
            var command = _mediator.Send(new GetMovesByIdCommand { Idgame = idgame }).Result;
            return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
        }
        [Authorize]
        [HttpGet("getallgames")]
        public IActionResult GetAllGames()
        {
            var command = _mediator.Send(new GetAllGamesCommand()).Result;
            return new ObjectResult(command.Data) { StatusCode=command.StatuseCode} ;
        }

        
        [Authorize]
        [HttpGet("get/{id}")]
        public IActionResult GetById(int id)
        {
            var command = _mediator.Send(new GetGameByIdCommand() { Id=id}).Result;
            return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
        }

        
        [Authorize]
        [HttpPost("join")]
        public IActionResult JoinToLobby(string title,int iduser, int idhost, string password = null)
        {
            ReturnResult command= _mediator.Send(new JoinToLobbyCommand() { Title=title,Password=password }).Result;
            ReturnResult command2=new ReturnResult();
            if (command.StatuseCode == 200)
            {
              command2 = _mediator.Send(new AddGameCommand() { Iduser1 = idhost, Iduser2 = iduser }).Result;
            }


            return new ObjectResult($"{command.Data}; {command2.Data}") { StatusCode = command.StatuseCode };
        }
        [Authorize]
        [HttpPost("createlobby")]
        public IActionResult CreateLobby(string title, int idhost, string? password = null)
        {
            var command = _mediator.Send(new AddGameLobbyCommand() { Title = title, Password = password, Idhost = idhost }).Result;
           
            return new ObjectResult(command.Data) { StatusCode=command.StatuseCode};
        }
        [HttpPost("register")]
        public IActionResult CreateLobby( string login, string password)
        {
            var command = _mediator.Send(new AddUserCommand() { Password = password, Login=login}).Result;

            return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
        }

        [Authorize]
        [HttpDelete("{title}")]
        public IActionResult Delete(string title, int iduser)
        {
            var command = _mediator.Send(new RemoveGameLobbyCommand() { Title = title, Iduser=iduser }).Result;
            return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
        }
        [HttpGet("login")]
        public IActionResult login(string login ,string password)
        {
          
            var command = _mediator.Send(new LoginUserCommand() { Login = login, Password = password }).Result;
            if (command.StatuseCode != 200) return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
            var user = GetIdentity(login, password);
          
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOption.ISSUER,
                    audience: AuthOption.AUDIENCE,
                    notBefore: now,
                    claims: user.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOption.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOption.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                login = user.Name,
                id = command.Data
            };
            return new ObjectResult(response) { StatusCode = (int)Statuse.Success };
        }
        [Authorize]
        [HttpPost("setmove/{idgame}")]
        public IActionResult Setmove(int idgame,int iduser,int row, int column)
        {
            
            var command = _mediator.Send(new AddMoveCommand() { Row=row, Column=column, Idgame=idgame, Iduser=iduser }).Result;
            
            return new ObjectResult(command.Data) { StatusCode = command.StatuseCode };
        }
        private ClaimsIdentity GetIdentity(string login, string password)
        {

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,null
                    );
                return claimsIdentity;


        }
    }
}
