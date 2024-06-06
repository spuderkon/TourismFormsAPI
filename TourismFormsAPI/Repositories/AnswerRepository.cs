using System.Linq.Expressions;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TourismContext _context;

        public AnswerRepository(TourismContext context)
        {
            _context = context;
        }

        #region POST
        public Task SaveMyAll(AnswerPost[] body)
        {
            try
            {
                foreach (var item in body)
                {
                    var itemToCreate = new Answer()
                    {
                        SurveyId = item.SurveyId,
                        Text = item.Text,
                        QuestionId = item.QuestionId,
                        Score = GetScore(item)
                    };
                    _context.Answers.Add(itemToCreate);
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PUT
        public Task UpdateMyAll(AnswerPut[] body)
        {
            foreach (var item in body)
            {
                var itemToUpdate = _context.Answers.FirstOrDefault(a => a.SurveyId == item.SurveyId && a.QuestionId == item.QuestionId);
                if (itemToUpdate is not null) 
                {
                    itemToUpdate.Text = item.Text;
                    _context.Answers.Update(itemToUpdate);
                }
                else
                {
                    var itemToCreate = new Answer()
                    {
                        SurveyId = item.SurveyId,
                        Text = item.Text,
                        Score = 5,
                        QuestionId = item.QuestionId,
                    };
                    _context.Answers.Add(itemToCreate);
                }
            }
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        #endregion

        #region TOOLMETHODS
        public int GetScore(AnswerPost body)
        {
            try
            {
                var question = _context.Questions.FirstOrDefault(q => q.Id == body.QuestionId);
                if(question is not null)
                {
                    if (question.Formula == "=0")
                        return 0;


                    var questions = _context.Questions.Where(q => q.CriteriaId == question.CriteriaId).ToList();

                    string[] elements = question.Formula!.Split(['*', '+', '=', '/', '-']);
                    var result = 0.0;
                    foreach (string element in elements)
                    {
                        if(element.Length > 2)
                        {
                            var number = questions.FirstOrDefault(q => q.Sequence == GetNumberFromLetter(element[0]));
                        }    
                    }
                }
                /*double result = 0;
                if (question is not null)
                {
                    if (question.Formula == "=0")
                    {
                        return 0;
                    }
                    
                    var questions = _context.Questions.Where(q => q.CriteriaId == question.CriteriaId).ToList();

                    string[] elements = question.Formula.Split(['*', '+', '=', '/', '-']);
                    foreach (var element in elements) 
                    {
                        if(element.Length > 2)
                        {
                            var number = questions.FirstOrDefault(q => q.Sequence == GetNumberFromLetter(element[0]));
                        }
                        else
                        {
                            var number = questions.FirstOrDefault(q => q.Sequence == GetNumberFromLetter(element));
                        }
    
                    }
                }*/
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return 0;
        }
        public int GetNumberFromLetter(char letter)
        {
            // Проверяем, является ли символ кириллической буквой
            if (!char.IsLetter(letter) || !char.IsUpper(letter))
            {
                throw new ArgumentException("Пожалуйста, введите заглавную кириллическую букву.");
            }

            // Получаем код символа в кодировке UTF-16
            int unicodeValue = (int)letter;

            // Для русского алфавита коды букв начинаются с 1040 (А) и заканчиваются 1071 (Я)
            // Первая буква будет соответствовать 1, вторая - 2 и так далее
            int number = unicodeValue - 1039;

            return number;
        }
        #endregion
    }
}
