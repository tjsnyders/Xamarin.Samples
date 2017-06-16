using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ImageFilterSample.Controls
{
	public class HorizontalList : Grid, INotifyPropertyChanged
	{
		protected readonly ICommand SelectedCommand;
		protected readonly StackLayout ItemsStackLayout;
		protected readonly StackLayout StackWithDot;

		public event EventHandler SelectedItemChanged;

		public static readonly BindableProperty SpacingProperty =
			BindableProperty.Create(nameof(Spacing), typeof(int), typeof(HorizontalList), 0, BindingMode.TwoWay);

		public int Spacing
		{
			get { return (int)GetValue(SpacingProperty); }
			set { SetValue(SpacingProperty, value); }
		}

		public static StackOrientation ListOrientation { get; set; } = StackOrientation.Horizontal;


		public HorizontalList()
		{
			SelectedCommand = new Command<object>(item =>
			{
				SelectedItem = item;

				SelectedItem = null;
				/*
				var selectable = item as ISelectable;
				if (selectable == null)
					return;

				SetSelected(selectable);
				SelectedItem = selectable.IsSelected ? selectable : null;*/
			});

			ItemsStackLayout = new StackLayout
			{
				Orientation = ListOrientation,
				Padding = this.Padding,
				Spacing = this.Spacing,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			//StackWithDot = new StackLayout
			//{
			//	Orientation = StackOrientation.Vertical,
			//	Spacing = 3,
			//	VerticalOptions = LayoutOptions.FillAndExpand
			//};
			//StackWithDot.Children.Add(ItemsStackLayout);
			//StackWithDot.Children.Add(new PagerIndicatorDots());

			Children.Add(ItemsStackLayout);
		}

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create<HorizontalList, ICommand>(p => p.Command, null);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<HorizontalList, IEnumerable>(p => p.ItemsSource, default(IEnumerable<object>), BindingMode.TwoWay, null, ItemsSourceChanged);

		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create<HorizontalList, object>(p => p.SelectedItem, default(object), BindingMode.TwoWay, null, null);

		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create<HorizontalList, DataTemplate>(p => p.ItemTemplate, default(DataTemplate));

		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		private static void ItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
		{
			var itemsLayout = (HorizontalList)bindable;
			itemsLayout.SetItems();
		}

		protected virtual void SetItems()
		{
			ItemsStackLayout.Children.Clear();
			ItemsStackLayout.Orientation = ListOrientation;
			if (ItemsSource == null)
				return;

			foreach (var item in ItemsSource)
				ItemsStackLayout.Children.Add(GetItemView(item));

			SelectedItem = ItemsSource.OfType<ISelectable>().FirstOrDefault(x => x.IsSelected);
		}

		protected virtual View GetItemView(object item)
		{
			var content = ItemTemplate.CreateContent();
			var view = content as View;
			if (view == null)
				return null;

			view.BindingContext = item;

			var gesture = new TapGestureRecognizer
			{
				Command = SelectedCommand,
				CommandParameter = item
			};

			AddGesture(view, gesture);

			return view;
		}

		protected void AddGesture(View view, TapGestureRecognizer gesture)
		{
			view.GestureRecognizers.Add(gesture);

			var layout = view as Layout<View>;

			if (layout == null)
				return;

			foreach (var child in layout.Children)
				AddGesture(child, gesture);
		}

		protected virtual void SetSelected(ISelectable selectable)
		{
			selectable.IsSelected = true;
		}

		protected virtual void SetSelectedItem(ISelectable selectedItem)
		{
			var items = ItemsSource;

			foreach (var item in items.OfType<ISelectable>())
				item.IsSelected = selectedItem != null && item == selectedItem && selectedItem.IsSelected;

			var handler = SelectedItemChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var itemsView = (HorizontalList)bindable;
			if (newValue == oldValue)
				return;

			var selectable = newValue as ISelectable;
			itemsView.SetSelectedItem(selectable ?? oldValue as ISelectable);
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == SpacingProperty.PropertyName)
			{
				ItemsStackLayout.Spacing = Spacing;
			}
		}

	}


	public interface ISelectable
	{
		bool IsSelected { get; set; }

		ICommand SelectCommand { get; set; }
	}
}