using Xamarin.Forms;

namespace FontSample
{
    public partial class FontSamplePage : ContentPage
    {
        public FontSamplePage()
        {
            InitializeComponent();
        }

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
            await Navigation.PushModalAsync(new HomePage());
		}
	}
}
