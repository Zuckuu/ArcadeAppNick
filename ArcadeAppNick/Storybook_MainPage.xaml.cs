namespace ArcadeAppNick;

public partial class Storybook_MainPage : ContentPage
{
	public Storybook_MainPage()
	{
		InitializeComponent();
	}

    private void Page_1_ToggleText(object sender, EventArgs e)
    {
        if (Page_1_TextBox.IsVisible == true)
        {
            Page_1_TextBox.IsVisible = false;
        }
        else
        {
            Page_1_TextBox.IsVisible = true;
        }
    }
    private void Page_2a_ToggleText(object sender, EventArgs e)
    {
        if (Page_2a_TextBox.IsVisible == true)
        {
            Page_2a_TextBox.IsVisible = false;
        }
        else
        {
            Page_2a_TextBox.IsVisible = true;
        }
    }
    private void Page_3a_ToggleText(object sender, EventArgs e)
    {
        if (Page_3a_TextBox.IsVisible == true)
        {
            Page_3a_TextBox.IsVisible = false;
        }
        else
        {
            Page_3a_TextBox.IsVisible = true;
        }
    }
    private void Page_3b_ToggleText(object sender, EventArgs e)
    {
        if (Page_3b_TextBox.IsVisible == true)
        {
            Page_3b_TextBox.IsVisible = false;
        }
        else
        {
            Page_3b_TextBox.IsVisible = true;
        }
    }
    private void Page_4a_ToggleText(object sender, EventArgs e)
    {
        if (Page_4a_TextBox.IsVisible == true)
        {
            Page_4a_TextBox.IsVisible = false;
        }
        else
        {
            Page_4a_TextBox.IsVisible = true;
        }
    }
    private void Page_4b_ToggleText(object sender, EventArgs e)
    {
        if (Page_4b_TextBox.IsVisible == true)
        {
            Page_4b_TextBox.IsVisible = false;
        }
        else
        {
            Page_4b_TextBox.IsVisible = true;
        }
    }
    private void Page_4c_ToggleText(object sender, EventArgs e)
    {
        if (Page_4c_TextBox.IsVisible == true)
        {
            Page_4c_TextBox.IsVisible = false;
        }
        else
        {
            Page_4c_TextBox.IsVisible = true;
        }
    }
    private void Page_2b_ToggleText(object sender, EventArgs e)
    {
        if (Page_2b_TextBox.IsVisible == true)
        {
            Page_2b_TextBox.IsVisible = false;
        }
        else
        {
            Page_2b_TextBox.IsVisible = true;
        }
    }
    private void Page_3c_ToggleText(object sender, EventArgs e)
    {
        if (Page_3c_TextBox.IsVisible == true)
        {
            Page_3c_TextBox.IsVisible = false;
        }
        else
        {
            Page_3c_TextBox.IsVisible = true;
        }
    }
    private void Page_3d_ToggleText(object sender, EventArgs e)
    {
        if (Page_3d_TextBox.IsVisible == true)
        {
            Page_3d_TextBox.IsVisible = false;
        }
        else
        {
            Page_3d_TextBox.IsVisible = true;
        }
    }
    private void Page_3e_ToggleText(object sender, EventArgs e)
    {
        if (Page_3e_TextBox.IsVisible == true)
        {
            Page_3e_TextBox.IsVisible = false;
        }
        else
        {
            Page_3e_TextBox.IsVisible = true;
        }
    }

    private void Page_1_SizeChanged(object sender, EventArgs e)
    {
        if (Width < 950)
        {
            Page_1_ToggleButton.WidthRequest = 300;
            Page_1_ToggleButton.FontSize = 12;

            Page_1_TextBox.WidthRequest = 300;
            Page_1_TextBox.HeightRequest = 250;

            Page_1_Text.FontSize = 12;

            Page_1_Choice1.WidthRequest = 150;
            Page_1_Choice1.FontSize = 12;
            Page_1_Choice1.Margin = new Thickness(0, 400, 150, 0);

            Page_1_Choice2.WidthRequest = 150;
            Page_1_Choice2.FontSize = 12;
            Page_1_Choice2.Margin = new Thickness(150, 400, 0, 0);

        }
        else
        {
            Page_1_ToggleButton.WidthRequest = 500;
            Page_1_ToggleButton.FontSize = 18;

            Page_1_TextBox.WidthRequest = 500;
            Page_1_TextBox.HeightRequest = 350;

            Page_1_Text.FontSize = 18;

            Page_1_Choice1.WidthRequest = 200;
            Page_1_Choice1.FontSize = 18;
            Page_1_Choice1.Margin = new Thickness(0, 300, 300, 0);

            Page_1_Choice2.WidthRequest = 200;
            Page_1_Choice2.FontSize = 18;
            Page_1_Choice2.Margin = new Thickness(300, 300, 0, 0);
        }
    }

    private async void Page_1_Choice1_Clicked(object sender, EventArgs e)
    {
        Page_1_Choice2.IsEnabled = false;
        Page_2a.Opacity = 0; //makes page 2a transparent
        Page_2a.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_2a, ScrollToPosition.Start, true);

        await Page_2a.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity
    }
    private async void Page_2a_Choice1_Clicked(object sender, EventArgs e)
    {
        Page_2a_Choice2.IsEnabled = false;
        Page_3a.Opacity = 0; //makes page 2a transparent
        Page_3a.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_3a, ScrollToPosition.Start, true);

        await Page_3a.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity
    }
    private async void Page_2a_Choice2_Clicked(object sender, EventArgs e)
    {
        Page_2a_Choice1.IsEnabled = false;
        Page_3b.Opacity = 0; //makes page 2a transparent
        Page_3b.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_3b, ScrollToPosition.Start, true);

        await Page_3b.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity
    }
    private async void Page_3b_Choice1_Clicked(object sender, EventArgs e)
    {
        Page_3b_Choice2.IsEnabled = false;
        Page_3b_Choice3.IsEnabled = false; 
        Page_4a.Opacity = 0; //makes page 2a transparent
        Page_4a.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_4a, ScrollToPosition.Start, true);

        await Page_4a.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity

    }
    private async void Page_3b_Choice2_Clicked(object sender, EventArgs e)
    {
        Page_3b_Choice1.IsEnabled = false;
        Page_3b_Choice3.IsEnabled = false;
        Page_4b.Opacity = 0; //makes page 2a transparent
        Page_4b.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_4b, ScrollToPosition.Start, true);

        await Page_4b.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity

    }
    private async void Page_3b_Choice3_Clicked(object sender, EventArgs e)
    {
        Page_3b_Choice1.IsEnabled = false;
        Page_3b_Choice2.IsEnabled = false;
        Page_4c.Opacity = 0; //makes page 2a transparent
        Page_4c.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_4c, ScrollToPosition.Start, true);

        await Page_4c.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity

    }
    private async void Page_2b_Choice1_Clicked(object sender, EventArgs e)
    {
        Page_2b_Choice2.IsEnabled = false;
        Page_2b_Choice3.IsEnabled = false;
        Page_3c.Opacity = 0; //makes page 2a transparent
        Page_3c.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_3c, ScrollToPosition.Start, true);

        await Page_3c.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity

    }
    private async void Page_2b_Choice2_Clicked(object sender, EventArgs e)
    {
        Page_2b_Choice1.IsEnabled = false;
        Page_2b_Choice3.IsEnabled = false;
        Page_3d.Opacity = 0; //makes page 2a transparent
        Page_3d.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_3d, ScrollToPosition.Start, true);

        await Page_3d.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity
    }
    private async void Page_2b_Choice3_Clicked(object sender, EventArgs e)
    {
        Page_2b_Choice1.IsEnabled = false;
        Page_2b_Choice2.IsEnabled = false;
        Page_3e.Opacity = 0; //makes page 2a transparent
        Page_3e.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_3e, ScrollToPosition.Start, true);

        await Page_3e.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity

    }
    private async void Page_1_Choice2_Clicked(object sender, EventArgs e)
    {
        Page_1_Choice1.IsEnabled = false;
        Page_2b.Opacity = 0; //makes page 2a transparent
        Page_2b.IsVisible = true; //setting page 2a to visible (still transparent)

        await Task.Delay(100); //small delay to load page 2a

        //scrolling to the start of page 2a
        await scrollview.ScrollToAsync(Page_2b, ScrollToPosition.Start, true);

        await Page_2b.FadeTo(1, 100); //over 100 milliseconds, fades to solid opacity
    }
}