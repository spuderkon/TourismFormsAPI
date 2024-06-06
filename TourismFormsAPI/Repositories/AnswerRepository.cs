using System.Linq.Expressions;
using System.Text.RegularExpressions;

using AngouriMath;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using TourismFormsAPI.Interfaces;
using TourismFormsAPI.Models;
using TourismFormsAPI.ModelsDTO;
using TourismFormsAPI.ModelsDTO.Requests;

namespace TourismFormsAPI.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly TourismContext _context;
        private readonly IFormRepository _iFormRepository;

        public AnswerRepository(TourismContext context, IFormRepository iFormRepository)
        {
            _context = context;
            _iFormRepository = iFormRepository;
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
                        Score = 0
                    };
                    _context.Answers.Add(itemToCreate);
                }
                _context.SaveChanges();
                //SetScore(body[0].SurveyId);
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
            bool answersAreFull = true;
            foreach (var item in body)
            {
                if (answersAreFull)
                {
                    answersAreFull = !item.Text.IsNullOrEmpty();
                }
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
                        Score = null,
                        QuestionId = item.QuestionId,
                    };
                    _context.Answers.Add(itemToCreate);
                }
            }
            _context.SaveChanges();
            if(answersAreFull)
                SetScore(body[1].SurveyId);
            return Task.CompletedTask;
        }
        #endregion

        #region TOOLMETHODS
        public void SetScore(int surveyId)
        {
            try
            {
                var answersBySurveyId = _context.Answers.Where(a => a.SurveyId == surveyId).ToList();

                var survey = _context.Surveys.Where(s => s.Id == surveyId).Include(s => s.Form).FirstOrDefault();
                var criterias = _context.Criteria.Where(c => c.FormId == survey.FormId).Include(c => c.Questions).ToList();
                
                foreach(var criteria in criterias)
                {
                    foreach(var question in criteria.Questions)
                    {
                        if (question.Formula != "=0" && !question.Formula.IsNullOrEmpty())
                        {
                            var targetAnswer = answersBySurveyId.FirstOrDefault(a => a.QuestionId == question.Id);
                            string[] elements = question.Formula!.Split(['*', '+', '=', '/', '-']);
                            elements = elements.Skip(1).ToArray();
                            string pattern = @"[\+\-\*\/]";
                            MatchCollection matches = Regex.Matches(question.Formula, pattern);
                            var result = new List<string>();
                            foreach (string element in elements)
                            {
                                bool isNumeric = int.TryParse(element, out int n);
                                if (element.Length >= 2 && !isNumeric)
                                { 
                                    int criteriaSequence = GetNumberFromLetter(element[0], element);
                                    var criteriaBySequence = criterias.FirstOrDefault(c => c.Sequence == criteriaSequence);
                                    int questionSequence = Convert.ToInt32(element.Substring(1));
                                    var questionBySequence = criteriaBySequence.Questions.FirstOrDefault(c => c.Sequence == questionSequence);
                                    var answerByQuestionSequence = answersBySurveyId.FirstOrDefault(a => a.QuestionId == questionBySequence.Id);
                                    result.Add(answerByQuestionSequence.Text);
                                }
                                else
                                    result.Add(element);
                            }
                            if (result.Count != 0)
                            {
                                var str = "";
                                for(int i =0; i < result.Count; i++)
                                {
                                    if(i+1 < result.Count)
                                        str += result[i].ToString() + matches[i];
                                    else
                                        str += result[i].ToString();
                                }
                            
                                Entity expr = str;
                                var resultExpr = (double)expr.EvalNumerical();
                                targetAnswer.Score = resultExpr;
                                _context.Answers.Update(targetAnswer);
                                _context.SaveChanges();
                            }
                        }
                        else if (question.Formula == "=0")
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public int GetNumberFromLetter(char letter, string check)
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
