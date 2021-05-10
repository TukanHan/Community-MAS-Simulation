using Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logs
{
    public abstract class LogMessage
    {
        public uint LogNr { get; set; }
        public Agent Agent { get; set; }
        public LogType LogType { get; set; }
        public int Round { get; set; }
    }
}