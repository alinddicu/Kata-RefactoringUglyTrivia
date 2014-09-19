namespace Trivia.Staff
{
    using System;
    using Trivia.Question;

    public class GameAnnouncer
    {
        public GameAnnouncer(Action<string> announce)
        {
            Announce = announce;
        }

        public Action<string> Announce { get; private set; }

        public void CorrectAnswer(string roundStartMessage)
        {
            Announce(roundStartMessage);
        }

        public void WrongAnswer()
        {
            Announce("Question was incorrectly answered");
        }

        public void PlayerWasAdded(Player player)
        {
            Announce(player + " was added");
        }

        public void PlayerPosition(int position)
        {
            Announce("They are player number " + position);
        }

        public void CurrentPlayer(Player player)
        {
            Announce(player + " is the current player");
        }

        public void CurrentRoll(int roll)
        {
            Announce("They have rolled a " + roll);
        }

        public void CurrentPlayerGetsOutOfPenaltyBox(Player player, bool getsOut)
        {
            if (getsOut)
            {
                Announce(player + " is getting out of the penalty box");
            }
            else
            {
                Announce(player + " is not getting out of the penalty box");
            }
        }

        public void PlayerWasSentToPenaltyBox(Player player)
        {
            Announce(player + " was sent to the penalty box");
        }

        public void CurrentPlayerLocation(Player player)
        {
            Announce(player + "'s new location is " + player.Place);
        }

        public void CurrentCategory(QuestionCategory questionCategory)
        {
            Announce("The category is " + questionCategory);
        }

        public void PlayerGoldCoins(Player player)
        {
            Announce(player + " now has " + player.GetGoldCoins() + " Gold Coins.");
        }
    }
}
