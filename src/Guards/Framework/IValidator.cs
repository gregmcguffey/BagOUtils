using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards.Framework
{
    public interface IValidator<TValue>
    {
        TValue ValidateAndReturn();
    }
}
