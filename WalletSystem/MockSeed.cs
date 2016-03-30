using System;
using Wallet.Models.Contracts;

namespace MyActorSystem
{
    internal class MockSeed : ISeed
    {
        public MockSeed()
        {
            Amount = 1000;
        }

        public long Amount { get; set; }
    }
}