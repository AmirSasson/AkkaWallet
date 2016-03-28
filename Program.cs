using Akka.Actor;
using Akka.DI.Core;
using MyActorSystem.Actors;
using Ninject;
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
            Console.Title = _actorSystem.Name + " System";
            Console.WriteLine("ActorSystem Created");

            #region DI 

            IKernel container = new StandardKernel();
            container.Bind<IAudit>().To<MockAudit>().InSingletonScope();
            container.Bind<ISeed>().To<MockSeed>().InSingletonScope();
            IDependencyResolver d = new Akka.DI.Ninject.NinjectDependencyResolver(container, _actorSystem);
            //_actorSystem.RegisterOnTermination(() => { using (container) { } });
            #endregion


            Props prop = _actorSystem.DI().Props<WalletActor>();// _actorSystem.ActorOf();        

            Parallel.For(1, 200, (i) =>
            {
                IActorRef user = _actorSystem.ActorOf(prop, $"user{i}");
                Console.WriteLine($"Actor {user.Path.Name} Created");
            });


            //Parallel.For(0, 20, (i) => { user1.Tell(new Messages.Withdraw(i.ToString(), 1)); });


            var selection = _actorSystem.ActorSelection("akka://Wallets/user/user1");
            //selection.Tell(PoisonPill.Instance);

            //var actor = selection.ResolveOne(TimeSpan.FromMilliseconds(100)).ContinueWith(a => a.Result.Tell(new Messages.Withdraw(20))).;

            //IActorRef user2 = _actorSystem.ActorOf(prop, "user1");






            Console.ReadKey();
            _actorSystem.Terminate();
            _actorSystem.TerminationTask.Wait();


            Console.ReadKey();

        }
    }
}
