using ph_UserEnv.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ph_UserEnv.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Newtonsoft.Json;

namespace ph_UserEnv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public GameController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext db)
        {
            _db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [Route("getGamesByName")]
        [HttpPost]
        public async Task<IActionResult> GetGamesByName([FromBody] GameNameModel gameNameModel)
        {
                GameSession[] games = _db.GameSessions.Where(X => (X.game_name == gameNameModel.game_name && X.status == GameSession.gameStatus.Matchmaking)).OrderBy(x => x.created_at).ToArray();

            List<CleanGame> cleanGames = new List<CleanGame>();
            foreach (GameSession c in games)
            {
                if((DateTime.Now - c.created_at).Seconds < 15)
                {
                    cleanGames.Add(new CleanGame { created_at = c.created_at, creator_id = c.creator_id, game_state = c.game_state, creator_username = _db.Users.Where(x => x.Id == c.creator_id).FirstOrDefault().UserName, current_turn_id = c.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == c.current_turn_id).FirstOrDefault().UserName, game_name = c.game_name, status = c.status, updated_at = c.updated_at, id = c.id });

                }
                else
                {
                    _db.GamePlayers.RemoveRange(_db.GamePlayers.Where(x => x.gameSession_id == c.id).ToArray());
                    _db.GameSessions.Remove(c);
                    _db.SaveChanges();
                }
            }



            return Ok(cleanGames);
        }
        [Route("createGame")]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameModel gameModel)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);


                GameSession game = new GameSession()
            {
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = GameSession.gameStatus.Matchmaking,
                game_state = JsonConvert.SerializeObject((new string[,] { {"", "", "" }, { "", "", "" }, { "", "", "" } })),
                creator_id = claim,
                current_turn_id = claim,
                game_name = gameModel.game_name

            };
            _db.GameSessions.Add(game);
            _db.SaveChanges();

            GamePlayer gamePlayer = new GamePlayer()
            {
                gameSession_id = _db.GameSessions.Where(x => x == game).FirstOrDefault().id,
                user_id = claim,
                created_at = game.updated_at,
                updated_at = game.updated_at
            };
            _db.GamePlayers.Add(gamePlayer);
            _db.SaveChanges();


            return Ok(game);
        }
        [Route("joinGame")]
        [HttpPost]
        public async Task<IActionResult> JoinGame([FromBody] GameIdModel gameIdModel)
        {
            GameSession game = _db.GameSessions.Where(x => (x.id == gameIdModel.game_id && x.status == GameSession.gameStatus.Matchmaking)).FirstOrDefault();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;


                var user = await userManager.FindByIdAsync(claim);
                GamePlayer gamePlayer = new GamePlayer()
                {
                    gameSession_id = game.id,
                    user_id = claim,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                game.status = GameSession.gameStatus.in_progress;
                game.updated_at = DateTime.Now;
                _db.GamePlayers.Add(gamePlayer);
                _db.GameSessions.Update(game);
                _db.SaveChanges();


                return Ok(game);
            }
            else
            {
                return Ok("failure");
            }
        }
        [AllowAnonymous]
        [Route("remove")]
        [HttpPost]
        public async Task<IActionResult> remove([FromBody] GameIdModel gameIdModel)
        {
            GameSession game = _db.GameSessions.Where(x => (x.id == gameIdModel.game_id )).FirstOrDefault();
            if (game.status != GameSession.gameStatus.completed)
            {
                List<GamePlayer> gamePlayers = _db.GamePlayers.Where(x => x.gameSession_id == gameIdModel.game_id).ToList();

                foreach (GamePlayer player in gamePlayers)
                {
                    _db.GamePlayers.Remove(player);
                }



                _db.GameSessions.Remove(game);
                _db.SaveChanges();

            }
                return Ok(game);
            
        }
        [Route("checkMoveTTT")]
        [HttpPost]
        public async Task<IActionResult> CheckMoveTTT([FromBody] GameStateIdModel gameStateIdModel)
        {
            GameSession pastGame = _db.GameSessions.Where(X => (X.id == gameStateIdModel.game_id )).FirstOrDefault();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            
                IEnumerable<Claim> claims = identity.Claims;
                // or
                string claim = identity.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<GamePlayer> players = _db.GamePlayers.Where(x => x.gameSession_id == gameStateIdModel.game_id).ToList();
                var user = await userManager.FindByIdAsync(claim);
            string[,] pastGameState = JsonConvert.DeserializeObject<string[,]>(pastGame.game_state);
            //string[,] purposedGameState = JsonConvert.DeserializeObject<string[,]>(gameStateIdModel.game_state);

            if (pastGame.status != GameSession.gameStatus.in_progress || pastGame.current_turn_id != claim || pastGame.updated_at != gameStateIdModel.update_at)
            {
                CleanGame sameGame = new CleanGame { created_at = pastGame.created_at, creator_id = pastGame.creator_id, game_state = pastGame.game_state, creator_username = _db.Users.Where(x => x.Id == pastGame.creator_id).FirstOrDefault().UserName, current_turn_id = pastGame.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == pastGame.current_turn_id).FirstOrDefault().UserName, game_name = pastGame.game_name, status = pastGame.status, updated_at = pastGame.updated_at, id = pastGame.id };

                return

                Ok(new
                {
                    game = sameGame,
                    error = "returned early",
                    
                });
            }
            if(TTTGame.MarkBoard(gameStateIdModel.row,gameStateIdModel.col,((claim == pastGame.creator_id)? "X": "O"),pastGameState))
            {
                pastGameState[gameStateIdModel.row, gameStateIdModel.col] = ((claim == pastGame.creator_id) ? "X" : "O");
            }
            CleanGame cleanGame;
            if(TTTGame.CheckForWinner(pastGameState))
            {
                pastGame.winner_id = pastGame.current_turn_id;
                pastGame.status = GameSession.gameStatus.completed;
                pastGame.updated_at = DateTime.Now;
                pastGame.game_state = JsonConvert.SerializeObject(pastGameState);
                cleanGame = new CleanGame {winner_username = _db.Users.Where(x => x.Id == pastGame.winner_id).FirstOrDefault().UserName, winner_id = pastGame.winner_id, created_at = pastGame.created_at, creator_id = pastGame.creator_id, game_state = pastGame.game_state, creator_username = _db.Users.Where(x => x.Id == pastGame.creator_id).FirstOrDefault().UserName, current_turn_id = pastGame.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == pastGame.current_turn_id).FirstOrDefault().UserName, game_name = pastGame.game_name, status = pastGame.status, updated_at = pastGame.updated_at, id = pastGame.id };

            }
            else
            {
                pastGame.current_turn_id = pastGame.current_turn_id == players[0].user_id? players[1].user_id : players[0].user_id;
                pastGame.updated_at = DateTime.Now;
                pastGame.game_state = JsonConvert.SerializeObject(pastGameState);
                cleanGame = new CleanGame { created_at = pastGame.created_at, creator_id = pastGame.creator_id, game_state = pastGame.game_state, creator_username = _db.Users.Where(x => x.Id == pastGame.creator_id).FirstOrDefault().UserName, current_turn_id = pastGame.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == pastGame.current_turn_id).FirstOrDefault().UserName, game_name = pastGame.game_name, status = pastGame.status, updated_at = pastGame.updated_at, id = pastGame.id };
            }

            _db.GameSessions.Update(pastGame);
            _db.SaveChanges();





            



            return Ok(new
            {
                game = cleanGame,
                error = "none",

            }); ;
        }
        [Route("getGame")]
        [HttpPost]
        public async Task<IActionResult> GetGame([FromBody] IdModel idModel)
        {
            GameSession pastGame = _db.GameSessions.Where(X => (X.id == idModel.id)).FirstOrDefault();
            if (pastGame != null)
            {


                CleanGame cleanGame;
                if (pastGame.winner_id != null)
                {

                    cleanGame = new CleanGame { winner_username = _db.Users.Where(x => x.Id == pastGame.winner_id).FirstOrDefault().UserName, winner_id = pastGame.winner_id, created_at = pastGame.created_at, creator_id = pastGame.creator_id, game_state = pastGame.game_state, creator_username = _db.Users.Where(x => x.Id == pastGame.creator_id).FirstOrDefault().UserName, current_turn_id = pastGame.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == pastGame.current_turn_id).FirstOrDefault().UserName, game_name = pastGame.game_name, status = pastGame.status, updated_at = pastGame.updated_at, id = pastGame.id };

                }
                else
                {
                    cleanGame = new CleanGame { created_at = pastGame.created_at, creator_id = pastGame.creator_id, game_state = pastGame.game_state, creator_username = _db.Users.Where(x => x.Id == pastGame.creator_id).FirstOrDefault().UserName, current_turn_id = pastGame.current_turn_id, current_turn_username = _db.Users.Where(x => x.Id == pastGame.current_turn_id).FirstOrDefault().UserName, game_name = pastGame.game_name, status = pastGame.status, updated_at = pastGame.updated_at, id = pastGame.id };
                }

                return Ok(cleanGame);
            }
            else
            {
                return Ok(new { });
            }
        }



    }
}
