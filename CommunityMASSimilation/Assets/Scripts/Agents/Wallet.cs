using System;

namespace Agents
{
    public class Wallet
    {
        private int currency;

        public Wallet(int currency)
        {
            this.currency = currency;
        }

        public void AddCurrency(int currency)
        {
            this.currency += currency;
        }

        public void SubstractCurrency(int currency)
        {
            this.currency -= currency;
            if (currency < 0)
                throw new Exception("Unacceptable!");
        }

        public int GetCurrency()
        {
            return currency;
        }
    }
}
