using Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Convert.ExceptionHandling
{
    public static class ExceptionHandling
    {
        private static List<object> DecorateException(this AggregateException aggregateException)
        {
            var result = aggregateException.InnerExceptions.SelectMany(a =>
                a.GetType().BaseType == typeof(BaseException) ? (a as BaseException).DecorateException() : a.ToViewModel());
            //IList<object> result = new List<object>();
            //foreach (var exception in aggregateException.InnerExceptions)
            //{
            //    if(exception.GetType().BaseType == typeof(BaseException))
            //        result.Concat((exception as BaseException).ToViewModel());
            //    else
            //        result.Concat(exception.ToViewModel());
            //}
            //var result = aggregateException.InnerExceptions.Select(a => new
            //{
            //    Code = a.GetType().GetProperty("Code")?.GetValue(a, null),
            //    a.Message,
            //    Value = a.GetType().GetProperty("Value")?.GetValue(a, null)
            //}).ToList();
            return result.Cast<object>().ToList();
        }
        private static List<object> DecorateException(this BaseException exception)
        {
            var result = new
            {
                Code = exception.GetType().GetProperty("Code")?.GetValue(exception, null),
                exception.Message,
                Value = exception.GetType().GetProperty("Value")?.GetValue(exception, null)
            };
            return new List<object> { result };
        }
        private static List<object> DecorateException(this Exception exception, bool isDeveloperMode)
        {
            var result = new
            {
                Code = exception.GetType().GetProperty("Code")?.GetValue(exception, null),
                Message = isDeveloperMode ? exception.Message : "...خطایی پیش آمده است...",
                Value = exception.GetType().GetProperty("Value")?.GetValue(exception, null)
            };
            return new List<object> { result };
        }

        public static List<object> ToViewModel(this Exception exception, bool isDeveloperMode = false)
        {
            if (exception.GetType() == typeof(AggregateException))
                return (exception as AggregateException).DecorateException();
            if (exception.GetType().BaseType == typeof(BaseException))
                return (exception as BaseException).DecorateException();
            return exception.DecorateException(isDeveloperMode);
        }
    }
}
