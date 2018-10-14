using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    public interface IResultSet
    {
        int Value { get; }
        List<IRoll> Rolls { get; }
    }
}
