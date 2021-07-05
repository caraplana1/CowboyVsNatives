using Godot;

public class UserInterface : Control
{

    #region Field Declaration

    // Children
    private Label textPoints;
    private Button newGameButton;
    private Label textGameOver;
    private Label textHigherScore;

    [Signal] delegate void ButtonNewGamePressed();

    private int points;
    private int higherScore;

    #endregion

    public override void _Ready()
    {
        textPoints = GetChild<Label>(0);
        newGameButton = GetChild<Button>(1);
        textGameOver = GetChild<Label>(2);
        textHigherScore = GetChild<Label>(3);

        points = 0;
        higherScore = 0;
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
        newGameButton.Text = "New Game";
        textPoints.Visible = false;
        newGameButton.Visible = true;
        textGameOver.Visible = true;
        points = -1;
        IncreasePoints();
    }

    void OnNewGameButtonPressed()
    {
        textPoints.Visible = true;
        textHigherScore.Visible = higherScore > 0 ? true : false;
        newGameButton.Visible = false;
        textGameOver.Visible = false;

        EmitSignal("ButtonNewGamePressed");
    }
    
}
