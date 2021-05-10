using Professions;
using SpaceGeneration.DataModels;
using System.Linq;

namespace Agents.Work
{
    public class AgentWorkTask
    {
        public WorkTask WorkTask { get; set; }
        public ResourceCountPair Product { get { return WorkTask.product; } }
        public Workplace Profession { get { return WorkTask.profession; } }
        public HexType WorkplaceHexType { get { return Profession.workplaceHexType; } }

        public bool IsIngredience(Resource resource)
        {
            return WorkTask.GetRequiredIngredience().Any(r => r.resource == resource);
        }
    }
}
