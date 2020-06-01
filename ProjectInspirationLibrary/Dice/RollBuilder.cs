using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspirationLibrary.Dice
{
    public class RollBuilder
    {
        private List<RollRequest> requests = new List<RollRequest>();

        public RollBuilder()
        {

        }

        // Add a request to the roller
        public RollRequest AddRequest(int sign, int count, int sides, Parser.FilterType filterType, int filterValue)
        {
            RollRequest request = new RollRequest(count, sides, filterType, filterValue);
            requests.Add(request);
            return request;
        }

        // Roll all dice in the roller.
        public List<List<RollResult>> Roll()
        {
            List<List<RollResult>> results = new List<List<RollResult>>();

            foreach(RollRequest request in requests)
            {
                results.Add(request.Roll());
            }

            return results;
        }
    }
}
