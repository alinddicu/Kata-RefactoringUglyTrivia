using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trivia.Actors;

namespace Trivia
{
    public class Turn
    {
        private readonly GameMaster _gameMaster;
        private readonly QuestionMaster _questionMaster;
        private readonly Announcer _announcer;

        public Turn(GameMaster gameMaster)
        {
            _gameMaster = gameMaster;
            _questionMaster = _gameMaster.QuestionManager;
            _announcer = _gameMaster.Announcer;
        }

        public void Play(int roll)
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
    }
}
