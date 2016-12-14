using NSubstitute;
using NUnit.Framework;
using Desktop.Client.Models;
using Desktop.Client.Presenters;
using Desktop.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Client.DBAccess;

namespace Desktop.Client.Tests.Presenters
{
    [TestFixture]
    public class ProductPresenterTests
    {
        [Test]
        public void ExpectToCallAddProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IProductDataAccess dataAccess = Substitute.For<IProductDataAccess>();
            ProductPresenter presenter = new ProductPresenter(view, dataAccess);
           
            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = "Test",
                PriceText = "2"
            };
           
            view.AddNewProduct += Raise.Event<EventHandler<ProductViewModel>>(view, viewModel);
            dataAccess.Received().AddProduct(Arg.Is<Product>(x=>x.Price == 2 && x.Name == "Test"));

        }

        [Test]
        public void ExpectToCallEditProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IProductDataAccess dataAccess = Substitute.For<IProductDataAccess>();
            ProductPresenter presenter = new ProductPresenter(view, dataAccess);

            ProductViewModel viewModel = new ProductViewModel()
            {
                NameText = "Test",
                PriceText = "2"
            };

            view.ModifyProduct += Raise.Event<EventHandler<ProductViewModel>>(view, viewModel);
            dataAccess.Received().EditProduct(Arg.Any<int>(), Arg.Is<Product>(x => x.Price == 2 && x.Name == "Test"));

        }

        [Test]
        public void ExpectToCallDeleteProductOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IProductDataAccess dataAccess = Substitute.For<IProductDataAccess>();
            ProductPresenter presenter = new ProductPresenter(view, dataAccess);

  

            view.DeleteProduct += Raise.Event<EventHandler<int>>(view, 2);
          
            dataAccess.Received().DeleteProduct(2);
    
        }

        [Test]
        public void ExpectToCallGetAllProductsOnAppropriateEventReceived()
        {
            IProductView view = Substitute.For<IProductView>();
            IProductDataAccess dataAccess = Substitute.For<IProductDataAccess>();
            ProductPresenter presenter = new ProductPresenter(view, dataAccess);

            view.ViewLoad += Raise.EventWith(view, new EventArgs());
             dataAccess.Received().GetAllProducts();
           
        }



    }
}
