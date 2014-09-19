namespace Trivia.Question
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class QuestionStack
    {
        private readonly List<IQuestion> _questions = new List<IQuestion>();

        public QuestionStack()
        {
            for (var i = 0; i < 50; i++)
            {
                _questions.Add(new Question("Pop Question " + i, QuestionCategory.Pop));
                _questions.Add(new Question("Science Question " + i, QuestionCategory.Science));
                _questions.Add(new Question("Sports Question " + i, QuestionCategory.Sports));
                _questions.Add(new Question("Rock Question " + i, QuestionCategory.Rock));
            }
        }

        public Question Pop(QuestionCategory category)
        {
            var question = _questions.First(q => q.IsFromCategory(category));
            _questions.Remove(question);
            return (Question)question;
        }
    }
}
