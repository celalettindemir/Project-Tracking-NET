using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BussinessAspects.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles=roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor?.HttpContext.User.ClaimRoles();
            foreach(var role in _roles){
                if(roleClaims!=null&&roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorazitionDenied);
        }
    }
}
