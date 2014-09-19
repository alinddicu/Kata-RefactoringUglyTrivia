namespace Trivia.Staff
{
    using System.Collections.Generic;

    public class GameMaster
    {
        private readonly GameAnnouncer _gameAnnouncer;

        private readonly List<Player> _players = new List<Player>();

        private int _currentPlayerIndex = 0;

        public GameMaster(GameAnnouncer gameAnnouncer)
        {
            _gameAnnouncer = gameAnnouncer;
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

        public int CountPlayers()
        {
            return _players.Count;
        }

        public void SetNextPlayer()
        {
            _currentPlayerIndex++;
        }

        public void StartNewRound()
        {
            if (_currentPlayerIndex == _players.Count)
            {
                _currentPlayerIndex = 0;
            }
        }
    }
}
