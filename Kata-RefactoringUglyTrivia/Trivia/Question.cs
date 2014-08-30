namespace Trivia
{
    public class Question
    {
        private readonly string _contenu;

        public Question(string contenu)
        {
            _contenu = contenu;
        }

        public override string ToString()
        {
            return _contenu;
        }
    }
}
