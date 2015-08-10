﻿using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	//Health, status and abilities
	public float hp = 100;
	public float speed = 30;
	public float turnspeed = 10;
	public float jumpforce = 300;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	//Power-ups
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Erdbeereis") {
			Destroy(other.gameObject);
			//gameObject.renderer.material.color = Color.red;
			if(hp < 100) {
				hp++;
				Debug.Log(hp);
			} else if(hp == 100) {
				hp=100;
				Debug.Log(hp);
			}
		}
	}

}
