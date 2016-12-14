using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void ExpectToFailValidationIfProductNameIsEmpty()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductName(string.Empty, out message);
            Assert.IsFalse(result);
        }

        [Test]
        public void ExpectToReturnAppropriateMessageIfProductNameIsEmpty()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductName(string.Empty, out message);
            Assert.AreEqual("Product name is required field!", message);

        }

        [Test]
        public void ExpectToPassValidationIfProductNameIsNotEmpty()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductName("Test product name", out message);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExpectToFailValidationIfProductPriceIsEmpty()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductPrice(string.Empty, out message);
            Assert.IsFalse(result);
        }

        [Test]
        public void ExpectToReturnAppropriateMessageIfProductPriceIsEmpty()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductPrice(string.Empty, out message);
            Assert.AreEqual("Product price is required field!", message);

        }

        [Test]
        public void ExpectToFailValidationIfProductPriceIsNotNumber()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductPrice("test3", out message);
            Assert.IsFalse(result);
        }

        [Test]
        public void ExpectToReturnAppropriateMessageIfProductPriceIsNotNumber()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductPrice("test3", out message);
            Assert.AreEqual("Price should be a number!", message);

        }

        [Test]
        public void ExpectToPassValidationIfProductPriceIsNumber()
        {
            Validator validator = new Validator();
            string message;
            bool result = validator.ValidateProductPrice("3.4", out message);
            Assert.IsTrue(result);

        }

    }
}
