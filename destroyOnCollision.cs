using UnityEngine;
using System.Collections;
using System;


[System.Serializable]
public class uniqueEffects {
	
	bool hasPlayed = false; 
	public Transform effectTransform; 
	public GameObject playEffect; 
	public AudioSource playSound; 

	
	public bool getPlayedStatus() {
		return hasPlayed;
	}

	
	public void setPlayedStatus() {
		hasPlayed = true;
	}
}

public class CollisionDestroy : MonoBehaviour {

	int i; // loop variable
	static bool playNow = false; 
	Component [] streetComponent; 
	int randomIndex; 
	public int minVelocityToDestroy; 
	public uniqueEffects[] effects;
	public AudioSource[] defaultAudioToPlay;
	public GameObject[] destroyObjects; 
	Collider triggerComponent; 

	
	public void playAudio() {
		randomIndex = UnityEngine.Random.Range(0,defaultAudioToPlay.Length); 

		
		try {
			defaultAudioToPlay [randomIndex].playOnAwake=false; 
			defaultAudioToPlay [randomIndex].Play(); 
			//Debug.Log (randomIndex);
		} catch(Exception e) {
			throw new Exception ("Please add Audio Source" + e.ToString());
		}
	}

	
	void setStatic() {
		foreach(Rigidbody rigid in streetComponent) {
			rigid.isKinematic = true;
			rigid.detectCollisions = true;
		}
	}

	
	void destroyIfCollision() {
		foreach(Rigidbody rigid in streetComponent) {
			rigid.isKinematic = false;
			rigid.detectCollisions = true;
		}
		playNow = true;
		triggerComponent = GetComponent<Collider> ();
		triggerComponent.enabled = false; 
	}

	
	void Start () {
		streetComponent = GetComponentsInChildren (typeof(Rigidbody));
		setStatic ();
	}


	
	void OnTriggerEnter(Collider other) {
		if(playNow)	
		playAudio ();
		
		if (other.attachedRigidbody.velocity.magnitude > minVelocityToDestroy) {
			destroyIfCollision ();
			for(i=0;i<effects.Length;i++) {
				if(effects[i].getPlayedStatus()==false) {
					effects [i].setPlayedStatus ();
					
					Instantiate (effects[i].playEffect, effects[i].effectTransform.transform.position, effects[i].effectTransform.transform.rotation);
					effects[i].playSound.Play ();
				}
			}
			
			for(i=0;i<destroyObjects.Length;i++) {
				Destroy (destroyObjects[i]);
			}
		} 
	}
}



