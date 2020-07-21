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
    public class BookingBusinessObject
    {
        protected readonly BookingDataAccessObject _dao;

        TransactionOptions transactionOptions = new TransactionOptions
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TimeSpan.FromSeconds(30)
        };


        public BookingBusinessObject()
        {
            _dao = new BookingDataAccessObject();
        }

        #region List
        public virtual OperationResult<List<Booking>> List()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Booking>>> ListAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region List Non-deleted
        public virtual OperationResult<List<Booking>> ListNonDeleted()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<List<Booking>>> ListNonDeletedAsync()
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(x => !x.IsDeleted).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Filter
        public OperationResult<List<Booking>> Filter(Func<Booking, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.List();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }

        public async Task<OperationResult<List<Booking>>> FilterAsync(Func<Booking, bool> predicate)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ListAsync();
                result = result.Where(predicate).ToList();
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Create

        public virtual OperationResult Create(Booking clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Create(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> CreateAsync(Booking clientRecord)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.CreateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Read 
        public virtual OperationResult<Booking> Read(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = _dao.Read(id);
                transactionScope.Complete();
                return new OperationResult<Booking> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<Booking>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult<Booking>> ReadAsync(Guid id)
        {
            try
            {

                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                var result = await _dao.ReadAsync(id);
                transactionScope.Complete();
                return new OperationResult<Booking> { Result = result, Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<Booking>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Update
        public virtual OperationResult Update(Booking clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                _dao.Update(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }

        public async virtual Task<OperationResult> UpdateAsync(Booking clientRecord)
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
                await _dao.UpdateAsync(clientRecord);
                transactionScope.Complete();
                return new OperationResult<List<Booking>> { Success = true };
            }
            catch (Exception e)
            {
                return new OperationResult<List<Booking>>() { Success = false, Exception = e };
            }
        }
        #endregion

        #region Delete
        public virtual OperationResult Delete(Booking clientRecord)
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

        public async virtual Task<OperationResult> DeleteAsync(Booking clientRecord)
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