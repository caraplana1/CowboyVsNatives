using Godot;

public class UserInterface : Control
{

    #region Field Declaration

    // Children
    private Label textPoints;
    private TextureButton newGameButton;
    private Label textGameOver;
    private Label textHigherScore;
    private ColorRect controlsWindow;

    [Signal] delegate void ButtonNewGamePressed();

    private int points;
    private int higherScore;

    // Control window
    private bool isControlActive;
    private bool isGameOver;

    #endregion

    public override void _Ready()
    {
        textPoints = GetChild<Label>(0);
        newGameButton = GetChild<TextureButton>(1);
        textGameOver = GetChild<Label>(2);
        textHigherScore = GetChild<Label>(3);
        controlsWindow = GetChild<ColorRect>(4);

        points = higherScore = 0;

        isGameOver = isControlActive = true;
    }

    public override void _UnhandledInput(InputEvent input)
    {
        // Enter buttom programed to play new game and quit controls page.
        if(input.IsActionReleased("ui_accept"))
        {
            if(isControlActive)
            {
                isControlActive = false;

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

        textHigherScore.Text = "HS:" + higherScore.ToString();
        newGameButton.GetChild<Label>(0).Text = "New Game";
        textPoints.Visible = false;
        newGameButton.Visible = textGameOver.Visible = isGameOver = true;
    }

    void OnNewGameButtonPressed()
    {
        points = 0;
        textPoints.Text = points.ToString();
        textPoints.Visible = true;
        textHigherScore.Visible = higherScore > 0 ? true : false;
        newGameButton.Visible = false;
        textGameOver.Visible = false;
        isGameOver = false;

        EmitSignal("ButtonNewGamePressed");
    }
    
}
