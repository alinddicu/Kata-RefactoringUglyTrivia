namespace Trivia.Actors
{
    using Trivia.Question;

    public class QuestionManager
    {
        private readonly Announcer _announcer;
        private readonly QuestionDeck _questionDeck = new QuestionDeck();
        private readonly CategorySelector _categorySelector = new CategorySelector();

        public QuestionManager(Announcer gameAnnouncer)
        {
            _announcer = gameAnnouncer;
        }

        public void GetNextQuestion(Player currentPlayer, int roll)
        {
            currentPlayer.MoveForward(roll);
            _announcer.CurrentPlayerLocation(currentPlayer);
            _announcer.CurrentCategory(GetCurrentCategory(currentPlayer));
            NextQuestion(currentPlayer);
        }

        private void NextQuestion(Player currentPlayer)
        {
            _announcer.PresentQuestion(_questionDeck.Pop(GetCurrentCategory(currentPlayer)));
        }

        private QuestionCategory GetCurrentCategory(Player currentPlayer)
        {
            return _categorySelector.Select(currentPlayer.Place);
        }
    }
}
