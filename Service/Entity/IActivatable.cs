using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Entity
{
    public interface IActivatable
    {
        DateTime Activation { get; set; }
    }
}
