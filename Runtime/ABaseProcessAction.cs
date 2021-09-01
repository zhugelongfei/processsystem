using System;

namespace Lonfee.ProcessSystem
{
    public abstract class ABaseProcessAction
    {
        private Action finishedCB;

        internal void Init(Action finishedCB)
        {
            this.finishedCB = finishedCB;
        }

        public abstract void Enter();

        public abstract void Exit();

        protected void OnFinished()
        {
            finishedCB?.Invoke();
        }

    }
}
