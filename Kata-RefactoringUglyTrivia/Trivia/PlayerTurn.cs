using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trivia.Actors;

namespace Trivia
{
    public class PlayerTurn
    {
        private readonly GameMaster _gameMaster;
        private readonly QuestionMaster _questionMaster;
        private readonly Announcer _announcer;

        public PlayerTurn(GameMaster gameMaster)
        {
            _gameMaster = gameMaster;
            _questionMaster = _gameMaster.QuestionMaster;
            _announcer = _gameMaster.Announcer;
        }

        public void Execute(int roll)
        {
            _announcer.CurrentPlayer(_gameMaster.GetCurrentPlayer());
            _announcer.CurrentRoll(roll);

            if (_gameMaster.GetCurrentPlayer().InPenaltyBox)
            {
                PlayOnRoll(roll);
            }
            else
            {
                _questionMaster.GetNextQuestion(_announcer, _gameMaster.GetCurrentPlayer(), roll);
            }
        }

        private void PlayOnRoll(int roll)
        {
            var currentPlayer = _gameMaster.GetCurrentPlayer();
            if (roll % 2 != 0)
            {
                currentPlayer.IsGettingOutOfPenaltyBox = true;
                _announcer.CurrentPlayerGetsOutOfPenaltyBox(currentPlayer, true);
                _questionMaster.GetNextQuestion(_announcer, currentPlayer, roll);
            }
            else
            {
                _announcer.CurrentPlayerGetsOutOfPenaltyBox(currentPlayer, false);
                currentPlayer.IsGettingOutOfPenaltyBox = false;
            }
        }
    }
}
