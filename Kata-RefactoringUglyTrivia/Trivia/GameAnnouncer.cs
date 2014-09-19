namespace Trivia
{
    using System;
    using Trivia.Question;

    public class GameAnnouncer
    {
        private readonly Action<string> _announce;

        public GameAnnouncer(Action<string> announce)
        {
            _announce = announce;
        }

        public void CorrectAnswer(string roundStartMessage)
        {
            _announce(roundStartMessage);
        }

        public void WrongAnswer()
        {
            _announce("Question was incorrectly answered");
        }

        public void PlayerWasAdded(Player player)
        {
            _announce(player + " was added");
        }

        public void PlayerPosition(int position)
        {
            _announce("They are player number " + position);
        }

        public void CurrentPlayer(Player player)
        {
            _announce(player + " is the current player");
        }

        public void CurrentRoll(int roll)
        {
            _announce("They have rolled a " + roll);
        }

        public void CurrentPlayerGetsOutOfPenaltyBox(Player player, bool getsOut)
        {
            if (getsOut)
            {
                _announce(player + " is getting out of the penalty box");
            }
            else
            {
                _announce(player + " is not getting out of the penalty box");
            }
        }

        public void PlayerWasSentToPenaltyBox(Player player)
        {
            _announce(player + " was sent to the penalty box");
        }

        public void CurrentPlayerLocation(Player player)
        {
            _announce(player + "'s new location is " + player.Place);
        }

        public void CurrentCategory(QuestionCategory questionCategory)
        {
            _announce("The category is " + questionCategory);
        }

        public void PlayerGoldCoins(Player player)
        {
            _announce(player + " now has " + player.GetGoldCoins() + " Gold Coins.");
        }
    }
}
