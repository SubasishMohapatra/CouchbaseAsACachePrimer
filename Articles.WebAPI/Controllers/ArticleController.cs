using Articles.Domain.Entities;
using Articles.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.WebAPI.Controllers
{
    [ArticlesLog]
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticleController(IArticleService service)
        {
            _service = service;
        }

        [ArticlesLog]
        [HttpGet]
        public IEnumerable<Article> GetAllArticles()
        {
            return _service.GetArticles();
        }

        [Route("{id}")]
        [HttpGet]
        public Article GetArticle(int id)
        {
            return _service.GetArticle(id);
        }

        [Route("SearchArticlesByAuthorName")]
        [HttpGet]
        public async Task<IEnumerable<Article>> SearchArticlesByAuthorName(string authorName)
        {
            await Task.Delay(3000);
            var results= await _service.SearchArticlesByAuthorName(authorName);
            return results;
        }
    }
}
