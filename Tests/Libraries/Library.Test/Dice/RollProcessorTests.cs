using System;
using System.Collections.Generic;
using System.Text;
using ProjectInspiration.Library.Dice;
using Xunit;

namespace ProjectInspiration.Test.Libraries.Library.Dice
{
    public class RollProcessorTests
    {
        [Fact]
        public void Roll_ShouldThrowOnNullArgument()
        {
            Exception ex = Assert.Throws<ArgumentNullException>(() => RollProcessor.Roll(null));
        }
    }
}
