namespace UglyTrivia
{
    using System;
    using Trivia.Question;
    using Trivia.Staff;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;

        private readonly QuestionStack _questionStack = new QuestionStack();

        private readonly GameAnnouncer _gameAnnouncer;
        private readonly QuestionManager _questionManager;
        private readonly GameMaster _gameMaster;
        
        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;

            _gameAnnouncer = new GameAnnouncer(_writeLine);
            _questionManager = new QuestionManager(_gameAnnouncer, _questionStack);
            _gameMaster = new GameMaster(_gameAnnouncer, _questionManager);
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
                _gameMaster.ActOnRoll(roll);
            }
            else
            {
                _questionManager.PresentNext(_gameMaster.GetCurrentPlayer(), roll);
            }
        }

        public bool wasCorrectlyAnswered()
        {
            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                _gameMaster.ManagePlayerInPenaltyBox();
            }
            else
            {
                return _gameMaster.DidPlayerWin("Answer was corrent!!!!");
            }
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
