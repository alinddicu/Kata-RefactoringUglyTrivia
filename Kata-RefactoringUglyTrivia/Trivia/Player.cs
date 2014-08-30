namespace Trivia
{
    public class Player
    {
        private readonly string _name;
        public int Place { get; private set; }

        private int _numberGoldCoins;
        //private readonly int _purse = 0;
        //private readonly bool _inPenaltyBox = false;

        public Player(string name)
        {
            _name = name;
        }

        public void Avance(int roll)
        {
            Place = (Place + roll) % 12;
        }

        public void AddGoldCoin()
        {
            _numberGoldCoins++;
        }

        public string GetLabelGoldCoin()
        {
            return _name
                   + " now has "
                   + _numberGoldCoins
                   + " Gold Coins.";
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
