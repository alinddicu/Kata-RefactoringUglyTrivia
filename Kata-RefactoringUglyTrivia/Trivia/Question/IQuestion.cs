namespace Trivia.Question
{
    public interface IQuestion
    {
        bool CanBeAsked(QuestionCategory category);
    }
}
