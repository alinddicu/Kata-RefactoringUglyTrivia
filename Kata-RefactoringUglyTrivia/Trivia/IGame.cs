using System;

namespace OldUglyTrivia
{
    public interface IGame
    {
        bool AddPlayer(String playerName);
        void roll(int roll);
        bool wasCorrectlyAnswered();
        bool wrongAnswer();
    }
}