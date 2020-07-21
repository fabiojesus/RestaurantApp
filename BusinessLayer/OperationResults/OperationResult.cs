using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.Academy.RestaurantApp.BusinessLayer.OperationResults
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; set; }
    }

}
