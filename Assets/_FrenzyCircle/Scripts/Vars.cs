using UnityEngine;

public class Vars : MonoBehaviour
{
    public static int CurrentMenu = 0; //0 - main menu, 1 - gameplay menu, 2 - reply
    public static int MainMenuCircles = 12;
    public static bool StartGame;
    public static int Obstacle;
    public static int NumberOfCircles = 12;
    public static int Score;
    public static float ObstacleScaleSpeed;

    public static void ResetAll()
    {
        MainMenuCircles = 12;
        StartGame = false;
        Obstacle = 0;
        NumberOfCircles = 12;
        Score = 0;
        ObstacleScaleSpeed = 0;
    }
}