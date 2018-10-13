using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.SDK.Shared
{
    public interface IServiceObject
    {
        String ServiceRoot { get; }
        String APIKey { get; }
    }
}
