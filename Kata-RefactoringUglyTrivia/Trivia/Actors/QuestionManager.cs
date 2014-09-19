namespace Trivia.Actors
{
    using Trivia.Question;

    public class QuestionManager
    {
        private readonly Announcer _announcer;
        private readonly QuestionStack _questionStack = new QuestionStack();
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
            _announcer.Announce(_questionStack.Pop(GetCurrentCategory(currentPlayer)).ToString());
        }

        private QuestionCategory GetCurrentCategory(Player currentPlayer)
        {
            return _categorySelector.Select(currentPlayer.Place);
        }
    }
}
