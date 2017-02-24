using UnityEngine;
using System.Collections;
using System;

public class destroyOnCollision : MonoBehaviour {

	public int minVelocityToDestroy; /* minimum relative velocity for destruction to occur on collision */
	Component [] streetComponent; /* array of rigidbodies in the game object */
	public AudioSource[] audioToPlay; /*array of audio sources to play for impact sound effects */
	int randomIndex; /* random index number of audio source to play */

	/* play random sounds on collision for fun ;-) */
	void playAudio() {
		randomIndex = UnityEngine.Random.Range(0,audioToPlay.Length); /* randomly pick a audio source */
		/* there must be atleast one audio source */
		try {
			audioToPlay [randomIndex].Play ();
			Debug.Log (randomIndex);
		} catch(Exception e) {
			throw new Exception ("Please add Audio Source or refer to manual for ... " + e.ToString());
		}
	}

	/* initially set all rigidbodies as kinematic */
	void setStatic() {
		foreach(Rigidbody rigid in streetComponent) {
			rigid.isKinematic = true;
			rigid.detectCollisions = true;
		}
	}

	/* set all kinematic rigidbodies as non-kinematic on a collision*/
	void destroyIfCollision() {
		foreach(Rigidbody rigid in streetComponent) {
			rigid.isKinematic = false;
			rigid.detectCollisions = true;
		}
	}

	/* initialize */
	void Start () {
		streetComponent = GetComponentsInChildren (typeof(Rigidbody));
		setStatic ();
	}

	/* called on trigger enter */
	void OnTriggerEnter(Collider other) {
		playAudio (); /* play sound on collision */
		if (other.attachedRigidbody.velocity.magnitude > minVelocityToDestroy) /* destruct object on a minimum velocity of collider */
			destroyIfCollision ();
	}

}
