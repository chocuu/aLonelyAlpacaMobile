using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Anonym.Isometric;

public class EnvironmentController : MonoBehaviour
{    
    private Map mp; // reference to the worldscript's map object
    private bool lightsLoaded; // Whether lights were enabled when the scene was started.
    
    // Base values for the unique priority field for different types of lights
    private enum LightPriorityBases : int {
        CLOUD = 0,
        WINBLOCK = CLOUD + MAX_CLOUDS,
        MAGMA = WINBLOCK + 1,
    };


    public void SetMap(ref Map m)
    {
        if(m != null) {
            this.mp = m;
        }
    }


    private void Awake() {
        lightsLoaded = PlayerSettingsController.UseLights;
    }


    // Update is called once per frame
    void Update()
    {
        if(PlayerSettingsController.UseLights) {
            if(!cloudWaiting) {
                StartCoroutine(RollForCloud());
            }
        }
    }


    /** Called by PlayerSettingsController **/
    public void SetUseLights(bool set)
    {
        if(!set) { /** turn lights off **/
            // Get rid of all clouds
            foreach(GameObject c in cloudObjects) {
                if(c != null) {
                    Destroy(c);
                }
            }
            // Magma Lights
            if(magmaLights != null) {
                foreach(IsoLight l in magmaLights) {
                    l.RemoveTarget_All();
                    l.enabled = false;
                }
            }
            // Star Light
            if(starLight != null) {
                starLight.RemoveTarget_All();
                starLight.enabled = false;
            }
        }
        else { /** turn lights on **/
            // lights are already in the scene, just enable them
            if(lightsLoaded) { 
                // Magma Lights
                if(magmaLights != null) {
                    foreach(IsoLight l in magmaLights) {
                        l.enabled = true;
                        l.AddTarget_AllWithinRadius_Cached_Dynamic(magmaLightsTargetRadius, this);
                    }
                }
                // Star Light
                if(starLight != null) {
                    starLight.enabled = true;
                    starLight.AddTarget_AllWithinRadius_Cached_Dynamic(starLightsTargetRadius, this);
                }
            }
            // lights dont exist yet, need to make them
            else {
                // Magma Lights
                if(magmaLights != null) {
                    foreach(Block b in magmaBlocks) {
                        IsoLight l = CreateMagmaLight(b.coord_obj.transform);
                        magmaLights.Add(l); // add magma light to list of magma lights
                    }
                }
                // Star Light
                if(starBlock != null) {
                    starLight = CreateStarLight(starBlock.coord_obj.transform);
                }
                lightsLoaded = true;
            }
        }
    }

    // ============================================================================================================== //
    #region Magma Lights


    [Header("Magma Lights Settings")]
    [SerializeField] private GameObject magmaLightsPrefab;
    [SerializeField] private float magmaLightsTargetRadius; // how far from the magma light the light should affect

    // Internal data
    private List<Block> magmaBlocks = new List<Block>(); // List of all magma lights in the scene
    private List<IsoLight> magmaLights = new List<IsoLight>(); // List of all magma lights in the scene
    [HideInInspector] public bool areLightsThisLevelCached; // Whether the IsoLightRecivers have been cached or not
    [HideInInspector] public IEnumerable<IsoLightReciver> allRecivers; // Cached list of all light recivers inthe entire scene
    [HideInInspector] public IEnumerable<IsoLightReciver> allBlocklessReceiversInScene; // Cached list of all light recivers w/ no ISO2Dbase object in scene
    [HideInInspector] public IEnumerable<IsoLightReciver> allInteractiveRecivers; // Cached list of all light recivers belonging to interactive blocks


    /** Add an IsoLight object as a child of the argument magma block, if one doesn't exist already.
        IsoLights only add receivers within a radius as adding too many receivers for each block introduces significant lag
        Called on block construction in Map.cs / GridCoordinates.cs Start() **/
    public void AddLightsToMagmaBlock(Block b)
    {
        if(b == null) return;
        magmaBlocks.Add(b);

        Transform existingLight = b.coord_obj.transform.Find("Magma Light(Clone)");
        if(PlayerSettingsController.UseLights) {
            // Get the existing light or make a new one
            IsoLight l = (existingLight == null) ? CreateMagmaLight(b.coord_obj.transform) : existingLight.GetComponent<IsoLight>();
            // add magma light to list of magma lights
            magmaLights.Add(l);
        }
        else if(existingLight != null) { // Disable existing magma lights, but keep them in the list
            IsoLight l = existingLight.GetComponent<IsoLight>();
            l.RemoveTarget_All();
            l.enabled = false;
            magmaLights.Add(l);
        }
    }


    /** instantiate light object as a child of the magma block, set its priority and targets **/
    private IsoLight CreateMagmaLight(Transform parent)
    {
        IsoLight l = UnityEngine.Object.Instantiate(magmaLightsPrefab, parent).GetComponent<IsoLight>();
        l.UniquePriority = (int)LightPriorityBases.MAGMA + magmaLights.Count;
        l.AddTarget_AllWithinRadius_Cached_Dynamic(magmaLightsTargetRadius, this); // target all blocks within a radius + receivers w/ no block (e.g. alpaca) + allinteractable blocks
        return l;
    }


    #endregion
    // ============================================================================================================== //
    #region Star Lights


    [Header("Star Lights Settings")]
    [SerializeField] private GameObject starLightsPrefab;
    [SerializeField] private float starLightsTargetRadius; // how far from the magma light the light should affect

    // Internal data
    private Block starBlock;
    private IsoLight starLight;


    public void AddLightsToStarBlock(Block b)
    {
        if(b == null) return;
        starBlock = b;

        Transform existingLight = b.coord_obj.transform.Find("Star Light(Clone)");
        if(PlayerSettingsController.UseLights) {
            // Get the existing light or make a new one
            starLight = (existingLight == null) ? CreateStarLight(b.coord_obj.transform) : existingLight.GetComponent<IsoLight>();
        }
        else if(existingLight != null) { // Disable existing light
            starLight = existingLight.GetComponent<IsoLight>();
            starLight.RemoveTarget_All();
            starLight.enabled = false;
        }
    }


    /** instantiate light object as a child of the magma block, set its priority and targets **/
    private IsoLight CreateStarLight(Transform parent)
    {
        IsoLight l = UnityEngine.Object.Instantiate(starLightsPrefab, parent).GetComponent<IsoLight>();
        l.UniquePriority = (int)LightPriorityBases.WINBLOCK;
        l.AddTarget_AllWithinRadius_Cached_Dynamic(starLightsTargetRadius, this); // target all blocks within a radius + receivers w/ no block (e.g. alpaca) + allinteractable blocks
        return l;
    }


    #endregion
    // ============================================================================================================== //
    #region Cloud Generation


    [Header("Cloud Generation Settings")]
    [SerializeField] private GameObject cloudPrefab;
	[SerializeField, Range(0f, 1f)] private float cloudProbability;
    [SerializeField] private float cloudRollPeriod;

    // Internal data
    private bool cloudWaiting;
    private const int MAX_CLOUDS = 3;
    private List<GameObject> cloudObjects = new List<GameObject>(MAX_CLOUDS);


    /** Roll for whether or not a cloud should be spawned **/
    private IEnumerator RollForCloud()
    {
        cloudWaiting= true;
        
        cloudObjects.RemoveAll(obj => obj == null); // clear despawned clouds from cloud list
        
        if(Random.Range(0, 1f) < cloudProbability && cloudObjects.Count < MAX_CLOUDS) {
            //Instantiate cloud object
            GameObject cloud = Instantiate(cloudPrefab);

            // Set cloud light priority and targets
            IsoLight l = cloud.GetComponent<IsoLight>();
            l.UniquePriority = (int)LightPriorityBases.CLOUD + cloudObjects.Count;
            l.AddTarget_All();

            // let cloud begin moving
            MoveToTarget temp = cloud.GetComponent<MoveToTarget>();
            temp.destroyOnArrival = true;
            temp.go = true;

            // track cloud /add to count
            cloudObjects.Add(cloud);
        }

        yield return new WaitForSeconds(cloudRollPeriod);
        cloudWaiting = false;
    }


    #endregion
    // ============================================================================================================== //
}
