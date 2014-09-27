namespace UglyTrivia
{
    using System;
    using Trivia.Actors;
    using Trivia.Question;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;

        private readonly Announcer _announcer;
        private readonly QuestionMaster _questionMaster;
        private readonly GameMaster _gameMaster;
        
        public Game(Action<string> writeLine)
        {
            _writeLine = writeLine;

            _announcer = new Announcer(_writeLine);
            _questionMaster = new QuestionMaster();
            _gameMaster = new GameMaster(_announcer, _questionMaster);
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
                _questionMaster.GetNextQuestion(_announcer, _gameMaster.GetCurrentPlayer(), roll);
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
