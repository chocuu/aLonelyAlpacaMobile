using UnityEngine;

public enum MusicLevel : byte {OFF, HALF, FULL};


public class PlayerSettingsController : MonoBehaviour
{
    public static PlayerSettingsController instance;

    // Called once at startup
    private void Awake() 
    {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }    
    }


    #region Sound
    private static MusicLevel currSoundLevel = MusicLevel.FULL;
    public static MusicLevel CurrSoundLevel{
        get {
            return currSoundLevel;
        }
        set { 
            currSoundLevel = value;
            UseLights = !UseLights;
            switch (value) {
                case MusicLevel.FULL:
				    AudioListener.volume = 1f;
				    break;
                case MusicLevel.HALF:
                    AudioListener.volume = 0.5f;
                    break;
                case MusicLevel.OFF:
                    AudioListener.volume = 0f;
                    break;
            }
        }
    } 
    #endregion // Sound


    #region Arrows
    private static bool useArrows = true;
    public static bool UseArrows{
        get {
            return useArrows;
        }
        set {
            if(GameObject.FindGameObjectsWithTag("WORLD").Length > 0) {
                useArrows = value;
                GameObject.FindGameObjectsWithTag("WORLD")[0].GetComponent<ArrowQuadrantsController>().SetUseArrows(value);
            }
        }
    }
    #endregion // Arrows


    #region Lights
    private static bool useLights = true;
    public static bool UseLights{
        get {
            return useLights;
        }
        set {
            if(GameObject.FindGameObjectsWithTag("WORLD").Length > 0) {
                useLights = value;
                GameObject.FindGameObjectsWithTag("WORLD")[0].GetComponent<EnvironmentController>().SetUseLights(value);
            }
        }
    }
    #endregion // Lights


}
