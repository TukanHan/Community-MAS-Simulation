using Professions;

namespace Agents.Work
{
    public class AgentTask
    {
        public WorkTask WorkTask { get; }

        public float CompletePercent { get; private set; }
        public uint RemainingTime { get; private set; }
        public bool IsDone { get; private set; }

        public AgentTask(WorkTask workTask)
        {
            WorkTask = workTask;
            this.RemainingTime = workTask.time;
        }

        public void UpdateWorked()
        {
            CompletePercent += 1f / WorkTask.time;
            Update();
        }

        public void UpdateSkiped()
        {
            Update();
        }

        private void Update()
        {
            RemainingTime--;
            if (RemainingTime == 0)
                IsDone = true;
        }
    }
}