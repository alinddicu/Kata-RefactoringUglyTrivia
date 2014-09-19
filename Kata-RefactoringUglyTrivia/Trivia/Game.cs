namespace UglyTrivia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OldUglyTrivia;
    using Trivia;
    using Trivia.Staff;
    using Trivia.Question;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;
        private readonly GameAnnouncer _gameAnnouncer;
        private readonly QuestionPresentor _questionPresentor;

        private readonly List<Player> _players = new List<Player>();
        private readonly QuestionStack _questionStack = new QuestionStack();

        int _currentPlayerIndex = 0;
        private Player CurrentPlayer { get { return _players[_currentPlayerIndex]; } }

        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;
            _gameAnnouncer = new GameAnnouncer(_writeLine);
            _questionPresentor = new QuestionPresentor(_gameAnnouncer, _questionStack);
        }

        public bool AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            _gameAnnouncer.PlayerWasAdded(player);
            _gameAnnouncer.PlayerPosition(_players.Count());

            return true;
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
                    CurrentPlayer.IsGettingOutOfPenaltyBox = true;

                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(CurrentPlayer, true);

                    _questionPresentor.AskNextQuestion(CurrentPlayer, roll);
                }
                else
                {
                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(CurrentPlayer, false);
                    CurrentPlayer.IsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _questionPresentor.AskNextQuestion(CurrentPlayer, roll);
            }
        }

        public bool wasCorrectlyAnswered()
        {
            if (CurrentPlayer.InPenaltyBox)
            {
                if (CurrentPlayer.IsGettingOutOfPenaltyBox)
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
