using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OnlineStore
{
	public partial class CatalogPage : Page
	{
		public CatalogPage()
		{
			InitializeComponent();
			LoadProducts();
		}

		private void LoadProducts()
		{
			var products = StoreEntities.GetContext().Products.ToList();
			DGridProducts.ItemsSource = products;
		}

		private void AddToCart_Click(object sender, RoutedEventArgs e)
		{
			var selectedProduct = (Products)((Button)sender).DataContext;

			if (selectedProduct.Stock > 0)
			{
				var existingCartItem = StoreEntities.GetContext().Cart.FirstOrDefault(c => c.ProductID == selectedProduct.ProductID);

				if (existingCartItem != null)
				{
					existingCartItem.Quantity++;
				}
				else
				{
					StoreEntities.GetContext().Cart.Add(new Cart
					{
						ProductID = selectedProduct.ProductID,
						Quantity = 1
					});
				}

				selectedProduct.Stock--;
				StoreEntities.GetContext().SaveChanges();
				LoadProducts();
			}
			else
			{
				MessageBox.Show("Товар закончился на складе.");
			}
		}
	}
}
