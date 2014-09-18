namespace Trivia.Question
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class QuestionStack
    {
        private readonly LinkedList<IQuestion> _questions = new LinkedList<IQuestion>();

        public QuestionStack()
        {
            for (var i = 0; i < 50; i++)
            {
                _questions.AddLast(new Question("Pop Question " + i, QuestionCategory.Pop));
                _questions.AddLast(new Question("Science Question " + i, QuestionCategory.Science));
                _questions.AddLast(new Question("Sports Question " + i, QuestionCategory.Sports));
                _questions.AddLast(new Question("Rock Question " + i, QuestionCategory.Rock));
            }
        }

        public void Pop(QuestionCategory category, Action<string> asker)
        {
            var question = _questions.First(q => q.CanBeAsked(category));
            asker(question.ToString());
            _questions.Remove(question);
        }
    }
}
