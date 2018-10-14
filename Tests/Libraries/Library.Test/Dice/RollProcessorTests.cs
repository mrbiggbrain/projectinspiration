using System;
using System.Collections.Generic;
using System.Text;
using ProjectInspiration.Library.Dice;
using ProjectInspiration.Library.Dice.Exceptions;
using ProjectInspiration.Library.Dice.Models.Request;
using Xunit;

namespace ProjectInspiration.Test.Libraries.Library.Dice
{
    public class RollProcessorTests
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Roll_ShouldThrowOnNullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => RollProcessor.Roll(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Roll_ShouldThrowOnArgumentWithNullRollText()
        {
            // Setup
            RollRequest request = new RollRequest(null);

            // Assert
            Assert.Throws<ArgumentException>(() => RollProcessor.Roll(request));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Roll_ShouldThrowOnMissingDiceCount()
        {
            RollRequest request = new RollRequest("d20");

            Assert.Throws<RollParseException>(() => RollProcessor.Roll(request));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void Roll_ShouldThrowOnMissingDiceSides()
        {
            RollRequest request = new RollRequest("5d");

            Assert.Throws<RollParseException>(() => RollProcessor.Roll(request));
        }

        [Fact]
        public void Roll_ShouldThrowOnInvalidCharacters()
        {
            RollRequest request = new RollRequest("23fhkhjdkdeirueo25");
            Assert.Throws<RollParseException>(() => RollProcessor.Roll(request));
        }

        [Fact]
        public void Roll_ShouldThrowOnInvalidRoll()
        {
            RollRequest request = new RollRequest("2dd20");
            Assert.Throws<RollParseException>(() => RollProcessor.Roll(request));
        }
    }
}
