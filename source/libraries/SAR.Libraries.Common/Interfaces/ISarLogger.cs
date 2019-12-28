using System;
using System.Collections.Generic;
using System.Text;

namespace SAR.Libraries.Common.Interfaces
{
    public interface ISarLogger
    {
        void Verbose(string message);
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Error(Exception ex);
        void Fatal(string message);
    }
}
