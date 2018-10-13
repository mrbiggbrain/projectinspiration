using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models
{
    public interface IRoll
    {
        int Value { get; }
        int Sides { get; }
    }
}
