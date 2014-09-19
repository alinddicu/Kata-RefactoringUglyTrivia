namespace UglyTrivia
{
    using System;
    using Trivia.Actors;
    using Trivia.Question;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;

        private readonly Announcer _announcer;
        private readonly QuestionManager _questionManager;
        private readonly GameMaster _gameMaster;
        
        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;

            _announcer = new Announcer(_writeLine);
            _questionManager = new QuestionManager(_announcer);
            _gameMaster = new GameMaster(_announcer, _questionManager);
        }

        public bool AddPlayer(string playerName)
        {
            _gameMaster.AddPlayer(playerName);

            return true;
        }

        public void roll(int roll)
        {
            _announcer.CurrentPlayer(_gameMaster.GetCurrentPlayer());
            _announcer.CurrentRoll(roll);

            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                _gameMaster.PlayOnRoll(roll);
            }
            else
            {
                _questionManager.GetNextQuestion(_gameMaster.GetCurrentPlayer(), roll);
            }
        }

        public bool wasCorrectlyAnswered()
        {
            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                return _gameMaster.IsPlayerGettingOutOfPenaltyBox();
            }
            else
            {
                return _gameMaster.DidPlayerWin("Answer was corrent!!!!");
            }
        }

        public bool wrongAnswer()
        {
            _gameMaster.ContinueOnWrongAnswer();

            return true;
        }
    }
}
