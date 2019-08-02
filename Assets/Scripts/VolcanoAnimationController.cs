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
	// Use this for initialization
	void Start () {
		//animatorComponent.speed = 0.5f;
		//StartCoroutine(playAnimation());
		pos = new Vector3(0.71f, 0 ,0.71f);
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
		StartCoroutine(stopMovement());
		timeSinceSceneStart = 0;
	}

	// Update is called once per frame
	void Update () {
		if(timeSinceSceneStart >1.175f) { 
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
		timeSinceSceneStart += Time.deltaTime;
	}
	
	public IEnumerator stopMovement() {
	 	canvas.SetActive(false);
		player.GetComponent<WorldScript>().enabled = false;
    	yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.1f);
		player.GetComponent<WorldScript>().enabled = true;
 		canvas.SetActive(true);
	}
}
