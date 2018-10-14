using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ProjectInspiration.Library.Dice.Models.Request
{
    public class RollRequest : IRollRequest
    {
        [JsonConstructor]
        public RollRequest(string rollText) => RollText = rollText;

        public String RollText { get; set; }
    }
}
