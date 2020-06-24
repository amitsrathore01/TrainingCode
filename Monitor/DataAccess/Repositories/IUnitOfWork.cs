using System;
using System.Collections.Generic;
using System.Text;

namespace XupMonitor.DataAccess.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        ICheckRegisterRepository CheckRegisterRepository { get; }

        ICheckRunRepository CheckRunRepository { get; }
        void Commit();
    }
}
