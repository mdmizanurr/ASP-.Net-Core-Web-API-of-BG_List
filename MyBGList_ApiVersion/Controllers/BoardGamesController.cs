using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO;

namespace MyBGList.Controllers
{
    [Route("api/BoardGames")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }


        //   [HttpGet(Name = "GetBoardGames")]
        [HttpGet]
        public RestDTO<BoardGame[]> GetAll()
        {

            return new RestDTO<BoardGame[]>()
            {
                Data = new BoardGame[]
                {
                    new BoardGame(){
                        Id = 1, Name = "Axis & Allies", year = 1981
                    },
                    new BoardGame()
                    {
                        Id = 2, Name = "Citadels", year = 2000
                    },
                    new BoardGame()
                    {
                        Id = 3, Name = "Terraforming Mars", year = 2016
                    }
            },
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
                    Id = 1, Name = "Axis & Allies", year = 1981
                }
            };
        }




    }
}
