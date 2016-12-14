using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Client
{
    public class Validator
    {
        public bool ValidateProductName(string productName, out string validationMessage)
        {
            if (string.IsNullOrEmpty(productName))
            {
                validationMessage = "Product name is required field!";
                return false;
            }
            validationMessage = string.Empty;
            return true;
        }


        public bool ValidateProductPrice(string productPriceText, out string validationMessage)
        {
            if (string.IsNullOrEmpty(productPriceText))
            {
                validationMessage = "Product price is required field!";
                return false;
            }

            float price;

            if (!float.TryParse(productPriceText, out price))
            {
                validationMessage = "Price should be a number!";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }
    }
}
