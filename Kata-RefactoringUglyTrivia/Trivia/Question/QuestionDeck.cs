namespace Trivia.Question
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class QuestionDeck
    {
        private readonly List<IQuestionStack> _questionStacks = new List<IQuestionStack>();

        public QuestionDeck()
        {
            var questionCategories = Enum.GetValues(typeof(QuestionCategory));

            foreach (QuestionCategory questionCategory in questionCategories)
            {
                var questionStack = new QuestionStack(questionCategory);
                for (var i = 49; i >= 0; i--)
                {
                    questionStack.Push(new Question(questionCategory, i));
                }

                _questionStacks.Add(questionStack);
            }
        }

        public Question Pop(QuestionCategory category)
        {
            return _questionStacks.First(q => q.Supports(category)).Pop();
        }
    }
}
