using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionViewFormattedStringSlowness
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<ListItem> items;
		public ObservableCollection<ListItem> Items 
		{ 
			get
			{
				return items;
			}
			set
			{
				this.items = value;
				value.CollectionChanged += Value_CollectionChanged;
				Value_CollectionChanged(this, null);
			}
		}

		private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
		}

		public MainPageViewModel()
		{
			Items = new ObservableCollection<ListItem>();
			for (int i = 0; i < 20; i++)
			{
				//Generate some random test data
				ListItem itm = new ListItem();
				itm.Num = i;
				itm.Bin = $"{RandomString(2)}-{Random.Shared.Next(10, 20)}-{RandomString(2)}-{Random.Shared.Next(10, 20)}";
				itm.Quantity = Random.Shared.Next(0, 100);
				itm.Id = Random.Shared.Next(1000000, 10000000);
				itm.SKU = RandomString(Random.Shared.Next(8,9));
				itm.UnitSize = Random.Shared.Next(1, 4);
				itm.UnitOfMeasure = RandomSelection(new string[] { "EACH", "CASE", "PACK", "FOOT", "MM" });
				Items.Add(itm);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string RandomString(int length)
		{
			string s = "";
			for (int i = 0; i < length; i++)
			{
				s += (char)Random.Shared.Next(65, 65 + 26); //A-Z
			}
			return s;
		}

		public string RandomSelection(string[] choices)
		{
			return choices[Random.Shared.Next(0, choices.Length - 1)];
		}
	}

	public class ListItem
	{
		public int Num { get; set; }
		public string Bin { get; set; }
		public int Id { get; set; }
		public string SKU { get; set; }
		public decimal Quantity { get; set; }
		public string UnitOfMeasure { get; set; }
		public int UnitSize { get; set; }

		public string QuantityUnitOfMeasure
		{
			get
			{
				return $"{Quantity} : [{UnitOfMeasure} {UnitSize}]";
			}
		}
	}


}

