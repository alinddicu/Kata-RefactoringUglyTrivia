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
        private readonly GameMaster _gameMaster;

        private readonly QuestionStack _questionStack = new QuestionStack();
        
        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;
            _gameAnnouncer = new GameAnnouncer(_writeLine);
            _questionPresentor = new QuestionPresentor(_gameAnnouncer, _questionStack);
            _gameMaster = new GameMaster(_gameAnnouncer);
        }

        public bool AddPlayer(string playerName)
        {
            _gameMaster.AddPlayer(playerName);

            return true;
        }

        public int howManyPlayers()
        {
            return _gameMaster.CountPlayers();
        }

        public void roll(int roll)
        {
            _gameAnnouncer.CurrentPlayer(_gameMaster.GetCurrentPlayer());
            _gameAnnouncer.CurrentRoll(roll);

            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _gameMaster.GetCurrentPlayer().IsGettingOutOfPenaltyBox = true;
                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(_gameMaster.GetCurrentPlayer(), true);
                    _questionPresentor.AskNextQuestion(_gameMaster.GetCurrentPlayer(), roll);
                }
                else
                {
                    _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(_gameMaster.GetCurrentPlayer(), false);
                    _gameMaster.GetCurrentPlayer().IsGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _questionPresentor.AskNextQuestion(_gameMaster.GetCurrentPlayer(), roll);
            }
        }

        public bool wasCorrectlyAnswered()
        {
            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                if (_gameMaster.GetCurrentPlayer().IsGettingOutOfPenaltyBox)
                {
                    return PlayRound("Answer was correct!!!!");
                }
                else
                {
                    _gameMaster.SetNextPlayer();
                    _gameMaster.StartNewRound();

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
            _gameMaster.GetCurrentPlayer().AddGoldCoin();
            _gameAnnouncer.PlayerGoldCoins(_gameMaster.GetCurrentPlayer());

            bool winner = _gameMaster.GetCurrentPlayer().DidIWin();
            _gameMaster.SetNextPlayer();
            _gameMaster.StartNewRound();

            return winner;
        }

        public bool wrongAnswer()
        {
            _gameAnnouncer.WrongAnswer();
            _gameAnnouncer.PlayerWasSentToPenaltyBox(_gameMaster.GetCurrentPlayer());
            _gameMaster.GetCurrentPlayer().InPenaltyBox = true;

            _gameMaster.SetNextPlayer();
            _gameMaster.StartNewRound();

            return true;
        }
    }
}
