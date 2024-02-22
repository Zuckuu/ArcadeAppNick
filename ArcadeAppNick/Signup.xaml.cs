namespace ArcadeAppNick;

public partial class Signup : ContentPage
{
	public Signup()
	{
		InitializeComponent();
	}

    async private void SignUpButton_Clicked(object sender, EventArgs e)
    {
		 if(UsernameEntry.Text != null && PasswordEntry.Text != null) {
			if(App.UserRepo.GetUser(UsernameEntry.Text) == null)
			{
				App.UserRepo.AddUser(UsernameEntry.Text, PasswordEntry.Text); 
			}
            await Shell.Current.GoToAsync("arcade_main");
        }
    }
}