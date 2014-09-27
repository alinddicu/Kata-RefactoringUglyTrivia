namespace Trivia.Actors
{
    using System.Collections.Generic;

    public class GameMaster
    {
        private readonly Announcer _announcer;
        private readonly QuestionMaster _questionManager;

        private readonly List<Player> _players = new List<Player>();

        private int _currentPlayerIndex = 0;

        public GameMaster(Announcer announcer, QuestionMaster questionManager)
        {
            _announcer = announcer;
            _questionManager = questionManager;
        }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            _announcer.PlayerWasAdded(player);
            _announcer.PlayerPosition(_players.Count);
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
                _announcer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), true);
                _questionManager.GetNextQuestion(_announcer, GetCurrentPlayer(), roll);
            }
            else
            {
                _announcer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), false);
                GetCurrentPlayer().IsGettingOutOfPenaltyBox = false;
            }
        }

        public bool DidPlayerWin(string correctAnswerMessage)
        {
            _announcer.CorrectAnswer(correctAnswerMessage);
            GetCurrentPlayer().AddGoldCoin();
            _announcer.PlayerGoldCoins(GetCurrentPlayer());

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
            _announcer.WrongAnswer();
            _announcer.PlayerWasSentToPenaltyBox(GetCurrentPlayer());
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
