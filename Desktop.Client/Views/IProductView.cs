using Desktop.Client.Models;
using System;
using System.Collections.Generic;

namespace Desktop.Client.Views
{
    public interface IProductView
    {
        event EventHandler ViewLoad;
        event EventHandler<ProductViewModel> AddNewProduct;
        event EventHandler<ProductViewModel> ModifyProduct;
        event EventHandler<int> DeleteProduct;
        event EventHandler<int> ProductSelected;

        void PopulateDataGridView(IList<Product> products);
        void ClearInputControls();
        void ShowMessage(string message);
    }
}