using Godot;

public class UserInterface : Control
{

    #region Field Declaration

    // Children
    private TextureRect logo;
    private Label textPoints;
    private TextureButton newGameButton;
    private Label textGameOver;
    private Label textHigherScore;
    private ColorRect controlsWindow;
    private Timer reshowMenu;

    [Signal] delegate void ButtonNewGamePressed();

    private int points;
    private int higherScore;

    // Control window
    private bool isControlActive;
    private bool isGameOver;

    #endregion

    public override void _Ready()
    {
        logo = GetChild<TextureRect>(0);
        textPoints = GetChild<Label>(1);
        newGameButton = GetChild<TextureButton>(2);
        textGameOver = GetChild<Label>(3);
        textHigherScore = GetChild<Label>(4);
        controlsWindow = GetChild<ColorRect>(5);
        reshowMenu = GetChild<Timer>(6);

        points = higherScore = 0;

        isGameOver = true;
    }

    public override void _UnhandledInput(InputEvent input)
    {
        // Enter buttom programed to play new game and quit controls page.
        if(input.IsActionReleased("ui_accept"))
        {
            if(controlsWindow.Visible)
            {
                logo.Visible = true;
                controlsWindow.Visible = false;
                newGameButton.Visible = true;
                GD.Print("Desactivando controles");
                
            }
            else if(isGameOver)
            {
                OnNewGameButtonPressed();
            }
        }
    }

    private void IncreasePoints()
    {
        points ++;
        textPoints.Text = points.ToString();
    }

    private void GameOver()
    {
        higherScore = points > higherScore ? points : higherScore;
        reshowMenu.Start();

        textHigherScore.Text = "HS:" + higherScore.ToString();
        textHigherScore.Visible = textPoints.Visible = false;
        textGameOver.Visible = isGameOver = true;
    }

    void OnNewGameButtonPressed()
    {
        textHigherScore.Visible = higherScore > 0 ? true : false;
        points = 0;
        textPoints.Text = points.ToString();
        textPoints.Visible = true;

        if(logo.Visible) logo.Visible = false;
        newGameButton.Visible = false;
        textGameOver.Visible = false;

        isGameOver = false;

        EmitSignal("ButtonNewGamePressed");
    }

    void ReshowMenu()
    {
        newGameButton.GetChild<Label>(0).Text = "New Game";
        textGameOver.Visible = false;
        if (isGameOver) logo.Visible = newGameButton.Visible = true;
    }   
}
