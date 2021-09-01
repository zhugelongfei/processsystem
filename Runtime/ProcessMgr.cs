using System.Collections.Generic;

namespace Lonfee.ProcessSystem
{
    public class ProcessMgr
    {
        private int index = -1;
        private List<ABaseProcessAction> processList;
        private ABaseProcessAction currentProcess;
        private bool isRunning;

        private System.Action<bool> finishedCB;

        public ProcessMgr(System.Action<bool> finishedCB, int defaultProcessCapSize = 8)
        {
            this.finishedCB = finishedCB;
            processList = new List<ABaseProcessAction>(defaultProcessCapSize);
        }

        public void AddProcess(ABaseProcessAction process)
        {
            if (process == null)
                return;

            process.Init(OnOneProcessFinished);
            processList.Add(process);
        }

        public void Start()
        {
            if (isRunning)
                return;

            isRunning = true;
            if (processList.Count == 0)
            {
                Stop(false);
            }
            else
            {
                index = -1;
                ToNext();
            }
        }

        private void OnOneProcessFinished()
        {
            ToNext();
        }

        private void ToNext()
        {
            ++index;

            // exit pre process
            if (currentProcess != null)
                currentProcess.Exit();
            currentProcess = null;

            // to next
            if (index >= processList.Count)
            {
                Stop(false);
            }
            else
            {
                currentProcess = processList[index];

                currentProcess.Enter();
            }
        }

        public void Stop()
        {
            Stop(true);
        }

        private void Stop(bool isBreak)
        {
            if (!isRunning)
                return;

            isRunning = false;

            if (currentProcess != null)
                currentProcess.Exit();
            currentProcess = null;

            finishedCB?.Invoke(isBreak);
        }
    }
}
