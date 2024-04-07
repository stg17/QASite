using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwApril1.Data
{
    public class TagRepository
    {
        private string _connectionString;
        public TagRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddTags(List<string> tags, int questionId)
        {
            using var context = new QADataContext(_connectionString);
            foreach (var name in tags)
            {
                var tag = context.Tags.FirstOrDefault(t => t.Name == name);
                if (tag == null)
                {
                    tag = new Tag()
                    {
                        Name = name
                    };
                    context.Tags.Add(tag);
                    context.SaveChanges();
                }

                context.QuestionTags.Add(new()
                {
                    TagId = tag.Id,
                    QuestionId = questionId
                });

                context.SaveChanges();
            }
        }

        public List<Question> GetQuestionsForTag(string tagName)
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions.Where(q => q.QuestionTags.Any(qt => qt.Tag.Name == tagName))
                .Include(q => q.QuestionTags).ThenInclude(q => q.Tag).ToList();
        }
    }
}
