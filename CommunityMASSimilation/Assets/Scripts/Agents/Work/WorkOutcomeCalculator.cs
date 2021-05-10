using Professions;

namespace Agents.Work
{
    public static class WorkOutcomeCalculator
    {
        public static ResourceCountPair CalculateWorkOutcome(WorkExperience workExperience, WorkTask workTask)
        {
            float resourceCount = workTask.product.count * (1 + workExperience.GetWorkExperience(workTask) * 0.5f);
            return new ResourceCountPair(workTask.product.resource, resourceCount);
        }

        public static ResourceCountPair CalculatePartialWorkOutcome(WorkExperience workExperience, WorkTask workTask)
        {
            float resourceCount = (workTask.product.count / workTask.time) * (1 + workExperience.GetWorkExperience(workTask) * 0.5f);
            return new ResourceCountPair(workTask.product.resource, resourceCount);
        }
    }
}
