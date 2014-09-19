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
        private readonly GameAnnouncer _gameAnnouncer;

        private readonly List<Player> _players = new List<Player>();        
        private readonly QuestionStack _questionStack = new QuestionStack();

        int _currentPlayerIndex = 0;
        bool _isGettingOutOfPenaltyBox;
        private Player CurrentPlayer { get { return _players[_currentPlayerIndex]; } }

        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;
            _gameAnnouncer = new GameAnnouncer(_writeLine);
        }

        public bool AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            _gameAnnouncer.PlayerWasAdded(player);
            _gameAnnouncer.PlayerPosition(_players.Count());

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
            _gameAnnouncer.CurrentPlayer(CurrentPlayer);
            _gameAnnouncer.CurrentRoll(roll);

            if (CurrentPlayer.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(CurrentPlayer, true);
                    CurrentPlayer.Avance(roll);

                    _gameAnnouncer.CurrentPlayerLocation(CurrentPlayer);
                    _gameAnnouncer.CurrentCategory(GetCurrentCategory());
                    AskNextQuestion();
                }
                else
                {
                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(CurrentPlayer, false);
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {
                CurrentPlayer.Avance(roll);

                _gameAnnouncer.CurrentPlayerLocation(CurrentPlayer);
                _gameAnnouncer.CurrentCategory(GetCurrentCategory());
                AskNextQuestion();
            }
        }

        private void AskNextQuestion()
        {
            _questionStack.Pop(GetCurrentCategory(), WriteLine);
        }


        private QuestionCategory GetCurrentCategory()
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

        private bool PlayRound(string correctAnswerMessage)
        {
            _gameAnnouncer.CorrectAnswer(correctAnswerMessage);
            CurrentPlayer.AddGoldCoin();
            _gameAnnouncer.PlayerGoldCoins(CurrentPlayer);

            bool winner = CurrentPlayer.DidIWin();
            _currentPlayerIndex++;
            StartNewRound();

            return winner;
        }

        public bool wrongAnswer()
        {
            _gameAnnouncer.WrongAnswer();
            _gameAnnouncer.PlayerWasSentToPenaltyBox(CurrentPlayer);
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
