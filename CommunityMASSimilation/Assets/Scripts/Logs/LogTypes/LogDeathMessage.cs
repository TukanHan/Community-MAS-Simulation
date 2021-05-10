using Logs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logs
{
    public class LogDeathMessage : LogMessage
    {
        public override string ToString()
        {
            return $"Runda {Round}: {Agent.AgentName} umarł";
        }
    }
}