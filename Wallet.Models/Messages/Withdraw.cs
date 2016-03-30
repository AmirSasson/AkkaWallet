using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Models.Messages
{
    public class Withdraw
    {
        public Withdraw(string ticket, long amount)
        {
            Amount = amount;
            Ticket = ticket;
        }
        public long Amount { get; private set; }
        public string Ticket { get; private set; }
    }
}
