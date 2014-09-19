namespace UglyTrivia
{
    using System;

    public interface IGame
    {
        bool AddPlayer(String playerName);
        void roll(int roll);
        bool wasCorrectlyAnswered();
        bool wrongAnswer();
    }
}