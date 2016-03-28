using System;

namespace MyActorSystem
{
    internal class MockAudit : IAudit, IDisposable
    {
        private bool _isDisposing;

        public void Dispose()
        {
            _isDisposing = true;    
        }

        public void Write(string msg)
        {
            Console.WriteLine(msg);
        }


    }
}