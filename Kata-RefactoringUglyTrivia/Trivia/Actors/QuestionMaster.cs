namespace Trivia.Actors
{
    using Trivia.Question;

    public class QuestionMaster
    {
        private readonly QuestionDeck _questionDeck = new QuestionDeck();
        private readonly CategorySelector _categorySelector = new CategorySelector();

        public void GetNextQuestion(Announcer announcer, Player currentPlayer, int roll)
        {
            currentPlayer.MoveForward(roll);
            announcer.CurrentPlayerLocation(currentPlayer);
            announcer.CurrentCategory(GetCurrentCategory(currentPlayer));
            NextQuestion(announcer, currentPlayer);
        }

        private void NextQuestion(Announcer announcer, Player currentPlayer)
        {
            announcer.PresentQuestion(_questionDeck.Pop(GetCurrentCategory(currentPlayer)));
        }

        private QuestionCategory GetCurrentCategory(Player currentPlayer)
        {
            return _categorySelector.Select(currentPlayer.Place);
        }
    }
}
