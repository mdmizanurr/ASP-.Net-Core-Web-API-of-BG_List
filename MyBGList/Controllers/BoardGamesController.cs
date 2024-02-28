using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;

namespace MyBGList.Controllers
{
    [Route("api/BoardGames")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ApplicationDbContext context, ILogger<BoardGamesController> logger)
        {
            _context = context;
            _logger = logger;
        }



        //   [HttpGet(Name = "GetBoardGames")]
        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<BoardGame[]>> Get()
        {

            var query = _context.BoardGames;

            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(null, "BoardGames", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }

            };

        }


        [HttpGet("Single")]
        public IEnumerable<BoardGame> Single(int? id)
        {
            return new[]
{
                new BoardGame()
                {
                    Id = 1, Name = "Axis & Allies", Year = 1981
                }
            };
        }




    }
}
