using MyActorSystem.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyActorSystem.Actors
{
    public class WalletActor : Akka.Actor.ReceiveActor
    {
        long _ballance;
        public WalletActor(long ballance)
        {
            _ballance = ballance;
            Console.WriteLine($"Actor of {nameof(WalletActor)} Created! {this.Self.Path} , {nameof(_ballance)} is {ballance}");
            Receive<Withdraw>((m) =>
            {                
                 _ballance -= m.Amount;
                Console.WriteLine($"recieved a message {m.Ticket} {nameof(m.Amount)} {m.Amount}, {nameof(_ballance)} is {_ballance}");
            });
        }
    }
}
