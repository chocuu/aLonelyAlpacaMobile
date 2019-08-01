using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;
using Anonym.Isometric;

/**
 * The ~ all foreseeing ~ control script for every playable level.
 * Contains a model of the currentl level map, model of the alpaca,
 * and is in charge of processing user movements.
 */
public class WorldScript : MonoBehaviour {

	private int numberOfLevels = 28;
    public AudioSource winSound;
    public AudioSource jumpSound;
	// Reference to the canvas's pan controller; used to switch controls if in pan mode
	private PanButtonController pan_ctrlr;
	Map map;
	Alpaca alpaca;
	// used to highlight four quadrants
	public GameObject quadrant_0, quadrant_1, quadrant_2, quadrant_3;
	private Image[] quadrants;
	private BlockButt blockButt;

	/**
	 * 0 = hold in quadrant to drop/pick up
	 * 1 = hold anywhere to drop/pick up in facing direction
	 * 2 = click icon to hold/drop
	 */
	private int control_scheme = 0;

	// Use this for initialization
	void Start () {
		if(map == null) {
			map = new Map(100, 100);
		}
		quadrants = new Image[4]{quadrant_0.GetComponent<Image>(), 
										quadrant_1.GetComponent<Image>(), 
										quadrant_2.GetComponent<Image>(), 
										quadrant_3.GetComponent<Image>()};
		quadrants[0].enabled = false;
		quadrants[1].enabled = false;
		quadrants[2].enabled = false;
		quadrants[3].enabled = false;

		clickedWhere = lastClickedWhere = 2;
		pan_ctrlr = GameObject.Find("Pan Butt").GetComponent<PanButtonController>();
	}

	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = QUADRANT HIGHLIGHTING
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

	/**
	 * Remove all highlights from screen
	 */
	void ClearHighlights() {
		quadrants[0].enabled = false;
		quadrants[1].enabled = false;
		quadrants[2].enabled = false;
		quadrants[3].enabled = false;
    }

    /**
     * Makes the current click position highlighted
     */
    void HighlightQuadrant() {
    	// Debug.Log(clickedWhere);
    	ClearHighlights();
    	if(clickedWhere == -1) return;
    	quadrants[clickedWhere].enabled = true;
    }

	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = MODEL DECLARATION
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	
	/**
	 * Adds a block to the map. Every block declares itself when its GridCoordinates 
	 * object is initialized (Start() method).
	 * 
	 * @param {name} Name of block
	 * @param {last} Previous coordinate of block, if existed
	 * @param {coords} Location of block
	 */
	public void AddBlock(string name, Vector3 last, Vector3 coords, GridCoordinates obj) {
		if(map == null) {
			map = new Map(100, 100);
		}
		map.AddBlock(name, last, coords, obj);
	}

	/**
	 * Adds the alpaca model to world. Alpaca object declares itself in its constructor.
	 * 
	 * @param {a} Alpaca
	 */
	public void AddAlpaca(Alpaca a) {
		if(map == null) {
			map = new Map(100, 100);
		}
		alpaca = a;
	}

	public void AddBlockButt(BlockButt b) {
		if(map == null) {
			map = new Map(100, 100);
		}
		blockButt = b;
	}

	// Update is called once per frame
	void Update () {
		ProcessCurrBlock();
		ProcessInput();
	}

	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  BLOCK PROCESSING
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

	/**
	 * Get the block below the given location.
	 */
	Block GetBlockBelow(Vector3 loc) {
		if(loc == null || map == null) return null;
		loc.y--;
		loc.y = (float)Math.Ceiling(loc.y);
		return map.GetBlock(loc);
	}

	/**
	 * Get the block at the given location.
	 */
	Block GetBlockAt(Vector3 loc) {
		return map.GetBlock(loc);
	}

	/**
	 * Get the block above the given location.
	 */
	Block GetBlockAbove(Vector3 loc) {
		loc.y++;
		loc.y = (float)Math.Ceiling(loc.y);
		return map.GetBlock(loc);
	}

	float end_timer = 0; // used to delay level transition before wind sound plays

	/**
	 * Checks what block the alpaca is on currently and handles its logic.
	 * (Dies for lava and wins for win block.)
	 */
	void ProcessCurrBlock() {
		Block currBlock = GetBlockBelow(alpaca.GetCurrAlpacaLocation());
		if(currBlock == null) {
			return;
		}
		switch(currBlock.b_type) {
			case Block.BlockType.LAVA:
				alpaca.SetFlamed();
				break;
			case Block.BlockType.WIN:
				if(end_timer == 0) {
					winSound.Play();
				}
				end_timer += Time.deltaTime;
				if(end_timer > 0.2f) {
					int level = int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value);
					if(PlayerPrefs.GetInt("LevelPassed") < level) {
						PlayerPrefs.SetInt("LevelPassed", level);
					}
					if(end_timer < 100f) {
						end_timer = 999f;
						if(level != numberOfLevels)
							SceneManager.LoadSceneAsync("B" + (level+1), LoadSceneMode.Single);
					}
					else {
						FinalWinBlockController final = gameObject.GetComponent<FinalWinBlockController>();
						if(final != null) final.BeatFinalLevel();
						currBlock.b_type = Block.BlockType.NONE; // stop processing this block
					}
				}
				break;
			case Block.BlockType.GRASS: 
			case Block.BlockType.MOVEABLE:
				// do nothing
				return;
			default:
				Debug.Log("Alpaca is on a none block!");
				return;
		}
	}

	Block highlighted; // block highlighted if you're holding a block

	void HandleFrontBlockHighlight() {
		if( lastClickedWhere != clickedWhere) {
			alpaca.UpdateWalk();
			return;
		}
		if(highlighted != null)
			highlighted.Unhighlight();
		if(!alpaca.HasBlock())
			return;

    	Vector3 dest = alpaca.GetCurrAlpacaDest(clickedWhere);

		highlighted = map.GetHighestBlockBelow(dest);
		if(highlighted != null && GetBlockAt(dest) == null) {
			highlighted.Highlight();
		}
	}

	/**
	 * Changes the alpaca's coordinates depending on the click location, and
	 * the surrounding blocks.
	 */
	void MoveOnClick() {
		// change facing direction before walking in that direction
		if( lastClickedWhere != clickedWhere) {
			alpaca.UpdateWalk();
			lastClickedWhere = clickedWhere;
			return;
		}
    	Vector3 curr = alpaca.GetCurrAlpacaLocation();
    	Vector3 dest = alpaca.GetCurrAlpacaDest(clickedWhere);

		if(GetBlockAt(dest) != null) { // Is there a block right in front? --> climb mode
			if(GetBlockAbove(curr) != null) // Is there a block above alpaca?
				return;
			else {
				if(GetBlockAbove(dest) != null) // Is there a block the one right in front?
					return;
				else if(GetBlockAt(dest).b_type != Block.BlockType.WALL ) { // Is the block one banning walking?
					jumpSound.Play();
					dest.y++;
					alpaca.Move(dest);
				}
			}
		} else {
			if(GetBlockBelow(dest) != null) { // Is there a block that can walk on straight?
				if(GetBlockBelow(dest).b_type != Block.BlockType.WALL)	// Is it a block to not walk on? --> don't move
					alpaca.Move(dest);
			} else {
				Block top = map.GetHighestBlockBelow(dest);
				if(top != null && top.b_type != Block.BlockType.WALL) { // Is there a block alpaca can fall on?
					dest = top.getCoords();
					dest.y++;
					alpaca.Move(dest);
				}
			}
		}
		
		lastClickedWhere = clickedWhere;
    }

    public void FlagControlScheme(Text t) {
    	if(control_scheme == 0)
    		control_scheme = 1;
    	else if(control_scheme == 1) {
    		control_scheme = 2;
    	} else {
    		control_scheme = 0;
    		blockButt.SetColor(new Color(1, 1, 1, 0));
    	}
    	t.text = control_scheme.ToString();
    }

    public void ClickBlockButt() {
    	AttemptPickUpOrPlaceBlock();
    }

    void UpdateBlockButt() {
    	if(blockButt == null) return;

    	if(map.IsBlockHeld()) {
    		blockButt.SetColor(new Color(1, 1, 1, 1));
			return;
    	}

    	Vector3 curr = alpaca.GetCurrAlpacaLocation();
    	Vector3 dest = alpaca.GetCurrAlpacaDest(clickedWhere);

		// Is there a block above attempted block?
    	if(GetBlockAbove(dest) != null && GetBlockAbove(dest).b_type == Block.BlockType.MOVEABLE) {
    		blockButt.SetColor(new Color(1, 1, 1, 0));
			return;
    	}
    	if(GetBlockAt(dest) != null && GetBlockAt(dest).b_type == Block.BlockType.MOVEABLE) {
    		blockButt.SetColor(new Color(1, 1, 1, 0.5f));
    	} else {
    		blockButt.SetColor(new Color(1, 1, 1, 0));
    	}
    }

    /**
     * Picks up a block if there's a moveable on in front,
     * or drops a block if the alpaca has one and there's a platform it can fall on.
     */
    bool AttemptPickUpOrPlaceBlock() {
    	Vector3 curr = alpaca.GetCurrAlpacaLocation();
    	Vector3 dest = alpaca.GetCurrAlpacaDest(clickedWhere);
    	lastClickedWhere = clickedWhere;
    	// Is there a block above attempted block?
    	if(GetBlockAbove(dest) != null && GetBlockAbove(dest).b_type == Block.BlockType.MOVEABLE) 
			return false;
    	bool temp = (map.TryHoldOrPlaceBlock(dest));
    	if(temp) {
    		alpaca.SetBlock(map.IsBlockHeld());
    	} else {
    		map.LoadTryHoldBlock(dest, false);
    	}
    	return temp;
    }

     /**
     * Picks up a block if there's a moveable on in front,
     * or drops a block if the alpaca has one and there's a platform it can fall on.
     */
    bool LoadTryHoldBlock(bool set) {
    	if(control_scheme == 2) return false;
    	Vector3 curr = alpaca.GetCurrAlpacaLocation();
    	Vector3 dest = alpaca.GetCurrAlpacaDest(clickedWhere);
    	lastClickedWhere = clickedWhere;
    	// Is there a moveable block above attempted block?
    	if(GetBlockAbove(dest) != null && GetBlockAbove(dest).b_type == Block.BlockType.MOVEABLE) 
			return false;
    	bool temp = (map.LoadTryHoldBlock(dest, set));
    	return temp;
    }

	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  INPUT PROCESSING
	// = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

	Vector2 clickPos; // Position of click on this update, if is clicking right now
	bool didClick = false; // Whether there was a click the last update (click start/end detection)
	float lastTimeClicked = 0; // Duration of a click, if currently active. 100+ otherwise.
	float tilPickup = 0; // Duration between loading picking up block & picking up block
	int clickedWhere = 2; // Currect facing direction of the alpaca
	bool get = false; // Whether or not in process of holding / dropping a block (for animation delay)
	int lastClickedWhere = 2; // used to check if should just change direction or walk
							  // see ClickedWhere() for more
	bool flag = true; // only start the block pick up animation once

	float death_timer = 0.5f; // Used to delay before you can click screen to restart after death

	/**
	 * Processes the input for this update. In charge of:
	 * 
	 * # GAME MODE
	 *  - Moving alpaca
	 * 	- Picking up/setting down blocks
	 * # PANNING MODE
	 *  - Panning, if in panning mode
	 */
    void ProcessInput() {
		if(pan_ctrlr!=null && !pan_ctrlr.getIsPanning() || pan_ctrlr==null) {
		///// GAME MODE /////
			if(alpaca.IsDead()) {
				death_timer -= Time.deltaTime;
				if(ClickedNow() && death_timer < 0) { // reset on click
					Debug.Log("reset on click");
					if(ClickedWhere() != -1)
						clickedWhere = ClickedWhere();
					SceneManager.LoadSceneAsync( SceneManager.GetActiveScene().name );
				}
				return;
			}

			// if in process of loading of holding/dropping a block,
			// don't process input
			if(get) {
				Debug.Log("holding");
				tilPickup += Time.deltaTime;
				if(tilPickup > 0.3f) { // timer reached, actually process
					alpaca.StopWalk();
					get = false;
					if(control_scheme == 2) return;
					AttemptPickUpOrPlaceBlock();
					lastTimeClicked = 999;
				}
				return;
			}

			if(!ClickedNow() && didClick) { // click just ended
				if(control_scheme == 1) {
					clickPos = Input.mousePosition;
					clickedWhere = ClickedWhere();
					HighlightQuadrant();
					alpaca.SetFacingDirection(clickedWhere);
					alpaca.UpdateWalk();
				}
				if(lastTimeClicked < 100) { //did not pick up block
					MoveOnClick();
					map.LoadTryHoldBlock(new Vector3(0,0,0), false);
				}
				lastTimeClicked = 0;
				ClearHighlights();
				if(control_scheme == 0 || control_scheme == 2)
					alpaca.StopWalk();
				flag = true;
			} else if(ClickedNow()) { // click is happening
				if(control_scheme == 0 || control_scheme == 2) {
					clickPos = Input.mousePosition;
					clickedWhere = ClickedWhere();
					HighlightQuadrant();
					alpaca.SetFacingDirection(clickedWhere);
					alpaca.UpdateWalk();
				}
				lastTimeClicked += Time.deltaTime;
				// attempt to pick up block after certain time
				if(flag && lastTimeClicked > 0.25f) { 
					map.PreviewBlock(alpaca.GetCurrAlpacaDest(clickedWhere));
					LoadTryHoldBlock(true);
					flag = false;
					get = true;
					tilPickup = 0;
				}
			}
			HandleFrontBlockHighlight();
			if(control_scheme == 2)
				UpdateBlockButt();
			didClick = ClickedNow();
		} else {
		///// PANNING MODE /////
			if(pan_ctrlr!=null && ClickedNow()) {
				clickPos = Input.mousePosition;
				clickedWhere = ClickedWhere();
				HighlightQuadrant();
				pan_ctrlr.MoveCamera(clickedWhere);
			}
		}
	}

	/**
	 * Returns true iff there was a click during this update.
	 */
	bool ClickedNow() {
		// check if is on ui button (this version works for mobile too)
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		
		return Input.GetMouseButton(0) && !(results.Count > 0) && end_timer == 0;	
	}

	/**
	 * Also used in Lvl1Tutorial
	 */
	// Used to determine which quadrant is clicked
	int padding = Screen.height / 12; // do not process if user clicks too close to boundary
    int middle_x = Screen.width / 2;
    int middle_y = Screen.height / 2;

    /**
     * Returns which quadrant the click this update was on.
     *  -----------
	 * |  0  |  1  |
	 * |-----------
	 * |  3  |  2  |
	 *  -----------
	 *
	 *  Returns-1 if too close to the boundary
     */
    int ClickedWhere() {
		if(clickPos.x < middle_x - padding) {
			if (clickPos.y < middle_y - padding) return 3;
			else if(clickPos.y > middle_y + padding) return 0;
		} else if(clickPos.x > middle_x + padding) {
			if (clickPos.y < middle_y - padding) return 2;
			else if(clickPos.y > middle_y + padding) return 1;
		}
		return -1;
    }
}

