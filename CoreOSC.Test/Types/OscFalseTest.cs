﻿using CoreOSC.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreOSC.Test.Types
{
    class OscFalseTest
    {
        [Test]
        public void Deserialize()
        {
            var input = new DWord[] { new DWord(1, 2, 3, 4) };
            var expectedDWords = new DWord[] { new DWord(1, 2, 3, 4) };
            var expectedValue = OscFalse.False;

            var sut = new OscFalseConverter();
            var result = sut.Deserialize(input, out var value);

            Assert.AreEqual(expectedValue, value);
            Assert.AreEqual(expectedDWords, result);
        }

        [Test]
        public void Serialize()
        {
            var input = OscFalse.False;
            var expectedDWords = new DWord[0];

            var sut = new OscFalseConverter();
            var result = sut.Serialize(input);

            Assert.AreEqual(expectedDWords, result);
        }
    }
}
