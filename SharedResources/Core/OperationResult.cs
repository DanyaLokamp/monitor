using System;
namespace SharedResources.Core
{
    public class OperationResult<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool IsException { get; set; }
        public T Result { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(bool success, bool isException = false)
        {
            Success = success;
            IsException = isException;
        }

        public OperationResult(bool success, bool isException, string message)
        {
            Success = success;
            Message = message;
            IsException = isException;
        }

        public OperationResult(bool success, bool isException, string message, T result)
        {
            Success = success;
            Message = message;
            Result = result;
            IsException = isException;
        }
    }

    public class OperationResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool IsException { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(bool success, bool isException = false)
        {
            Success = success;
            IsException = isException;
        }

        public OperationResult(bool success, bool isException, string message)
        {
            Success = success;
            Message = message;
            IsException = isException;
        }
    }
}
