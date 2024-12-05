using System.Windows;

namespace OnlineStore
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MainFrame.Navigate(new CatalogPage());
		}

		private void CatalogButton_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new CatalogPage());
		}
		private void CartButton_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new CartPage());
		}

		private void HistoryButton_Click(object sender, RoutedEventArgs e)
		{
			MainFrame.Navigate(new OrderHistoryPage());
		}

	}
}