using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anonym.Isometric;


public class VolcanoAnimationController : MonoBehaviour {
	public float delay = 0f;
	public AudioSource volcanoExplosionSound;
	float speed = 0.50f; //how fast it shakes
	Vector3 pos;
	Animator animatorComponent;
	public GameObject player;
	public GameObject canvas;
	
	private bool soundPlayed = false;
	private float timeSinceSceneStart;

	void Start () {
		pos = new Vector3(0.71f, 0 ,0.71f);
		// Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
		// StartCoroutine(stopMovement());
		timeSinceSceneStart = 0;
		canvas.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		// start shaking, sound, and vibration after a bit
		if(timeSinceSceneStart > 1.175f) { 
			if(!soundPlayed) {
				volcanoExplosionSound.Play();
				soundPlayed = true;
			}
			pos.x = Mathf.Sin(100 * Time.time * speed);
			pos.y = -1;
			pos.z = Mathf.Sin(100 * Time.time * speed);
			transform.position = pos;
			Handheld.Vibrate();
		}
		// end scene when animation ends
		if(timeSinceSceneStart >= (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay)){
			player.GetComponent<WorldScript>().enabled = true;
 			canvas.SetActive(true);
			gameObject.SetActive(false);
		}
		timeSinceSceneStart += Time.deltaTime;
	}
}
