namespace Trivia
{
    public class Player
    {
        private readonly string _name;

        private int _numberGoldCoins;

        public Player(string name)
        {
            _name = name;
        }

        public bool InPenaltyBox { get; set; }

        public int Place { get; private set; }

        public void MoveForward(int roll)
        {
            Place = (Place + roll) % 12;
        }

        public void AddGoldCoin()
        {
            _numberGoldCoins++;
        }

        public int GetGoldCoins()
        {
            return _numberGoldCoins;
        }

        public bool DidIWin()
        {
            return _numberGoldCoins != 6;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
