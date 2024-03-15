using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class BasketWalletEvent
    {
        public string UserName { get; set; }
        public string CoinName { get; set; }
        public double PriceCoin { get; set; }
        public double Amount { get; set; }
        public double TotalPrice { get; set; }
        //public DateTime? DateTime { get; set; }
    }
}
