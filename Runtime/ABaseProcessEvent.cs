namespace Lonfee.ProcessSystem
{

    public abstract class ABaseProcessEvent : ABaseProcessAction
    {
        public override void Enter()
        {
            DoAction();

            OnFinished();
        }

        protected abstract void DoAction();

        public override void Exit()
        {

        }
    }
}