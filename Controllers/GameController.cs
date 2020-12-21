﻿using ph_UserEnv.Authentication;
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
                GameSession[] games = _db.GameSessions.Where(X => (X.game_name == gameNameModel.gameName && X.status == GameSession.gameStatus.Matchmaking)).OrderBy(x => x.created_at).ToArray();
                return Ok(games);
        }
        [Route("createGame")]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameModel gameModel)
        {
            GameSession game = new GameSession()
            {
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                status = GameSession.gameStatus.Matchmaking,
                game_state = gameModel.game_state,
                creator_id = gameModel.creator_id,
                current_turn_id = gameModel.current_turn_id,
                game_name = gameModel.game_name

            };
            _db.GameSessions.Add(game);
            _db.SaveChanges();

            GamePlayer gamePlayer = new GamePlayer()
            {
                gameSession_id = _db.GameSessions.Find(game).id,
                user_id = game.creator_id,
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
                return Ok();
            }
        }
        [Route("remove")]
        [HttpPost]
        public async Task<IActionResult> remove([FromBody] GameIdModel gameIdModel)
        {
            GameSession game = _db.GameSessions.Where(x => (x.id == gameIdModel.game_id )).FirstOrDefault();

            List<GamePlayer> gamePlayers = _db.GamePlayers.Where(x => x.gameSession_id == gameIdModel.game_id).ToList();
            
            foreach(GamePlayer player in gamePlayers)
            {
                _db.GamePlayers.Remove(player);
            }
                
               
                
                _db.GameSessions.Remove(game);
                _db.SaveChanges();


                return Ok(game);
            
        }


    }
}