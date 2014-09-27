namespace Trivia.Actors
{
    using System.Collections.Generic;

    public class GameMaster
    {
        private readonly List<Player> _players = new List<Player>();
        private int _currentPlayerIndex = 0;

        public GameMaster(Announcer announcer, QuestionMaster questionManager)
        {
            Announcer = announcer;
            QuestionMaster = questionManager;
        }

        public Announcer Announcer { get; private set; }

        public QuestionMaster QuestionMaster { get; private set; }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            Announcer.PlayerWasAdded(player);
            Announcer.PlayerPosition(_players.Count);
        }

        public bool ContinueOnCorrectAnswer()
        {
            if (GetCurrentPlayer().InPenaltyBox)
            {
                return IsPlayerGettingOutOfPenaltyBox();
            }
            else
            {
                return DidPlayerWin("Answer was corrent!!!!");
            }
        }

        public void ContinueOnWrongAnswer()
        {
            Announcer.WrongAnswer();
            Announcer.PlayerWasSentToPenaltyBox(GetCurrentPlayer());
            GetCurrentPlayer().InPenaltyBox = true;

            SetNextPlayer();
            StartNewRound();
        }

        public Player GetCurrentPlayer()
        {
            return _players[_currentPlayerIndex];
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

        private bool IsPlayerGettingOutOfPenaltyBox()
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

        private bool DidPlayerWin(string correctAnswerMessage)
        {
            Announcer.CorrectAnswer(correctAnswerMessage);
            GetCurrentPlayer().AddGoldCoin();
            Announcer.PlayerGoldCoins(GetCurrentPlayer());

            bool winner = GetCurrentPlayer().DidIWin();
            SetNextPlayer();
            StartNewRound();

            return winner;
        }
    }
}
