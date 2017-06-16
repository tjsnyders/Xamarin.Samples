using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ImageFilterSample.Helpers;
using ImageFilterSample.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;

namespace ImageFilterSample.ViewModels
{
	public class MainPageViewModel: INotifyPropertyChanged
	{
		public Command<Filter> OnFilterSelectedCommand { get; set; }

		public Command<FilterType> SelectFilterCommand { get; set; }

		public ICommand ApplyFilterCommand { get; set; }

		public ObservableCollection<Filter> Filters { get; set; } = new ObservableCollection<Filter>();

		Filter filterSelected;

		public event PropertyChangedEventHandler PropertyChanged;

		public Filter FilterSelected
		{
			get
			{

				return filterSelected;
			}
			set
			{
				filterSelected = value;
				if (value != null)
					OnFilterSelectedCommand.Execute(filterSelected);
			}
		}
		public MainPageViewModel()
		{
			OnFilterSelectedCommand = new Command<Filter>((f) => OnFilterSelected(f));

			Filters.Add(new Filter()
			{
				Name = "Original",
				Type = FilterType.NoFilter
			});
			Filters.Add(new Filter()
			{
				Name = "Hi-contrast",
				Type = FilterType.Hifi,
			});
			Filters.Add(new Filter()
			{
				Name = "Black And White",
				Type = FilterType.BlackAndWhite,
			});
			Filters.Add(new Filter()
			{
				Name = "Vintage",
				Type = FilterType.Vintage
			});
			Filters.Add(new Filter()
			{
				Name = "Saturated",
				Type = FilterType.Saturated
			});


		}
		public void OnFilterSelected(Filter filter)
		{
			SelectFilterCommand.Execute(filter.Type);
			foreach (var item in Filters)
			{
				item.IsSelected = (filter == item);
			}
		}
	}
}
