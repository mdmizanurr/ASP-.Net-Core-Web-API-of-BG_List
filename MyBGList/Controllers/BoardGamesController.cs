using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.DTO;
using MyBGList.Models;
using System.Linq.Dynamic.Core;

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
        public async Task<RestDTO<BoardGame[]>> Get(int pageIndex = 0, int pageSize = 10, string? sortColumn = "Name",
            string? sortOrder = "ASC", string? filterQuery = null)
        {
            var query = _context.BoardGames.AsQueryable();

            if (!string.IsNullOrEmpty(filterQuery))
                query = query.Where(i => i.Name.Contains(filterQuery.Trim()));
            var recordCount = await query.CountAsync();

            query = _context.BoardGames.OrderBy($"{sortColumn} {sortOrder}")
               .Skip(pageIndex * pageSize).Take(pageSize);

            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = recordCount,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(Url.Action(null, "BoardGames",
                        new {pageIndex, pageSize},
                              Request.Scheme)!,
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
