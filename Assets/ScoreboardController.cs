using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    // private static float[] worstScoreTimes = new[] {};
    
    // private static float[] bestScoreTimes = new[] {};
    
    private static int[] worstScoreNumMoves = new[] {
                                                    999,
                                                    999,
                                                    999,
                                                    999
                                                    };

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
        Debug.Log(numMovesMade);
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
        // float timeForWorstScore = worstScoreTimes[level-1];
        // float timeForBestScore = bestScoreTimes[level-1];
        int numMovesBestScore = bestScoreNumMoves[level-1];
        int numMovesWorstScore = worstScoreNumMoves[level-1];

        float timeAtLevelFinish = Time.time - levelStartTime;
        timeAtLevelFinish = Mathf.Round(timeAtLevelFinish * 100f)/100f; // Round to 2 decimal places

        // Give a normalized score for each stat, 1 is the worst, 0 is the best.
        // assert that XForWorstScore > XForBestScore always
        // float timeScore = timeAtLevelFinish <= timeForBestScore ? 0.0f : 
        //                     (timeAtLevelFinish >= timeForWorstScore ? 1.0f : 
        //                     (timeAtLevelFinish-timeForBestScore)/(timeForWorstScore-timeForBestScore));
        
        float numMovesScore = numMovesMade <= numMovesBestScore ? 0.0f : 
                            (numMovesMade >= numMovesWorstScore ? 1.0f : 
                            ((float)(numMovesMade-numMovesBestScore))/(numMovesWorstScore-numMovesBestScore));

        // Score based only on numMoves
        float totalScore = 5.5f * (1.0f-numMovesScore) + 0.5f;
        totalScore = Mathf.Round(totalScore * 2) * 0.5f; // round to nearest 0.5

        saveBestStatsIfApplicable(timeAtLevelFinish, numMovesMade, totalScore, level);  
    }

}
