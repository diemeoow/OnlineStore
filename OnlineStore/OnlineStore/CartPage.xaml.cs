using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OnlineStore
{
	public partial class CartPage : Page
	{
		public CartPage()
		{
			InitializeComponent();
			LoadCart();
		}

		private void LoadCart()
		{
			var cartItems = StoreEntities.GetContext().Cart
				.Include(c => c.Products).ToList();

			CartItems.ItemsSource = cartItems;

			TotalAmount.Text = $"Общая сумма: {cartItems.Sum(c => c.Quantity * c.Products.Price):C2}";
		}

		private void Checkout_Click(object sender, RoutedEventArgs e)
		{
			var cartItems = StoreEntities.GetContext().Cart.ToList();

			foreach (var item in cartItems)
			{
                StoreEntities.GetContext().OrderHistory.Add(new OrderHistory
				{
					ProductID = item.ProductID,
					Quantity = item.Quantity,
					OrderDate = DateTime.Now
				});

                StoreEntities.GetContext().Cart.Remove(item);
			}

			StoreEntities.GetContext().SaveChanges();
			LoadCart();
			MessageBox.Show("Заказ успешно оформлен!");
		}

		private void DelFromCart_Click(object sender, RoutedEventArgs e)
		{
			var selectedCartItem = (Cart)((Button)sender).DataContext;

			if (selectedCartItem != null)
			{
				var product = StoreEntities.GetContext().Products
					.FirstOrDefault(p => p.ProductID == selectedCartItem.ProductID);
				if (product != null)
				{
					product.Stock++;
					if (selectedCartItem.Quantity > 1)
					{
						selectedCartItem.Quantity--;
					}
					else
					{
						StoreEntities.GetContext().Cart.Remove(selectedCartItem);
					}
				}

				try
				{
					StoreEntities.GetContext().SaveChanges();
					LoadCart();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Ошибка при удалении товара из корзины: " + ex.Message);
				}
			}
		}

	}
}
