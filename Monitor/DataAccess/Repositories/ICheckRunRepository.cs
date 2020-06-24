using System;
using System.Collections.Generic;
using System.Text;
using XupMonitor.DataAccess.Entities;

namespace XupMonitor.DataAccess.Repositories
{
   public interface ICheckRunRepository
    {
        void AddCheckRun(CheckRun checkrun);
    }
}
