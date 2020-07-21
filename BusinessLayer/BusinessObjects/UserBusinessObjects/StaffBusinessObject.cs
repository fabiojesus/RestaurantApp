using Recodme.Academy.RestaurantApp.BusinessLayer.OperationResults;
using Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.UserDataAccessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.UserRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.UserBusinessObjects
{
    public class StaffRecordBusinessObject
    {
        protected readonly StaffRecordDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };


        public StaffRecordBusinessObject()
        {
            _dao = new StaffRecordDataAccessObject();
        }

        #region List
        public virtual OperationResult<List<StaffRecord>> List()
        {
            try
            {
                
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch(Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<StaffRecord>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Non-deleted
        public virtual OperationResult<List<StaffRecord>> ListNonDeleted()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<StaffRecord>>> ListNonDeletedAsync()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<StaffRecord>> Filter(Func<StaffRecord, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<StaffRecord>>> FilterAsync(Func<StaffRecord, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create

        public virtual OperationResult Create(StaffRecord staffRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(staffRecord);
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(StaffRecord staffRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(staffRecord);
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read 
        public virtual OperationResult<StaffRecord> Read(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<StaffRecord> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<StaffRecord>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<StaffRecord>> ReadAsync(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<StaffRecord> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<StaffRecord>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public virtual OperationResult Update(StaffRecord staffRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(staffRecord);
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(StaffRecord staffRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(staffRecord);
                transactionScope.Complete();
                return new OperationResult<List<StaffRecord>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffRecord>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public virtual OperationResult Delete(StaffRecord staffRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(staffRecord);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(StaffRecord staffRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(staffRecord);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public virtual OperationResult Delete(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Delete(id);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(Guid id)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(id);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }
        #endregion
    }
}