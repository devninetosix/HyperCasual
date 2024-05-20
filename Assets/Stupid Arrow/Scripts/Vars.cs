using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vars : MonoBehaviour
{
    public static int currentMenu = 0; //0 - main menu, 1 - gameplay menu, 2 - reply
    public static int mainMenuCircles = 15;
    public static bool startGame = false;
    public static int obstacle = 0;
    public static int numberOfCircles = 15;
    public static int score = 0;
    public static float obstacleScaleSpeed = 0;

    public static void Reset()
    {
        mainMenuCircles = 15;
        startGame = false;
        obstacle = 0;
        numberOfCircles = 15;
        score = 0;
        obstacleScaleSpeed = 0;
    }
}