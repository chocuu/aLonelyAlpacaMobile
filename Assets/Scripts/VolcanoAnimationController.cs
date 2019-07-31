using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anonym.Isometric;


public class VolcanoAnimationController : MonoBehaviour {
	public float delay = 0f;
	public AudioSource volcanoExplosionSound;
	float speed = 0.55f; //how fast it shakes
	Vector3 pos;
	Animator animatorComponent;
	public GameObject player;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		//animatorComponent.speed = 0.5f;
		//StartCoroutine(playAnimation());
		pos = new Vector3(0.71f, 0 ,0.71f);
		Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); 
		StartCoroutine(stopMovement());
	}
	
	private bool soundPlayed = false;
	// Update is called once per frame
	void Update () {
		if(!soundPlayed && Time.time > 0.5f) {
			volcanoExplosionSound.Play();
			soundPlayed = true;
		}
		if(Time.time >1.25f) { 
			pos.x = Mathf.Sin(100 * Time.time * speed);
			pos.z = Mathf.Sin(100 * Time.time * speed);
			transform.position = pos;
			Handheld.Vibrate();
		}
	}
	
	public IEnumerator stopMovement() {
	 	canvas.SetActive(false);
		player.GetComponent<WorldScript>().enabled = false;
    	yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length - 0.1f);
		player.GetComponent<WorldScript>().enabled = true;
 		canvas.SetActive(true);
	}
}
