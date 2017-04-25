namespace Fanex.Razorbill.Module.Core.Web.Filters
{
    using System;
    using System.Web.Mvc;
    using Alpha.Web.Validators;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class JsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var result = AntiForgeryValidator.ValidateRequest();

            if (result.ErrCode != 0)
            {
                throw new HttpAntiForgeryException("Validation Anti XSRF failed.");
            }
        }
    }
}