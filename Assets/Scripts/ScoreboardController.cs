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

    private float timeAtLevelFinish;

    private int numMovesMade;

    private int score;
    
    private string sceneName;

    private string bestTimeKey;

    private string bestNumMovesMadeKey;

    private string bestScoreKey;

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

    private string floatToTimeString(float time) {
        string min = ((int) (time/60)).ToString();
        string sec = ((int) (time % 60)).ToString();
        while (sec.Length < 2) sec = "0" + sec;
        string mil = ((int) ((time * 100) % 100)).ToString();
        while (mil.Length < 2) mil = "0" + mil;
        string str = min + ":" + sec + ":" + mil;
        if(min.Length > 3)
            str = min + ":" + sec;
        else if(min.Length > 7)
            str = min;
        return str;
    }

    public string getTotalTime() {
        return floatToTimeString(timeAtLevelFinish);
    }

    public string getNumMovesMade() {
        return numMovesMade.ToString();
    }

    public int getScore() {
        return score;
    }

    public string getBestTotalTime() {
        return floatToTimeString(PlayerPrefs.GetFloat(bestTimeKey));
    }

    public string getBestNumMovesMade() {
        return PlayerPrefs.GetInt(bestNumMovesMadeKey).ToString();
    }

    public int getBestScore() {
        return PlayerPrefs.GetInt(bestScoreKey);
    }

    /** Locally save the scores if they were the best achieved by this player */
    private void saveBestStatsIfApplicable(float timeInLevel, int numMoves, int totalScore, int levelNumber)
    {
        // save best time
        bestTimeKey = "Level" + levelNumber + "BestTime";
        if(!PlayerPrefs.HasKey(bestTimeKey)){
            PlayerPrefs.SetFloat(bestTimeKey, timeInLevel);
        }
        else if(timeInLevel < PlayerPrefs.GetFloat(bestTimeKey)){
            PlayerPrefs.SetFloat(bestTimeKey, timeInLevel);
        }

        // save best move count
        bestNumMovesMadeKey = "Level" + levelNumber +  "BestNumMovesMade";
        if(!PlayerPrefs.HasKey(bestNumMovesMadeKey)){
            PlayerPrefs.SetInt(bestNumMovesMadeKey, numMovesMade);
        }
        else if(numMovesMade < PlayerPrefs.GetInt(bestNumMovesMadeKey)){
            PlayerPrefs.SetInt(bestNumMovesMadeKey, numMovesMade);
        }

        // save best score
        bestScoreKey = "Level" + levelNumber +  "BestScore";
        if(!PlayerPrefs.HasKey(bestScoreKey)){
            PlayerPrefs.SetInt(bestScoreKey, totalScore);
        }
        else if(numMovesMade < PlayerPrefs.GetInt(bestScoreKey)){
            PlayerPrefs.SetInt(bestScoreKey, totalScore);
        }
    }

    /** Calculates a score for level performance. Saves this score if it is the best achieved for this level.
      * Score will be a value from 1 and 6
      * Only call upon level completion.
    */
    public void processFinalScore(int level)
    {
        int numMovesBestScore = bestScoreNumMoves[level-1];
        // moves for worst score is 40% more than best score
        int numMovesWorstScore = (int)Mathf.Ceil(numMovesBestScore *1.8f);

        // Calculate time at level finish in milliseconds
        timeAtLevelFinish = Time.time - levelStartTime;
        timeAtLevelFinish = Mathf.Round(timeAtLevelFinish * 100f)/100f; // Round to 2 decimal places

        // Give a normalized score, 1 is the worst, 0 is the best.
        float numMovesScore = numMovesMade <= numMovesBestScore ? 0.0f : 
                            (numMovesMade >= numMovesWorstScore ? 1.0f : 
                            ((float)(numMovesMade-numMovesBestScore))/(numMovesWorstScore-numMovesBestScore));

        // Score based only on numMoves
        score = (int)Mathf.Round(5.0f * (1.0f-numMovesScore) + 1.0f);
        saveBestStatsIfApplicable(timeAtLevelFinish, numMovesMade, score, level);  
    }

}
