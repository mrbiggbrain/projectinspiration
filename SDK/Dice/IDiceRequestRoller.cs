using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.Library.Dice.Models.Request;
using ProjectInspiration.Library.Dice.Models.Responce;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.SDK.Dice
{
    interface IDiceRequestRoller
    {
        IRollResult Roll(IRollRequest request);
    }
}
