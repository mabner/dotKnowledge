using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ArticleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _dbContext.Articles.ToListAsync();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            _dbContext.Articles.Add(article);
            await _dbContext.SaveChangesAsync();
            return Ok(article);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Article article)
        {
            if (id != article.Id)
                return BadRequest();

            _dbContext.Entry(article).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _dbContext.Articles.FindAsync(id);
            if (article == null)
                return NotFound();

            _dbContext.Articles.Remove(article);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
