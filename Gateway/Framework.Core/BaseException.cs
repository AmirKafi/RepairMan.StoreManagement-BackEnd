using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class BaseException : Exception
    {
        public string Code { get; protected set; }

        public BaseException(string code, string? message) : base(message)
        {
            this.Code = code;
        }
    }

    public class EntityNotFoundException : BaseException
    {
        public EntityNotFoundException(string code = "1000602", string message = "آیتم مورد نظر یافت نشد!") : base(code, message)
        {
        }
    }
}
