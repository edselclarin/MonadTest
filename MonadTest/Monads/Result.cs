namespace MonadTest.Monads
{
    /// <summary>
    /// Either monad pattern which allows action to be taken on the value of 
    /// either the success or failure case.
    /// </summary>
    public class Result<TVal, TErr>
    {
        private readonly TVal val_;
        private readonly TErr err_;
        private readonly bool isSuccess_;

        private Result(TVal val)
        {
            val_ = val;
            err_ = default(TErr);
            isSuccess_ = true;
        }

        private Result(TErr err)
        {
            val_ = default(TVal);
            err_ = err;
            isSuccess_ = false;
        }

        public static Result<TVal, TErr> Success(TVal val) => new Result<TVal, TErr>(val);
        public static Result<TVal, TErr> Failure(TErr err) => new Result<TVal, TErr>(err);

        /// <summary>
        /// Processes either the value or error depending on isSuccess and returns the new type T.
        /// </summary>
        public T Match<T>(Func<TVal, T> successFunc, Func<TErr, T> failureFunc)
        {
            return isSuccess_ ? successFunc(val_) : failureFunc(err_);
        }

        /// <summary>
        /// Action to execute on successful state.
        /// </summary>
        public Result<TVal, TErr> ApplyOnSuccess(Action<TVal> successAction)
        {
            if (isSuccess_) 
            { 
                successAction(val_); 
            }
            return this;
        }

        /// <summary>
        /// Action to execute on failed state.
        /// </summary>
        public Result<TVal, TErr> ApplyOnFailure(Action<TErr> failureAction)
        {
            if (!isSuccess_)
            {
                failureAction(err_);
            }
            return this;
        }
    }
}
