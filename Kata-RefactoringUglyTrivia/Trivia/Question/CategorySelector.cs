namespace Trivia.Question
{
    using System;
    using System.Collections.Generic;
    using Trivia.Actors;

    public class CategorySelector
    {
        public QuestionCategory GetCurrentCategory(Player currentPlayer)
        {
            return (QuestionCategory)(currentPlayer.Place % 4);
        }
    }
}
