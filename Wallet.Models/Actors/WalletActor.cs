using Akka.Actor;
using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models.Contracts;
using Wallet.Models.Messages;

namespace MyActorSystem.Actors
{
    public class WalletActor : ReceivePersistentActor
    {
        ISeed _balance;
        IAudit _audit;

        public override string PersistenceId
        {
            get
            {
                return Self.Path.Name;
            }
        }

        public WalletActor(ISeed balance, IAudit audit)
        {
            _balance = balance;
            _audit = audit;

            SetReceiveTimeout(TimeSpan.FromSeconds(20));
            Recover<SnapshotOffer>(snapshot =>
            {
                //_state = (ExampleState)snapshot.Snapshot;
            });

            Command<ReceiveTimeout>((m) => { Self.Tell(PoisonPill.Instance); });

            Console.WriteLine($"Actor of {nameof(WalletActor)} Created! {this.Self.Path} , {nameof(_balance.Amount)} is {balance.Amount}");
            
            Command<Withdraw>((m) =>
            {
                Persist<Withdraw>(m, (w) =>
                {
                    _balance.Amount -= m.Amount;
                    Console.WriteLine($"recieved a message {m.Ticket} {nameof(m.Amount)} {m.Amount}, {nameof(_balance.Amount)} is {_balance.Amount}");
                    _audit.Write($"{this.Self.Path.Name} Withdraw {m.Amount}");
                });

            });
        }

        protected override void PreStart()
        {
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine("PostStop");
            base.PostStop();
        }
        public override void AroundPostStop()
        {
            Console.WriteLine("AroundPostStop");
            base.AroundPostStop();
        }
        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PreRestart");
            base.PreRestart(reason, message);
        }
    }
}
