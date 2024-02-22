namespace ArcadeAppNick;
using ArcadeAppNick.Models;

public partial class Login : ContentPage
{

	public Login()
	{
		InitializeComponent();
        //current entry: username: dev | password: abc
	}

    async private void LoginButton_Clicked(object sender, EventArgs e)
    {
        Users result = App.UserRepo.GetUser(UsernameEntry.Text);

        if(result != null)
        {
            if(UsernameEntry.Text == result.Username && PasswordEntry.Text == result.Password)
            {
                await Shell.Current.GoToAsync("arcade_main");
                App.LoggedInUser = result.Username;
            }
        }
    }

    async private void ToSignUpButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("signup");
    }
}