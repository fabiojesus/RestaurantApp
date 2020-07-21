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
    public class MealBusinessObject
    {
        protected readonly MealDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };


        public MealBusinessObject()
        {
            _dao = new MealDataAccessObject();
        }

        #region List
        public virtual OperationResult<List<Meal>> List()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Meal>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Non-deleted
        public virtual OperationResult<List<Meal>> ListNonDeleted()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Meal>>> ListNonDeletedAsync()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Meal>> Filter(Func<Meal, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Meal>>> FilterAsync(Func<Meal, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create

        public virtual OperationResult Create(Meal clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Meal clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read 
        public virtual OperationResult<Meal> Read(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<Meal> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<Meal>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Meal>> ReadAsync(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<Meal> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<Meal>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public virtual OperationResult Update(Meal clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Meal clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Meal>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Meal>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public virtual OperationResult Delete(Meal clientRecord)
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

        public async virtual Task<OperationResult> DeleteAsync(Meal clientRecord)
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