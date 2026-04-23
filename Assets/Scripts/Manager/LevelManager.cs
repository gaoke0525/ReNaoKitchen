using UnityEngine;

public static class LevelManager
{
    private static int currentLevel = 1;

    private static int[] levelTargets = { 6, 8, 10 };

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }

    public static int GetTargetScore()
    {
        int index = currentLevel - 1;
        if (index < levelTargets.Length)
        {
            return levelTargets[index];
        }
        return levelTargets[levelTargets.Length - 1] + (currentLevel - levelTargets.Length) * 2;
    }

    public static bool IsLevelPassed(int totalScore)
    {
        return totalScore >= GetTargetScore();
    }

    public static void GoToNextLevel()
    {
        currentLevel++;
    }

    public static void ResetToFirstLevel()
    {
        currentLevel = 1;
    }
}