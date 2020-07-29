using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public interface IExpirable : IActivatable
    {
        DateTime Expiry { get; }
    }
}
