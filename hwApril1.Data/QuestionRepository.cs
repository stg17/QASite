using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwApril1.Data
{
    public class QuestionRepository
    {
        private string _connectionString;
        public QuestionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Question> GetQuestions()
        {
            using var context = new QADataContext(_connectionString);
            return context.Questions.Include(q => q.User).Include(q => q.Answers).Include(q => q.QuestionTags).ThenInclude(qt => qt.Tag).ToList();
        }

        public void AddQuestion(Question question, List<string> tags)
        {
            using var context = new QADataContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();
            var tagRepo = new TagRepository(_connectionString);
            tagRepo.AddTags(tags, question.Id);
        }

        public Question GetCompleteQuestionById(int id)
        {
            using var context = new QADataContext(_connectionString);
            var question = context.Questions.FirstOrDefault(q => q.Id == id);
            if (question == null)
            {
                return null;
            }
            return context.Questions.Include(q => q.User).Include(q => q.Answers).ThenInclude(a => a.User).Include(q => q.QuestionTags).ThenInclude(q => q.Tag).First(q => q.Id == id);
        }
    }
}
