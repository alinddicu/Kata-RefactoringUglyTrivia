namespace Trivia.Staff
{
    using Trivia.Question;

    public class QuestionPresentor
    {
        private readonly GameAnnouncer _gameAnnouncer;
        private readonly QuestionStack _questionStack;
        private readonly CategorySelector _categorySelector = new CategorySelector();

        public QuestionPresentor(GameAnnouncer gameAnnouncer, QuestionStack questionStack)
        {
            _gameAnnouncer = gameAnnouncer;
            _questionStack = questionStack;
        }

        public void AskNextQuestion(Player currentPlayer, int roll)
        {
            currentPlayer.MoveForward(roll);
            _gameAnnouncer.CurrentPlayerLocation(currentPlayer);
            _gameAnnouncer.CurrentCategory(GetCurrentCategory(currentPlayer));
            NextQuestion(currentPlayer);
        }

        private void NextQuestion(Player currentPlayer)
        {
            _questionStack.Pop(GetCurrentCategory(currentPlayer), _gameAnnouncer.Announce);
        }

        private QuestionCategory GetCurrentCategory(Player currentPlayer)
        {
            return _categorySelector.Select(currentPlayer.Place);
        }
    }
}
