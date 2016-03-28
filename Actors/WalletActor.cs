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
        ISeed _balance;
        IAudit _audit;

        public WalletActor(ISeed balance, IAudit audit)
        {
            _balance = balance;
            _audit = audit;

            Console.WriteLine($"Actor of {nameof(WalletActor)} Created! {this.Self.Path} , {nameof(_balance.Amount)} is {balance.Amount}");
            Receive<Withdraw>((m) =>
            {
                _balance.Amount -= m.Amount;
                Console.WriteLine($"recieved a message {m.Ticket} {nameof(m.Amount)} {m.Amount}, {nameof(_balance.Amount)} is {_balance.Amount}");
                _audit.Write($"{this.Self.Path.Name} Withdraw {m.Amount}");
            });
        }

        protected override void PostStop()
        {
            Console.WriteLine("PostStop");
            base.PostStop();
        }
        public override void AroundPostStop()
        {
            base.AroundPostStop();
        }
        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PreRestart");
            base.PreRestart(reason, message);
        }
    }
}
