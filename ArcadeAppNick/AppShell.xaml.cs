namespace ArcadeAppNick;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute("login", typeof(Login)); 

        Routing.RegisterRoute("arcade_main", typeof(MainPage)); 

		Routing.RegisterRoute("quiz_home", typeof(Quiz_HomePage));
        Routing.RegisterRoute("quiz_main", typeof(Quiz_MainPage));

        Routing.RegisterRoute("storybook_home", typeof(Storybook_HomePage));
        Routing.RegisterRoute("storybook_main", typeof(Storybook_MainPage));

        Routing.RegisterRoute("platform_home", typeof(Platform_HomePage));
        Routing.RegisterRoute("platform_main", typeof(Platform_MainPage));

        Routing.RegisterRoute("clicker_home", typeof(Clicker_HomePage));
        Routing.RegisterRoute("clicker_main", typeof(Clicker_MainPage));

        Routing.RegisterRoute("simulator", typeof(Simulator));

        Routing.RegisterRoute("signup", typeof(Signup));

        Routing.RegisterRoute("inventory", typeof(Inventory));

        Routing.RegisterRoute("legendary", typeof(Legendary));

        Routing.RegisterRoute("weather", typeof(Weather));

        Routing.RegisterRoute("tictactoe", typeof(TicTacToe));

        Routing.RegisterRoute("checkers", typeof(Checkers));

        Routing.RegisterRoute("zoo", typeof(Zoo));
    }
}
