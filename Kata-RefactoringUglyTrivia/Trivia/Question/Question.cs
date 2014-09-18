namespace Trivia.Question
{
    public class Question : IQuestion
    {
        private readonly string _contenu;
        private readonly QuestionCategory _category;

        public Question(string contenu, QuestionCategory category)
        {
            _contenu = contenu;
            _category = category;
        }

        public bool CanBeAsked(QuestionCategory category)
        {
            return _category == category;
        }

        public override string ToString()
        {
            return _contenu;
        }
    }
}
