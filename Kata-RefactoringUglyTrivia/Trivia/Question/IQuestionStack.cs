namespace Trivia.Question
{
    using System;

    public interface IQuestionStack
    {
        Question Pop();
        void Push(Question question);
        bool Supports(QuestionCategory questionCategory);
    }
}
