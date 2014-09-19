namespace Trivia.Actors
{
    using System.Collections.Generic;

    public class GameMaster
    {
        private readonly GameAnnouncer _gameAnnouncer;
        private readonly QuestionManager _questionManager;

        private readonly List<Player> _players = new List<Player>();

        private int _currentPlayerIndex = 0;

        public GameMaster(GameAnnouncer gameAnnouncer, QuestionManager questionPresentor)
        {
            _gameAnnouncer = gameAnnouncer;
            _questionManager = questionPresentor;
        }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            _gameAnnouncer.PlayerWasAdded(player);
            _gameAnnouncer.PlayerPosition(_players.Count);
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayerIndex];
        }

        public void PlayOnRoll(int roll)
        {
            if (roll % 2 != 0)
            {
                GetCurrentPlayer().IsGettingOutOfPenaltyBox = true;
                _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), true);
                _questionManager.GetNextQuestion(GetCurrentPlayer(), roll);
            }
            else
            {
                _gameAnnouncer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), false);
                GetCurrentPlayer().IsGettingOutOfPenaltyBox = false;
            }
        }

        public bool DidPlayerWin(string correctAnswerMessage)
        {
            _gameAnnouncer.CorrectAnswer(correctAnswerMessage);
            GetCurrentPlayer().AddGoldCoin();
            _gameAnnouncer.PlayerGoldCoins(GetCurrentPlayer());

            bool winner = GetCurrentPlayer().DidIWin();
            SetNextPlayer();
            StartNewRound();

            return winner;
        }

        public bool IsPlayerGettingOutOfPenaltyBox()
        {
            if (GetCurrentPlayer().IsGettingOutOfPenaltyBox)
            {
                return DidPlayerWin("Answer was correct!!!!");
            }
            else
            {
                SetNextPlayer();
                StartNewRound();

                return true;
            }
        }

        public void ContinueOnWrongAnswer()
        {
            _gameAnnouncer.WrongAnswer();
            _gameAnnouncer.PlayerWasSentToPenaltyBox(GetCurrentPlayer());
            GetCurrentPlayer().InPenaltyBox = true;

            SetNextPlayer();
            StartNewRound();
        }

        private void SetNextPlayer()
        {
            _currentPlayerIndex++;
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
