using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    // private static float[] worstScoreTimes = new[] {};
    
    // private static float[] bestScoreTimes = new[] {};

    private static int[] bestScoreNumMoves = new[] {
                                                    999,
                                                    999,
                                                    999,
                                                    999,
                                                    10,
                                                    46,
                                                    15,
                                                    24,
                                                    116,
                                                    104,
                                                    62,
                                                    9,
                                                    18,
                                                    35,
                                                    48,
                                                    50,
                                                    56,
                                                    25,
                                                    144,
                                                    65,
                                                    205,
                                                    47,
                                                    98,
                                                    114,
                                                    129
                                                    };

    private float levelStartTime;

    private int numMovesMade;
    
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        levelStartTime = Time.time; 
        numMovesMade = 0;
    }

    public void incrementNumMoves()
    {
        numMovesMade++;
    }

    /** Locally save the scores if they were the best achieved by this player */
    private void saveBestStatsIfApplicable(float timeInLevel, int numMoves, float totalScore, int levelNumber)
    {
        // save best time
        string bestTime = "Level" + levelNumber + "BestTime";
        if(!PlayerPrefs.HasKey(bestTime)){
            PlayerPrefs.SetFloat(bestTime, timeInLevel);
        }
        else if(timeInLevel < PlayerPrefs.GetFloat(bestTime)){
            PlayerPrefs.SetFloat(bestTime, timeInLevel);
        }

        // save best move count
        string bestNumMovesMade = "Level" + levelNumber +  "BestNumMovesMade";
        if(!PlayerPrefs.HasKey(bestNumMovesMade)){
            PlayerPrefs.SetInt(bestNumMovesMade, numMovesMade);
        }
        else if(numMovesMade < PlayerPrefs.GetInt(bestNumMovesMade)){
            PlayerPrefs.SetInt(bestNumMovesMade, numMovesMade);
        }

        // save best score
        string bestScore = "Level" + levelNumber +  "BestScore";
        if(!PlayerPrefs.HasKey(bestScore)){
            PlayerPrefs.SetFloat(bestScore, totalScore);
        }
        else if(numMovesMade < PlayerPrefs.GetInt(bestScore)){
            PlayerPrefs.SetFloat(bestScore, totalScore);
        }
    }

    /** Get a score for level performance.
      * Score will be a value between 0.5 and 6.0, rounded to the nearest 0.5
      * Only call upon level completion.
    */
    public void processFinalScore(int level)
    {
        int numMovesBestScore = bestScoreNumMoves[level-1];
        // moves for worst score is 40% more than best score
        int numMovesWorstScore = (int)Mathf.Ceil(numMovesBestScore *1.4f);

        float timeAtLevelFinish = Time.time - levelStartTime;
        timeAtLevelFinish = Mathf.Round(timeAtLevelFinish * 100f)/100f; // Round to 2 decimal places

        // Give a normalized score, 1 is the worst, 0 is the best.
        float numMovesScore = numMovesMade <= numMovesBestScore ? 0.0f : 
                            (numMovesMade >= numMovesWorstScore ? 1.0f : 
                            ((float)(numMovesMade-numMovesBestScore))/(numMovesWorstScore-numMovesBestScore));

        // Score based only on numMoves
        float totalScore = 5.5f * (1.0f-numMovesScore) + 0.5f;
        totalScore = Mathf.Round(totalScore * 2) * 0.5f; // round to nearest 0.5

        saveBestStatsIfApplicable(timeAtLevelFinish, numMovesMade, totalScore, level);  
    }

}
