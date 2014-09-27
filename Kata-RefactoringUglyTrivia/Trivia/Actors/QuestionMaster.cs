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
            var currentCategory = _categorySelector.GetCurrentCategory(currentPlayer);
            announcer.CurrentCategory(currentCategory);
            announcer.PresentQuestion(_questionDeck.Pop(currentCategory));
        }
    }
}
