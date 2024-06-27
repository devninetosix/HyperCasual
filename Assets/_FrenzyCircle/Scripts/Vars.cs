using UnityEngine;


namespace FrenzyCircle
{
    public class Vars : MonoBehaviour
    {
        public static int CurrentMenu = 0; //0 - main menu, 1 - gameplay menu, 2 - reply
        public static int MainMenuCircles = 15;
        public static bool StartGame;
        public static int Obstacle;
        public static int NumberOfCircles = 15;
        public static int Score;
        public static float ObstacleScaleSpeed;

        public static void ResetAll()
        {
            MainMenuCircles = 15;
            StartGame = false;
            Obstacle = 0;
            NumberOfCircles = 15;
            Score = 0;
            ObstacleScaleSpeed = 0;
        }
    }
}