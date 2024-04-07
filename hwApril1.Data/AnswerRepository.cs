using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hwApril1.Data
{
    public class AnswerRepository
    {
        private string _connectionString;
        public AnswerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddAnswer(Answer answer)
        {
            using var context = new QADataContext(_connectionString);
            context.Add(answer);
            context.SaveChanges();
        }
    }
}
