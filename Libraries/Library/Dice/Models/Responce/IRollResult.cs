using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models.Responce
{
    public interface IRollResult
    {
        List<IResultSet> Sets { get; }
        int Value { get; }
    }
}
