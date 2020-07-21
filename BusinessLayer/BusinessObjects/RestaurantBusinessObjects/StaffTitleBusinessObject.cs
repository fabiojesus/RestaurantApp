using Recodme.Academy.RestaurantApp.BusinessLayer.OperationResults;
using Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.RestaurantDataAcccessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.RestaurantRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.RestaurantBusinessObjects
{
    public class StaffTitleBusinessObject
    {
        protected readonly StaffTitleDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };


        public StaffTitleBusinessObject()
        {
            _dao = new StaffTitleDataAccessObject();
        }

        #region List
        public virtual OperationResult<List<StaffTitle>> List()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<StaffTitle>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Non-deleted
        public virtual OperationResult<List<StaffTitle>> ListNonDeleted()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<StaffTitle>>> ListNonDeletedAsync()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<StaffTitle>> Filter(Func<StaffTitle, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<StaffTitle>>> FilterAsync(Func<StaffTitle, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create

        public virtual OperationResult Create(StaffTitle staffTitle)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(staffTitle);
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(StaffTitle staffTitle)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(staffTitle);
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read 
        public virtual OperationResult<StaffTitle> Read(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<StaffTitle> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<StaffTitle>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<StaffTitle>> ReadAsync(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<StaffTitle> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<StaffTitle>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public virtual OperationResult Update(StaffTitle staffTitle)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(staffTitle);
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(StaffTitle staffTitle)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(staffTitle);
                transactionScope.Complete();
                return new OperationResult<List<StaffTitle>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<StaffTitle>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public virtual OperationResult Delete(StaffTitle staffTitle)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(staffTitle);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(StaffTitle staffTitle)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(staffTitle);
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