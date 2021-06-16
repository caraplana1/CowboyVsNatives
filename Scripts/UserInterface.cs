using Godot;

public class UserInterface : Control
{

    #region Field Declaration

    // Children
    private Label textPoints;
    private Button newGameButton;
    private Label textGameOver;

    [Signal] delegate void ButtonNewGamePressed();

    private int points;

    #endregion

    public override void _Ready()
    {
        textPoints = GetChild<Label>(0);
        newGameButton = GetChild<Button>(1);
        textGameOver = GetChild<Label>(2);

        points = 0;
    }

    private void IncreasePoints()
    {
        points ++;
        textPoints.Text = points.ToString();
    }

    private void GameOver()
    {
        textPoints.Visible = false;
        newGameButton.Visible = true;
        textGameOver.Visible = true;
        points = -1;
        IncreasePoints();
    }

    void OnNewGameButtonPressed()
    {
        textPoints.Visible = true;
        newGameButton.Visible = false;
        textGameOver.Visible = false;
        EmitSignal("ButtonNewGamePressed");
    }
    
}
