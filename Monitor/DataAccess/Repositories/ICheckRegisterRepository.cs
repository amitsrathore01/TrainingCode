using System;
using System.Collections.Generic;
using System.Text;
using XupMonitor.DataAccess.Entities;

namespace XupMonitor.DataAccess.Repositories
{
    public interface ICheckRegisterRepository
    {
        IEnumerable<CheckRegister> GetCheckUpdates();

        void UpdateCheckSchedule(Guid id, bool IsScheduled);
    }
}
