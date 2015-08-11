﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : Photon.MonoBehaviour {

	//Tuning
	public float horizontalDrag = 3;

	//Components of Game Object
	PlayerStatus status;
	Rigidbody rbody;
	Collider col;


	//Physic Simulation
	RaycastHit hitInfo;
	bool isGrounded = false;

	// Use this for initialization
	void Start () {
		status = gameObject.GetComponentInChildren<PlayerStatus>();
		rbody = gameObject.GetComponentInChildren<Rigidbody> ();
		col = gameObject.GetComponentInChildren<Collider> ();
	}

	// Update is called once per timestep
	void FixedUpdate () {
		if (photonView.isMine)
			ManageInput ();
	}

	void ManageInput(){
		//Mouse and keyboard navigation
		rbody.AddForce (gameObject.transform.forward * Input.GetAxis("Horizontal") * -1 * status.speed);
		rbody.AddForce (gameObject.transform.right * Input.GetAxis("Vertical")  * status.speed);
		gameObject.transform.Rotate(gameObject.transform.up * Input.GetAxis("Mouse X") * status.turnspeed);
		
		//Movement drag
		rbody.AddForce(new Vector3(-rbody.velocity.x * horizontalDrag, -3, -rbody.velocity.z * horizontalDrag));
		
		//Jump function
		if(!isGrounded &&
		   Physics.Raycast (new Ray (gameObject.transform.position, new Vector3(0,-1,0)), out hitInfo, 0.65f)){
			isGrounded = true;
		}
		if (Input.GetButtonDown("Jump") && isGrounded){
			rbody.AddForce (gameObject.transform.up * status.jumpforce);
			isGrounded = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "Water")
			status.hp = 0;
	}
}
