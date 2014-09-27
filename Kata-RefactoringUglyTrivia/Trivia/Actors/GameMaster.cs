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
            QuestionManager = questionManager;
        }

        public Announcer Announcer { get; private set; }

        public QuestionMaster QuestionManager { get; private set; }

        public void AddPlayer(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);
            Announcer.PlayerWasAdded(player);
            Announcer.PlayerPosition(_players.Count);
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
                Announcer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), true);
                QuestionManager.GetNextQuestion(Announcer, GetCurrentPlayer(), roll);
            }
            else
            {
                Announcer.CurrentPlayerGetsOutOfPenaltyBox(GetCurrentPlayer(), false);
                GetCurrentPlayer().IsGettingOutOfPenaltyBox = false;
            }
        }

        public bool DidPlayerWin(string correctAnswerMessage)
        {
            Announcer.CorrectAnswer(correctAnswerMessage);
            GetCurrentPlayer().AddGoldCoin();
            Announcer.PlayerGoldCoins(GetCurrentPlayer());

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
            Announcer.WrongAnswer();
            Announcer.PlayerWasSentToPenaltyBox(GetCurrentPlayer());
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
    }
}
