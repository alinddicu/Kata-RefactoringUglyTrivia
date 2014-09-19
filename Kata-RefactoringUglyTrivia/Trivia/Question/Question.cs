namespace Trivia.Question
{
    public class Question
    {
        private readonly string _contenu;

        public Question(string contenu, QuestionCategory category)
        {
            _contenu = contenu;
            Category = category;
        }

        public QuestionCategory Category { get; private set; }

        public override string ToString()
        {
            return _contenu;
        }
    }
}
