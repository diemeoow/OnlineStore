using System.Linq;
using System.Windows.Controls;

namespace OnlineStore
{
	public partial class OrderHistoryPage : Page
	{
		public OrderHistoryPage()
		{
			InitializeComponent();
			LoadOrderHistory();
		}

		private void LoadOrderHistory()
		{
			var orders = StoreEntities.GetContext().OrderHistory.Select(o => new
			{
				o.OrderID,
				o.OrderDate,
				ProductName = o.Products.Name,
				o.Quantity,
				TotalPrice = o.Quantity * o.Products.Price
			}).ToList();

			OrderHistoryGrid.ItemsSource = orders;
		}
	}
}
