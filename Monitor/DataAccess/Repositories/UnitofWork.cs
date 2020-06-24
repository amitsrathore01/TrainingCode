using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using XupMonitor.Helper;

namespace XupMonitor.DataAccess.Repositories
{
    public class UnitofWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ICheckRegisterRepository _checkregisterRepository;
        private ICheckRunRepository _checkrunRepository;
        private bool _disposed;
        public UnitofWork(ConnectionStrings config)
        {
            _connection = new SqlConnection(config.DbXubConString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public ICheckRegisterRepository CheckRegisterRepository => _checkregisterRepository ?? (_checkregisterRepository = new CheckRegisterRepository(_transaction));
        public ICheckRunRepository CheckRunRepository => _checkrunRepository ?? (_checkrunRepository = new CheckRunRepository(_transaction));
        private void ResetRepositories()
        {
            _checkregisterRepository = null;
            _checkrunRepository = null;
        }
        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }
        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UnitofWork()
        {
            Dispose(false);
        }
    }
}
