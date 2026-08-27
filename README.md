# Testable WinForms Application

A simple Windows Forms application demonstrating how to build a **testable WinForms application using the MVP (Model–View–Presenter) pattern**.

This project accompanies the article:

**[Architecting Testable WinForms Applications Using the MVP Pattern](https://optimalcoder.net/testable-winforms-applications-mvp-pattern/)**

## Architecture

```text
+-------------------+
|       View        |
|   Windows Form    |
+---------+---------+
          |
          | IProductView
          v
+-------------------+
|    Presenter      |
| ProductPresenter  |
+---------+---------+
          |
          | IProductDataAccess
          v
+-------------------+
|   Data Access     |
|  Product Storage  |
+-------------------+
```

The View communicates with the Presenter through the `IProductView` interface, while the Presenter communicates with the data layer through `IProductDataAccess`.

This separation makes it possible to replace real dependencies with mocks or substitutes when writing unit tests.

## Project Structure

```text
testable-winforms-app/
│
├── Desktop.Client/
│   └── WinForms application
│
├── Desktop.Client.Tests/
│   └── Unit tests
│
└── WinFormsTestableApp.sln
```

## Testing

The project demonstrates how Presenter logic can be tested without requiring the actual Windows Forms UI.

For example, the View and data-access dependencies can be replaced with test doubles:

```csharp
IProductView view = Substitute.For<IProductView>();
IProductDataAccess dataAccess = Substitute.For<IProductDataAccess>();

ProductPresenter presenter =
    new ProductPresenter(view, dataAccess);
```

A test can then raise a View event and verify that the Presenter invokes the expected data-access operation:

```csharp
view.AddNewProduct +=
    Raise.Event<EventHandler<ProductViewModel>>(view, viewModel);

dataAccess.Received().AddProduct(
    Arg.Is<Product>(x =>
        x.Price == 2 &&
        x.Name == "Test"));
```

This allows the application logic to be tested without displaying a Windows Form or interacting with the real data layer.

## Getting Started

### Running the Application

1. Clone the repository:

```bash
git clone https://github.com/mirnes-mrkaljevic/testable-winforms-app.git
```

2. Open the solution in Visual Studio.

3. Build the solution.

4. Run the `Desktop.Client` project.

## Related Article

For a detailed explanation of the architecture, MVP implementation, interfaces, and unit-testing approach, see:

**[Architecting Testable WinForms Applications Using the MVP Pattern](https://optimalcoder.net/testable-winforms-applications-mvp-pattern/)**

## License

This repository is provided as an educational example demonstrating a testable architecture for Windows Forms applications.