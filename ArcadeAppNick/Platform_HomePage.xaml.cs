namespace ArcadeAppNick;

public partial class Platform_HomePage : ContentPage
{
	public Platform_HomePage()
	{
		InitializeComponent();
        SizeChanged += Grid_SizeChanged;
    }

    private void Grid_SizeChanged(object sender, EventArgs e)
    {
        if (Width < 400 || Height < 600)
        {
            Title.FontSize = 40;
            Button1.FontSize = 12;
            Button1.WidthRequest = 160;
            Button1.HeightRequest = 50;
            Button1.Margin = new Thickness(0, 0, 0, 150);
            Button2.FontSize = 12;
            Button2.WidthRequest = 160;
            Button2.HeightRequest = 50;
            Button2.Margin = new Thickness(0, 0, 0, 100);
            Button3.FontSize = 12;
            Button3.WidthRequest = 160;
            Button3.HeightRequest = 50;
            Button2.Margin = new Thickness(0, 0, 0, 50);
        }
        else
        {
            Title.FontSize = 75;
            Button1.FontSize = 25;
            Button1.WidthRequest = 300;
            Button1.HeightRequest = 75;
            Button1.Margin = new Thickness(0, 0, 0, 300);
            Button2.FontSize = 25;
            Button2.WidthRequest = 300;
            Button2.HeightRequest = 75;
            Button2.Margin = new Thickness(0, 0, 0, 200);
            Button3.FontSize = 25;
            Button3.WidthRequest = 300;
            Button3.HeightRequest = 75;
            Button3.Margin = new Thickness(0, 0, 0, 100);
        }
    }

    private async void Button1_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("platform_main");
    }

    private async void Button3_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("arcade_main");
    }
}