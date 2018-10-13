using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using ProjectInspiration.Library.Dice.Models;
using ProjectInspiration.SDK.Shared;
using ProjectInspiration.SDK.Shared.Web;

namespace ProjectInspiration.SDK.Dice
{
    public class DiceService : IServiceObject
    {

        public String ServiceRoot { get; set; }
        public String APIKey { get; set; }

        public DiceService(String apiRoot, String apiKey)
        {
            this.ServiceRoot = $"{apiRoot}";
            this.APIKey = apiKey;
        }

        public RollResponce Roll(String rollText)
        {
            RollRequest request = new RollRequest()
            {
                RollText = rollText
            };

            return APIRequestor.Post<RollResponce, RollRequest>(this, "dice", null, request);
        }
    }
}
