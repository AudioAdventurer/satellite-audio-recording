using System;
using System.Diagnostics;

namespace SAR.Libraries.Common.Trace
{
    public class ConsoleTraceListener : TextWriterTraceListener
    {
        public override void WriteLine(object o)
        {
            Console.WriteLine(o);
        }
    }
}
