using Professions;
using System.Collections.Generic;
using UnityEngine;

namespace Agents.Work
{
    public class WorkExperience
    {
        private const float IncreaseExpValue = 0.02f;
        private const float DecreaseExpValue = 0.005f;

        public Dictionary<WorkTask, float> WorksExperiences { get; } = new Dictionary<WorkTask, float>();

        public float GetWorkExperience(WorkTask workTask)
        {
            if (WorksExperiences.ContainsKey(workTask))
                return WorksExperiences[workTask];
            
            return 0;
        }

        public void IncreaseWorkExperience(WorkTask task)
        {
            if (WorksExperiences.ContainsKey(task))
                WorksExperiences[task] = Mathf.Clamp(WorksExperiences[task] + IncreaseExpValue, 0, 1);
            else
                WorksExperiences[task] = IncreaseExpValue;
        }

        public void DecreaseWorksExperience()
        {
            IEnumerable<WorkTask> keys = new List<WorkTask>(WorksExperiences.Keys);
            foreach(WorkTask key in keys)
            {
                WorksExperiences[key] -= DecreaseExpValue;
                if(WorksExperiences[key] < 0)
                {
                    WorksExperiences.Remove(key);
                }
            }
        }
    }
}