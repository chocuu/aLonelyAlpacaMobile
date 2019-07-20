using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Anonym.Isometric;
using UnityEngine.UI;

/**
 * Model / controller for the alpaca. Handles animation, falling,
 * picking & dropping blocks, and handling death.
 */
public class Alpaca : MonoBehaviour {

	float dest_y = -100; // destined height (y coord), -100 if not falling
	bool squash = false; // will squash when reach destination
	bool dead = false; // whether alpaca be dead
	
	private bool isMoving = false; 
	private Vector3 movingDest; // where alpaca is being sent if it's moving
	private float moveSpeed = 12; // how fast alpaca moves

	const float OFFSET = 0.23f; // sketchy offset that you shift alpaca down for
	public AudioSource landSound;
	public AudioSource popSound;

	// Use this for initialization
	void Start () {
        if(GameObject.FindGameObjectsWithTag("WORLD").Length > 0) {
            WorldScript world = GameObject.FindGameObjectsWithTag("WORLD")[0].GetComponent<WorldScript>();
            world.AddAlpaca(this);
        }

        if(GameObject.Find("MusicTime") != null)
        	music = GameObject.Find("MusicTime").GetComponent<AudioSource>();

    	if (music != null) 
			music.volume = 0.3f;

		dest_y = -100;

		Vector3 tmp = GetCurrAlpacaLocation();
		tmp.y -= OFFSET;
		gameObject.transform.position = tmp;
	}
	
	// Update is called once per frame
	void Update () {
		// fall if not at destinated height yet
		if(dest_y != -100 && GetY() > dest_y) {
			Vector3 coords = gameObject.transform.position;
			coords.y -= Time.deltaTime * 15;
			// Debug.Log("falling " + coords.y);
			if(coords.y <= dest_y) {
				coords.y = dest_y;
				dest_y = -100;
				SetFalling(false);
				if(squash) {
					SetSquashed();
					squash = false;
				} else {
					landSound.Play();
				}
			} else {
				SetFalling(true);
			}
			gameObject.transform.position = coords;
		} else if(dest_y != -100 && GetY() - OFFSET - 0.05f <= dest_y) {
			Vector3 coords = gameObject.transform.position;
			coords.y = dest_y;
			gameObject.transform.position = coords;
			dest_y = -100;
			SetFalling(false);
			if(squash) {
				SetSquashed();
				squash = false;
			}
		}
	
		// Smooth Movement
		if(isMoving){
			transform.position = Vector3.MoveTowards(transform.position, movingDest, moveSpeed * Time.deltaTime);
			if(Vector3.Distance(transform.position, movingDest) <= 0.005){
				transform.position	= movingDest;
				isMoving = false;
			}
		}
	}

	/**
	 * Get current alpaca position
	 */
	public Vector3 GetCurrAlpacaLocation()
    {
    	Vector3 coords = gameObject.transform.position;
		if(isMoving) {
			coords.x = movingDest.x;
			coords.y = movingDest.y;
			coords.z = movingDest.z;	
		} else {
			coords.x = (float)Mathf.Round(coords.x);
			coords.y = (float)Mathf.Round(coords.y);
			coords.z = (float)Mathf.Round(coords.z);
		}
    	return coords;
    }

    /**
	 * Return the block vecotr in front of the alpaca (in direction its facing)
	 *
	 * clickedWhere is where the alpaca is facing
	 */
	public Vector3 GetCurrAlpacaDest(int clickedWhere) 
	{
		Vector3 dest = GetCurrAlpacaLocation();
    	switch(clickedWhere) {
    		case 0:
    			dest.x--;
    			break;
    		case 1:
    			dest.z++;
    			break;
    		case 2:
    			dest.x++;
    			break;
    		case 3:
    			dest.z--;
    			break;
    		default:
    			//Debug.Log("incorrect clickedWhere in curralpacadest call");
    			return dest;
    	}
    	return dest;
	}

    private float GetY() {
    	return gameObject.transform.position.y + 0.3f;
    }

    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = STATE
    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    /**
     * Move alpaca to this block
     *
     * @ param {direction} coordinate of where alpaca goes
     */
    public void Move(Vector3 dir)
    {
    	// direction.y -= 0.2f;
    	Vector3 coords = GetCurrAlpacaLocation();
    	if(dir.y < coords.y) {
    		if(coords.y - dir.y > 2.5) 
    			squash = true;
			gameObject.transform.position =  new Vector3(dir.x, coords.y, dir.z);
			dest_y = dir.y - OFFSET;
		} else {
			UpdateWalk();
			isMoving = true;
			dir.y -= OFFSET;
			movingDest = dir;
			//gameObject.transform.position = dir;
		}
    }

    /**
     * Returns whether alpaca is dead :(
     */
    public bool IsDead() {
    	return dead;
    }
    
    bool lastBlock; // true iff alpaca is holding a block

    public void SetBlock(bool has) {
    	popSound.Play();
    	animator.SetBool("poof", has);
		animator.SetBool("is_blockpaca", has);
		//animator.SetBool("drop_block", !has); //does not exist
		lastBlock = has;
    }

    public bool HasBlock() {
    	return lastBlock;
    }

    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = ANIMATION
    // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    public Animator animator; // alpaca animator
    private AudioSource music; // background music (reference to change volume if dead)
    public Image deathImage; // death banner image
    public AudioSource deathSong; // death audio

    public int dir = 2; // direction alpaca is facing, set SetFacingDirection for more
    					// also used for block tutorial

    /**
     * View stuff for death (draw death banner & play death music & soften bg music)
     */
    private void Died() {
    	dead = true;
		deathImage.enabled = true;
		if (!deathSong.isPlaying)
		{
			if (music != null) 
				music.volume = 0.005f;
			deathSong.Play();
		}
    }

    /**
     * View stuff for when alpaca is squashed
     */
    public void SetSquashed() {
    	animator.SetBool("walkse", false);
		animator.SetBool("walksw", false);
		animator.SetBool("walknw", false);
		animator.SetBool("walkne", false);
		animator.SetBool("death_by_fire", false);
		animator.SetBool("death_by_splat", true);
		animator.SetBool("dead", true);

		Died();
    }

    public void SetFlamed() {
    	//Debug.Log("death by fire");
    	animator.SetBool("walkse", false);
		animator.SetBool("walksw", false);
		animator.SetBool("walknw", false);
		animator.SetBool("walkne", false);
		animator.SetBool("death_by_splat", false);
		animator.SetBool("death_by_fire", true);
		animator.SetBool("dead", true);

    	Died();
    }

    /**
     *  -----------
	 * |  0  |  1  |
	 * |-----------
	 * |  3  |  2  |
	 *  -----------
     */
    public void SetFacingDirection(int dir) {
    	if(dir == -1) return;
		this.dir = dir;
    }

    public void StopWalk() {
    	animator.SetBool("poof", false);
		animator.SetBool("walkse", false);
		animator.SetBool("walksw", false);
		animator.SetBool("walknw", false);
		animator.SetBool("walkne", false);
    }

    public void UpdateWalk() {
		if (dir == 1 && !animator.GetBool("walkne"))
		{
			animator.SetBool("walkse", false);
			animator.SetBool("walksw", false);
			animator.SetBool("walknw", false);
			animator.SetBool("walkne", true);
		}

		//SW
		if (dir == 3 && !animator.GetBool("walksw"))
		{
			animator.SetBool("walkse", false);
			animator.SetBool("walksw", true);
			animator.SetBool("walknw", false);
			animator.SetBool("walkne", false);
		}

		//NW
		if (dir == 0 && !animator.GetBool("walknw"))
		{
			animator.SetBool("walkse", false);
			animator.SetBool("walksw", false);
			animator.SetBool("walknw", true);
			animator.SetBool("walkne", false);
		}

		//SE
		if (dir == 2 && !animator.GetBool("walkse"))
		{
			animator.SetBool("walkse", true);
			animator.SetBool("walksw", false);
			animator.SetBool("walknw", false);
			animator.SetBool("walkne", false);
		}
    }

    public void SetFalling(bool set) {
		animator.SetBool("walkse", false);
		animator.SetBool("walksw", false);
		animator.SetBool("walknw", false);
		animator.SetBool("walkne", false);
		animator.SetBool("is_falling", set);
    }

}

