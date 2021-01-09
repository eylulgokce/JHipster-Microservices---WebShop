using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    public abstract class AbstractTestPublisher
    {
        public void Start()
        {
            _runningTask = Task.Run(Run);
        }

        public void WaitForEnd()
        {
            _runningTask.Wait();
        }

        protected abstract void Run();
        private Task _runningTask;
    }
}
