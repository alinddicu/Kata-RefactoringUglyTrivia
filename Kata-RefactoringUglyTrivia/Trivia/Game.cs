namespace UglyTrivia
{
    using System;
    using Trivia;
    using Trivia.Actors;
    using Trivia.Question;

    public class Game : IGame
    {
        private readonly Action<string> _writeLine;

        private readonly Announcer _announcer;
        private readonly QuestionMaster _questionMaster;
        private readonly GameMaster _gameMaster;
        private PlayerTurn _playerTurn;

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
            _playerTurn = new PlayerTurn(_gameMaster);
            _playerTurn.Execute(roll);
        }

        public bool wasCorrectlyAnswered()
        {
            return _gameMaster.ContinueOnCorrectAnswer();
        }

        public bool wrongAnswer()
        {
            _gameMaster.ContinueOnWrongAnswer();

            return true;
        }
    }
}
