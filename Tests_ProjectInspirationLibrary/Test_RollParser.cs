//-----------------------------------------------------------------------
// <copyright file="Test_RollParser.cs" company="Project Inspiration">
//     Copyright (c) Nicholas A Young. All rights reserved.
// </copyright>
// <author>Nicholas A. Young</author>
//-----------------------------------------------------------------------
namespace Tests_ProjectInspirationLibrary
{
    using NUnit.Framework;
    using ProjectInspirationLibrary.Dice.Parser;

    /// <summary>
    /// Tests for ProjectInspirationLibrary.Dice.Parser.RollParser
    /// </summary>
    public class Test_RollParser
    {
        /// <summary>
        /// Perform setup steps for tests.
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Validates if text representing a simple dice roll returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing capital letters returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollCaps_IsTrue()
        {
            bool result = RollParser.Check("1D20");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if garbage text returns as valid. Should be false.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesGarbage_IsFalse()
        {
            bool result = RollParser.Check("aksdasfgahdwquefshdfb");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the ADV keyword returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollAdvantage_IsTrue()
        {
            bool result = RollParser.Check("1d20 adv");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the DIS keyword returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollDisadvantage_IsTrue()
        {
            bool result = RollParser.Check("1d20 dis");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the KL keyword returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepLow_IsTrue()
        {
            bool result = RollParser.Check("2d20 kl1");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the KL keyword with no value returns as valid. Should be false.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepLowNoValue_IsFalse()
        {
            bool result = RollParser.Check("2d20 kl");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the KH keyword returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepHigh_IsTrue()
        {
            bool result = RollParser.Check("2d20 kh1");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but containing the KH keyword with no value returns as valid. Should be false.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepHighNoValue_IsFalse()
        {
            bool result = RollParser.Check("2d20 kh");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but with the ADV keyword in caps returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesSimpleRollAdvantageCaps_IsTrue()
        {
            bool result = RollParser.Check("1d20 ADV");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a complex dice roll but with various dice types, keywords, simple numbers, and comments returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesComplexRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20 + 7d8 adv + 4d8 dis + 6 + 88d100 + 4D8 + 6d22 DIS + 7d4 KH2 + 8d4 Kl4 # Hello!");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but with the two kinds of dice returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesDoubleRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20 + 1d8");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but with a comment returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesComment_IsTrue()
        {
            bool result = RollParser.Check("1d20 #Test");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple number returns as valid. Should be true.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesNumber_IsTrue()
        {
            bool result = RollParser.Check("1337");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Validates if text representing a simple dice roll but with incorrect letter for separator returns as valid. Should be false.
        /// </summary>
        [Test]
        public void RollParser_Check_ValidatesDoubleRoll_IsFalse()
        {
            bool result = RollParser.Check("1x20");
            Assert.IsFalse(result);
        }
    }
}