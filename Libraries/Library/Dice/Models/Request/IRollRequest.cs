using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.Library.Dice.Models.Request
{
    public interface IRollRequest
    {
        String RollText { get; set; }
    }
}
