using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterShop.Goods.Domain.Primitives
{
    public class Result<TResult>
    {
        private Result(TResult result)
        {
            IsSuccess = true;
            Error = Error.None;
            Value = result;
        }

        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
            Value = default;
        }

        public bool IsSuccess { get; set; }

        public bool IsFailure => !IsSuccess;

        public TResult? Value { get; private set; }

        public Error Error { get; set; }

        public static Result<TResult> Success(TResult value) => new(value);
        public static Result<TResult> Failure(Error error) => new(false, error);

        public static implicit operator Result<TResult>(TResult value) => Result<TResult>.Success(value);
    }

    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; set; }

        public bool IsFailure => !IsSuccess;
        public Error Error { get; set; }

        public static Result Success() => new(true, Error.None);
       
        public static Result Failure(Error error) => new(false, error);
    }
}

