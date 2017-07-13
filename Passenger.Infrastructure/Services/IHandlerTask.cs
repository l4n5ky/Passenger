﻿using NLog;
using Passenger.Core.Domain;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnError(Func<Exception, Task> onError,
            bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Logger, Task> onError,
            bool propagateException = false);
        IHandlerTask OnCustomError(Func<PassengerException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<PassengerException, Logger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        Task ExecuteAsync();
    }
}
