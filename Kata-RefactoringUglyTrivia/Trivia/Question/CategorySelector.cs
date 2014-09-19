namespace Trivia.Question
{
    using System;
    using System.Collections.Generic;

    public class CategorySelector
    {
        public QuestionCategory Select(int playerPosition)
        {
            return (QuestionCategory)(playerPosition % 4);
        }
    }
}
