using Akka.Actor;
using MyActorSystem.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyActorSystem
{
    class Program
    {
        static ActorSystem _actorSystem;

        static void Main(string[] args)
        {
            _actorSystem = ActorSystem.Create("Wallets");
            Console.WriteLine("ActorSystem Created");
            Props prop = Props.Create<WalletActor>(20000);

            IActorRef user1 = _actorSystem.ActorOf(prop, "user1");
            Console.WriteLine($"Actor {nameof(user1)} Created");

            //Parallel.For(0, 20000, (i) => { user1.Tell(new Messages.Withdraw(i.ToString(), 1)); });            
            


            var selection = _actorSystem.ActorSelection("akka://Wallets/user/user1");
            selection.Tell(new Identify("1"));
            //var actor = selection.ResolveOne(TimeSpan.FromMilliseconds(100)).ContinueWith(a => a.Result.Tell(new Messages.Withdraw(20))).;

            //IActorRef user2 = _actorSystem.ActorOf(prop, "user1");

            Console.ReadKey();
            _actorSystem.Terminate();



        }
    }
}
