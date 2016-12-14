using Desktop.Client.DBAccess;
using Desktop.Client.Models;
using Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.IO;

namespace Desktop.Client.Presenters
{
    public class ProductPresenter
    {
        private IProductView view;
        private IProductDataAccess dataAccesService;

        private Product selectedProduct = new Product();

        public ProductPresenter(IProductView view, IProductDataAccess dataAccesService)
        {
            this.view = view;
            this.dataAccesService = dataAccesService;
            SubsribeToViewEvents();
        }

        private void SubsribeToViewEvents()
        {
            view.ViewLoad += View_Load;
            view.AddNewProduct += View_AddNewProduct;
            view.ProductSelected += View_ProductSelected;
            view.ModifyProduct += View_ModifyProduct;
            view.DeleteProduct += View_DeleteProduct;
        }

        private void View_DeleteProduct(object sender, int productId)
        {
            if(dataAccesService.DeleteProduct(productId))
            {
                view.ShowMessage(dataAccesService.ErrorMessage);
                return;
            }
           
            ReloadData();
        }

        private void View_ModifyProduct(object sender, ProductViewModel viewModel)
        {
            Validator validator = new Validator();
            string validationMessage = string.Empty;
            if (!validator.ValidateProductName(viewModel.NameText, out validationMessage) || !validator.ValidateProductPrice(viewModel.PriceText, out validationMessage))
            {
                view.ShowMessage(validationMessage);
                return;
            }

            PopulateProductFromViewModel(selectedProduct, viewModel);
           
            if (!dataAccesService.EditProduct(selectedProduct.Id, selectedProduct))
            {
                view.ShowMessage(dataAccesService.ErrorMessage);
                return;
            }

            ReloadData();
            view.ClearInputControls();
        }

        private void View_ProductSelected(object sender, int selectedProductId)
        {
            selectedProduct.Id = selectedProductId;
        }

        private void View_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            IList<Product> products = dataAccesService.GetAllProducts();

            view.PopulateDataGridView(products);
        }

        private void View_AddNewProduct(object sender, ProductViewModel productViewModel)
        {
            Validator validator = new Validator();
            string validationMessage = string.Empty;
            if (!validator.ValidateProductName(productViewModel.NameText, out validationMessage) || !validator.ValidateProductPrice(productViewModel.PriceText, out validationMessage))
            {
                view.ShowMessage(validationMessage);
                return;
            }

            Product product = new Product();
            PopulateProductFromViewModel(product, productViewModel);

            AddNewProductAsync(product);
          
        }

        private void AddNewProductAsync(Product product)
        {
            if (!dataAccesService.AddProduct(product))
            {
                view.ShowMessage(dataAccesService.ErrorMessage);
                return;
            }
            ReloadData();
            view.ClearInputControls();
        }

        private void PopulateProductFromViewModel(Product product, ProductViewModel productViewModel)
        {
            product.Name = productViewModel.NameText;
            product.Price = float.Parse(productViewModel.PriceText);
            string photoPath = productViewModel.PhotoPathText;

            if (!string.IsNullOrEmpty(photoPath))
            {
                product.Photo = File.ReadAllBytes(photoPath);
            }
        }

    }
}
