using NLog;
using Passenger.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class HandlerTask : IHandlerTask
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IHandler _handler;
        private readonly Func<Task> _runAsync;
        private Func<Task> _validateAsync;
        private Func<Task> _alwaysAsync;
        private Func<Task> _onSuccessAsync;
        private Func<Exception, Task> _onErrorAsync;
        private Func<Exception, Logger, Task> _onErrorWithLoggerAsync;
        private Func<PassengerException, Task> _onCustomErrorAsync;
        private Func<PassengerException, Logger, Task> _onCustomErrorWithLoggerAsync;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Func<Task> run)
        {
            _handler = handler;
            _runAsync = run;
        }

        public HandlerTask(IHandler handler, Func<Task> run, Func<Task> validate = null)
        {
            _handler = handler;
            _runAsync = run;
            _validateAsync = validate;
        }

        public IHandlerTask Always(Func<Task> always)
        {
            _alwaysAsync = always;

            return this;
        }

        public IHandler Next() => _handler;

        public IHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false)
        {
            _onErrorAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Logger, Task> onError, bool propagateException = false)
        {
            _onErrorWithLoggerAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<PassengerException, Logger, Task> onCustomError, bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLoggerAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccessAsync = onSuccess;

            return this;
        }


        public async Task ExecuteAsync()
        {
            try
            {
                if(_validateAsync != null)
                {
                    await _validateAsync();
                }
                await _runAsync();
                if(_onSuccessAsync != null)
                {
                    await _onSuccessAsync();
                }
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(exception);
                if(_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if(_alwaysAsync != null)
                {
                    await _alwaysAsync();
                }
            }
        }

        private async Task HandleExceptionAsync(Exception exception)
        {
            var customException = exception as PassengerException;
            if(customException != null)
            {
                if(_onCustomErrorAsync != null)
                {
                    await _onCustomErrorAsync(customException);
                }
            }

            var executeOnError = _executeOnError || customException == null;
            if(!executeOnError)
            {
                return;
            }
            if(_onErrorAsync != null)
            {
                await _onErrorAsync(exception);
            }
        }
    }
}
