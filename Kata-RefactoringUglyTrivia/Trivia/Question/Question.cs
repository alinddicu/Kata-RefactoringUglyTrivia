namespace Trivia.Question
{
    using System.Globalization;

    public class Question
    {
        private const string ContentFormat = "{0} Question {1}";
        private readonly string _content;

        public Question(string content)
        {
            _content = content;
        }

        public Question(QuestionCategory questionCategory, int i)
        {
            _content = string.Format(CultureInfo.CurrentCulture, ContentFormat, questionCategory, i);
        }

        public override string ToString()
        {
            return _content;
        }
    }
}
