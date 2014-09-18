namespace UglyTrivia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using OldUglyTrivia;
    using Trivia;
    using Trivia.Question;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;
        private readonly List<Player> _players = new List<Player>();        
        private readonly QuestionStack _questionStack = new QuestionStack();

        int _currentPlayerIndex = 0;
        bool _isGettingOutOfPenaltyBox;
        private Player CurrentPlayer { get { return _players[_currentPlayerIndex]; } }

        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;
        }

        public bool AddPlayer(String playerName)
        {
            _players.Add(new Player(playerName));

            WriteLine(playerName + " was added");
            WriteLine("They are player number " + _players.Count);
            return true;
        }

        private void WriteLine(string message)
        {
            _writeLine(message);
        }

        public int howManyPlayers()
        {
            return _players.Count;
        }

        public void roll(int roll)
        {
            WriteLine(CurrentPlayer + " is the current player");
            WriteLine("They have rolled a " + roll);

            if (CurrentPlayer.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    WriteLine(CurrentPlayer + " is getting out of the penalty box");
                    CurrentPlayer.Avance(roll);

                    WriteLine(CurrentPlayer + "'s new location is " + CurrentPlayer.Place);
                    WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    WriteLine(CurrentPlayer + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                CurrentPlayer.Avance(roll);

                WriteLine(CurrentPlayer + "'s new location is " + CurrentPlayer.Place);
                WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            _questionStack.Pop(currentCategory(), WriteLine);
        }


        private QuestionCategory currentCategory()
        {
            if (CurrentPlayer.Place % 4 == 0)
            {
                return QuestionCategory.Pop;
            }

            if (CurrentPlayer.Place % 4 == 1)
            {
                return QuestionCategory.Science;
            }

            if (CurrentPlayer.Place % 4 == 2)
            {
                return QuestionCategory.Sports;
            }

            return QuestionCategory.Rock;
        }

        public bool wasCorrectlyAnswered()
        {
            if (CurrentPlayer.InPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    return PlayRound("Answer was correct!!!!");
                }
                else
                {
                    _currentPlayerIndex++;
                    StartNewRound();

                    return true;
                }
            }
            else
            {
                return PlayRound("Answer was corrent!!!!");
            }
        }

        private bool PlayRound(string startRoundMessage)
        {
            WriteLine(startRoundMessage);
            CurrentPlayer.AddGoldCoin();
            WriteLine(CurrentPlayer.AnnounceHowManyGoldCoins());

            bool winner = CurrentPlayer.DidIWin();
            _currentPlayerIndex++;
            StartNewRound();

            return winner;
        }

        public bool wrongAnswer()
        {
            WriteLine("Question was incorrectly answered");
            WriteLine(CurrentPlayer + " was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;

            _currentPlayerIndex++;
            StartNewRound();

            return true;
        }

        private void StartNewRound()
        {
            if (_currentPlayerIndex == _players.Count)
            {
                _currentPlayerIndex = 0;
            }
        }
    }
}
