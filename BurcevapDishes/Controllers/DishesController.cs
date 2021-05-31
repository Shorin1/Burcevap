using BurcevapDishes.Data;
using BurcevapDishes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BurcevapDishes.Controllers
{
    [Produces("application/json")]
    [Route("webapi/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DishesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получить блюдо по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Dish> GetDish([FromQuery] int id)
        {
            var dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return Ok(dish);
        }

        /// <summary>
        /// Получить все блюда
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Dish[]> GetDishes(int? take = null, int? skip = null)
        {
            var dishes = _dbContext.Dishes.AsQueryable();

            if (skip.HasValue)
            {
                dishes = dishes.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                dishes = dishes.Take(take.Value);
            }

            return Ok(dishes.ToArray());
        }

        /// <summary>
        /// Создать блюдо
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateDish([FromBody] DishDto dish)
        {
            _dbContext.Dishes.Add(new Dish() 
            {
                Name = dish.Name,
                Description = dish.Description,
                Calorie = dish.Calorie,
                Weight = dish.Weight
            });

            _dbContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Изменить блюдо
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult EditDish([FromBody] Dish dish)
        {
            var existsEditedDish = _dbContext.Dishes.Any(x => x.Id == dish.Id);
            if (!existsEditedDish)
            {
                return NotFound();
            }

            _dbContext.Update(dish);
            _dbContext.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// Удалить блюдо
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteDish([FromQuery] int id)
        {
            var deletedDish = _dbContext.Dishes.FirstOrDefault(x => x.Id == id);
            if (deletedDish == null)
            {
                return NotFound();
            }

            _dbContext.Dishes.Remove(deletedDish);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
