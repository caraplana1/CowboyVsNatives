using Godot;
using System;

public class UserInterface : Control
{

    #region Field Declaration

    private Label textPoints;
    private int points;

    #endregion

    public override void _Ready()
    {
        textPoints = GetChild<Label>(0);
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
    }
    
}
