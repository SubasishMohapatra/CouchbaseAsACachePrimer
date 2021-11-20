using AppInsights.Domain.Interfaces;
using Articles.Domain.Entities;
using Articles.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.WebAPI.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Article> GetArticles()
        {
            return _repository.GetArticles();
        }

        public Article GetArticle(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The Id should be greater than 0", nameof(id));
            }

            return _repository.GetArticle(id);
        }

        public async Task<IEnumerable<Article>> SearchArticlesByAuthorName(string authorName)
        {
            return await _repository.SearchArticlesByAuthorName(authorName);
        }
    }
};
