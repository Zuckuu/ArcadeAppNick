namespace ArcadeAppNick;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void ImageButton1_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("quiz_home"); 
    }

    private async void ImageButton2_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("storybook_home"); 
    }

    private async void ImageButton3_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("platform_home"); 
    }

    private async void ImageButton4_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("clicker_home"); 
    }

    private async void ImageButton5_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("simulator"); 
    }
    
    private async void ImageButton7_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("weather"); 
    }

    async private void ImageButton6_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("legendary");
    }

    async private void ImageButton8_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("tictactoe");
    }

    async private void ImageButton9_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("checkers"); 
    }

    async private void ImageButton10_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("zoo");
    }

    private void AbsoluteLayout_SizeChanged(object sender, EventArgs e)
    {
        if(Width < 700)
        {
            App_Desc1.IsVisible = false;
            App_Desc2.IsVisible = false;
            App_Desc3.IsVisible = false;
            App_Desc4.IsVisible = false;
            App_Desc5.IsVisible = false;
            App_Desc6.IsVisible = false;
            App_Desc7.IsVisible = false; 
            App_Desc8.IsVisible = false;
            App_Desc9.IsVisible = false;
            App_Desc10.IsVisible = false;
        }
        else
        {
            App_Desc1.IsVisible = true;
            App_Desc2.IsVisible = true;
            App_Desc3.IsVisible = true;
            App_Desc4.IsVisible = true;
            App_Desc5.IsVisible = true;
            App_Desc6.IsVisible = true;
            App_Desc7.IsVisible = true;
            App_Desc8.IsVisible = true;
            App_Desc9.IsVisible = true;
            App_Desc10.IsVisible = true;
        }
    }

    async private void InventoryButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("inventory");
    }

}

