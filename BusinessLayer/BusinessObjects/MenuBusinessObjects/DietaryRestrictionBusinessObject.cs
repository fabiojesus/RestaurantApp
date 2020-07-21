using Recodme.Academy.RestaurantApp.BusinessLayer.OperationResults;
using Recodme.Academy.RestaurantApp.DataAccessLayer.DataAccessObjects.MenuDataAcccessObjects;
using Recodme.Academy.RestaurantApp.DataLayer.MenuRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Recodme.Academy.RestaurantApp.BusinessLayer.BusinessObjects.MenuBusinessObjects
{
    public class DietaryRestrictionBusinessObject
    {
        protected readonly DietaryRestrictionDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };


        public DietaryRestrictionBusinessObject()
        {
            _dao = new DietaryRestrictionDataAccessObject();
        }

        #region List
        public virtual OperationResult<List<DietaryRestriction>> List()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<DietaryRestriction>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Non-deleted
        public virtual OperationResult<List<DietaryRestriction>> ListNonDeleted()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<DietaryRestriction>>> ListNonDeletedAsync()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }
        #endregion
        
        #region Filter
        public OperationResult<List<DietaryRestriction>> Filter(Func<DietaryRestriction, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<DietaryRestriction>>> FilterAsync(Func<DietaryRestriction, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create

        public virtual OperationResult Create(DietaryRestriction clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(DietaryRestriction clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read 
        public virtual OperationResult<DietaryRestriction> Read(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<DietaryRestriction> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<DietaryRestriction>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<DietaryRestriction>> ReadAsync(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<DietaryRestriction> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<DietaryRestriction>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public virtual OperationResult Update(DietaryRestriction clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(DietaryRestriction clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<DietaryRestriction>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<DietaryRestriction>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public virtual OperationResult Delete(DietaryRestriction clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(clientRecord);
                transactionScope.Complete();
                return new OperationResult { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> DeleteAsync(DietaryRestriction clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.DeleteAsync(clientRecord);
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