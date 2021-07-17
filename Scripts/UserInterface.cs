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

        points = 0;
        higherScore = 0;

        isGameOver = false;
        isControlActive = true;
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
        newGameButton.Visible = true;
        textGameOver.Visible = true;
    }

    void OnNewGameButtonPressed()
    {
        points = 0;
        textPoints.Text = points.ToString();
        textPoints.Visible = true;
        textHigherScore.Visible = higherScore > 0 ? true : false;
        newGameButton.Visible = false;
        textGameOver.Visible = false;

        EmitSignal("ButtonNewGamePressed");
    }
    
}
