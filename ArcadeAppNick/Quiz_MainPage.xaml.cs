namespace ArcadeAppNick;

public partial class Quiz_MainPage : ContentPage
{
    string answer1 = "blue whale";
    string answer2 = "parrot";
    string answer3 = "zebra";
    string answer4 = "emperor penguin";
    string answer5 = "pack";
    string answer6 = "iguana";
    string answer7 = "octopus";
    string answer8 = "bald eagle";
    string answer9 = "lion";
    string answer10 = "shrew";
    public Quiz_MainPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        int score = 0;
        int correct = 0;
        string text1 = question1.Text.ToString();
        string text2 = question2.Text.ToString();
        string text3 = question3.Text.ToString();
        string text4 = question4.Text.ToString();
        string text5 = question5.Text.ToString();
        string text6 = question6.Text.ToString();
        string text7 = question7.Text.ToString();
        string text8 = question8.Text.ToString();
        string text9 = question9.Text.ToString();
        string text10 = question10.Text.ToString();

        if (string.Equals(text1, answer1, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text2, answer2, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text3, answer3, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text4, answer4, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text5, answer5, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text6, answer6, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text7, answer7, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text8, answer8, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text9, answer9, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }
        if (string.Equals(text10, answer10, StringComparison.OrdinalIgnoreCase))
        {
            correct++;
        }

        for (int i = 0; i < correct; i++)
        {
            score += 10;
        }

        Submit_Button.Text = "Score: " + score;
    }
}