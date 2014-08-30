using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OldUglyTrivia;
using Trivia;

namespace UglyTrivia
{
    public class Game : IGame
    {
        private readonly Action<string> _writeLine;

        readonly List<Player> _players = new List<Player>();
        
        readonly bool[] _inPenaltyBox = new bool[6];

        private readonly LinkedList<Question> _popQuestions = new LinkedList<Question>();
        private readonly LinkedList<Question> _scienceQuestions = new LinkedList<Question>();
        private readonly LinkedList<Question> _sportsQuestions = new LinkedList<Question>();
        private readonly LinkedList<Question> _rockQuestions = new LinkedList<Question>();

        int _currentPlayerIndex = 0;
        bool _isGettingOutOfPenaltyBox;
        private Player CurrentPlayer { get { return _players[_currentPlayerIndex]; } }

        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast(new Question("Pop Question " + i));
                _scienceQuestions.AddLast(new Question("Science Question " + i));
                _sportsQuestions.AddLast(new Question("Sports Question " + i));
                _rockQuestions.AddLast(new Question("Rock Question " + i));
            }
        }

        public bool AddPlayer(String playerName)
        {
            _players.Add(new Player(playerName));
            _inPenaltyBox[howManyPlayers()] = false;

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

            if (_inPenaltyBox[_currentPlayerIndex])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    WriteLine(CurrentPlayer + " is getting out of the penalty box");
                    CurrentPlayer.Avance(roll);

                    WriteLine(CurrentPlayer
                            + "'s new location is "
                            + CurrentPlayer.Place);
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

                WriteLine(CurrentPlayer
                        + "'s new location is "
                        + CurrentPlayer.Place);
                WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                WriteLine(_popQuestions.First().ToString());
                _popQuestions.RemoveFirst();
            }

            if (currentCategory() == "Science")
            {
                WriteLine(_scienceQuestions.First().ToString());
                _scienceQuestions.RemoveFirst();
            }

            if (currentCategory() == "Sports")
            {
                WriteLine(_sportsQuestions.First().ToString());
                _sportsQuestions.RemoveFirst();
            }

            if (currentCategory() == "Rock")
            {
                WriteLine(_rockQuestions.First().ToString());
                _rockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            var place = CurrentPlayer.Place;
            switch (place)
            {
                case 0:
                case 4:
                case 8:
                    return "Pop";

                case 1:
                case 5:
                case 9:
                    return "Science";

                case 2:
                case 6:
                case 10:
                    return "Sports";

                default:
                    return "Rock";
            }
        }

        public bool wasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayerIndex])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    WriteLine("Answer was correct!!!!");
                    CurrentPlayer.AddGoldCoin();
                    WriteLine(CurrentPlayer.GetLabelGoldCoin());

                    bool winner = CurrentPlayer.DidIWin();
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;

                    return winner;
                }
                else
                {
                    _currentPlayerIndex++;
                    if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
                    return true;
                }
            }
            else
            {
                WriteLine("Answer was corrent!!!!");
                CurrentPlayer.AddGoldCoin();
                WriteLine(CurrentPlayer.GetLabelGoldCoin());

                bool winner = CurrentPlayer.DidIWin();
                _currentPlayerIndex++;
                if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;

                return winner;
            }
        }

        public bool wrongAnswer()
        {
            WriteLine("Question was incorrectly answered");
            WriteLine(CurrentPlayer + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayerIndex] = true;

            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count) _currentPlayerIndex = 0;
            return true;
        }
    }
}
