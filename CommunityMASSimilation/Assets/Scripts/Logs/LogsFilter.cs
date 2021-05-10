using Agents;

namespace Logs
{
    public class LogsFilter
    {
        public LogType? LogType { get; set; }
        public Agent Agent { get; set; }
        public uint? RoundFrom { get; set; }
        public uint? RoundTo { get; set; }
        public int RowsOnPageCount { get; set; }
    }
}