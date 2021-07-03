using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace DeVeeraApp.Utils
{
    public class ParameterBasedOnFormNameAttribute : TypeFilterAttribute
    {
        public ParameterBasedOnFormNameAttribute(string formKeyName, string actionParameterName) : base(typeof(ParameterBasedOnFormNameFilter))
        {
            this.Arguments = new object[] { formKeyName, actionParameterName };
        }
        /// </summary>
        private class ParameterBasedOnFormNameFilter : IActionFilter
        {
            #region Fields

            private readonly string _formKeyName;
            private readonly string _actionParameterName;

            #endregion

            #region Ctor

            public ParameterBasedOnFormNameFilter(string formKeyName, string actionParameterName)
            {
                this._formKeyName = formKeyName;
                this._actionParameterName = actionParameterName;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Called before the action executes, after model binding is complete
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                if (context.HttpContext.Request == null)
                    return;

                //if form key with '_formKeyName' exists, then set specified '_actionParameterName' to true
                context.ActionArguments[_actionParameterName] = context.HttpContext.Request.Form.Keys.Any(key => key.Equals(_formKeyName));

                //we check whether form key with '_formKeyName' exists only
                //uncomment the code below if you want to check whether form value is specified
                //context.ActionArguments[_actionParameterName] = !string.IsNullOrEmpty(context.HttpContext.Request.Form[_formKeyName]);
            }

            /// <summary>
            /// Called after the action executes, before the action result
            /// </summary>
            /// <param name="context">A context for action filters</param>
            public void OnActionExecuted(ActionExecutedContext context)
            {
                //do nothing
            }

            #endregion
        }
    }
}
