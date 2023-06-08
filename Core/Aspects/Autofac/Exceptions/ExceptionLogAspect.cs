using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Exceptions
{
    public class ExceptionLogAspect : MethodInterception
    {
        private LoggerServiceBase? _loggerServiceBase;

        public ExceptionLogAspect(Type loggerService)
        {
            if (!typeof(LoggerServiceBase).IsAssignableFrom(loggerService))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = Activator.CreateInstance(loggerService) as LoggerServiceBase;
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logDetailWithException=GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = e.Message;
            _loggerServiceBase?.Error(logDetailWithException);
        }
        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = invocation.Arguments.Select((x, index) => new LogParameter
            {
                Value = x,
                Type = x.GetType().Name,
                Name = invocation.GetConcreteMethod().GetParameters()[index].Name ?? "<NULL>"
            }).ToList();
            var logDetail = new LogDetailWithException
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name
            };
            return logDetail;
        }
    }
}
