using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase? _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if(!typeof(LoggerServiceBase).IsAssignableFrom(loggerService))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = Activator.CreateInstance(loggerService) as LoggerServiceBase;
        }

        protected override void OnException(IInvocation invocation)
        {
            _loggerServiceBase?.Error(GetLogDetail(invocation));
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _loggerServiceBase?.Info(GetLogDetail(invocation));
        }
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters=invocation.Arguments.Select((x,index) => new LogParameter
            {
                Value=x,
                Type=x.GetType().Name,
                Name = invocation.GetConcreteMethod().GetParameters()[index].Name??"<NULL>"
            }).ToList();
            var logDetail = new LogDetail
            {
                LogParameters = logParameters,
                MethodName = invocation.Method.Name
            };
            return logDetail;
        }
    }
}
