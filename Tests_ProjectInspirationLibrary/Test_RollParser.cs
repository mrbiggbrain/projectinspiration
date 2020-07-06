using NUnit.Framework;
using ProjectInspirationLibrary.Dice.Parser;

namespace Tests_ProjectInspirationLibrary
{
    public class Test_RollParser
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollCaps_IsTrue()
        {
            bool result = RollParser.Check("1D20");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesGarbage_IsFalse()
        {
            bool result = RollParser.Check("aksdasfgahdwquefshdfb");
            Assert.IsFalse(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollAdvantage_IsTrue()
        {
            bool result = RollParser.Check("1d20 adv");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollDisadvantage_IsTrue()
        {
            bool result = RollParser.Check("1d20 dis");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepLow_IsTrue()
        {
            bool result = RollParser.Check("2d20 kl1");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepLowNoValue_IsFalse()
        {
            bool result = RollParser.Check("2d20 kl");
            Assert.IsFalse(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepHigh_IsTrue()
        {
            bool result = RollParser.Check("2d20 kh1");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollKeepHighNoValue_IsFalse()
        {
            bool result = RollParser.Check("2d20 kh");
            Assert.IsFalse(result);
        }

        [Test]
        public void RollParser_Check_ValidatesSimpleRollAdvantageCaps_IsTrue()
        {
            bool result = RollParser.Check("1d20 ADV");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesComplexRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20 + 7d8 adv + 4d8 dis + 6 + 88d100 + 4D8 + 6d22 DIS + 7d4 KH2 + 8d4 Kl4");
            Assert.IsTrue(result);
        }

        [Test]
        public void RollParser_Check_ValidatesDoubleRoll_IsTrue()
        {
            bool result = RollParser.Check("1d20 + 1d8");
            Assert.IsTrue(result);
        }

    }
}