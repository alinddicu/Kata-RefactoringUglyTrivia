namespace Trivia.Question
{
    using System.Collections.Generic;

    public class QuestionStack : IQuestionStack
    {
        private readonly Stack<Question> _questionStack = new Stack<Question>();
        private readonly QuestionCategory _questionCategory;

        public QuestionStack(QuestionCategory questionCategory)
        {
            _questionCategory = questionCategory;
        }

        public Question Pop()
        {
            return _questionStack.Pop();
        }

        public void Push(Question question)
        {
            _questionStack.Push(question);
        }

        public bool Supports(QuestionCategory questionCategory)
        {
            return _questionCategory == questionCategory;
        }
    }
}
