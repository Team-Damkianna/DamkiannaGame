﻿using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {
	Animator controller;
	int[] stateHashes = new int[3];
	float yRotation;
	
	void Start()
	{
		controller = GetComponent<Animator>();
		stateHashes[0] = Animator.StringToHash("walk");
		stateHashes[1] = Animator.StringToHash("run");
		stateHashes[2] = Animator.StringToHash("jump");
		yRotation = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			controller.SetBool(stateHashes[0], true);
		}
		
		if (Input.GetKeyUp(KeyCode.S))
		{
			controller.SetBool(stateHashes[0], false);
		}
		
		if (Input.GetKeyDown(KeyCode.W))
		{
			controller.SetBool(stateHashes[1], true);
		}
		
		if (Input.GetKeyUp(KeyCode.W))
		{
			controller.SetBool(stateHashes[1], false);
		}
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//int jumpHash = stateHashes[2];
			controller.SetBool(stateHashes[2], true);
			
			//StartCoroutine(UncheckStateFlag(2));
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			controller.SetBool(stateHashes[2], false);
		}

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			//transform.rotation = new Vector3(0, -90f, 0);
		}

		if (Input.GetKeyDown (KeyCode.D)) 
		{
			if(yRotation > 360f && yRotation < -360)
			{
				yRotation = 0f;
			}
			yRotation += 90f;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
		}

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			if(yRotation > 360f && yRotation < -360)
			{
				yRotation = 0f;
			}
			yRotation -= 90f;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
		}
	}
	
	IEnumerator UncheckStateFlag(int flagHash)
	{
		yield return new WaitForEndOfFrame();
		//Debug.Log("UncheckStateFlag");
		int hash = stateHashes[flagHash];
		controller.SetBool(hash, false);
	}
}

/*public enum Animations
{
	startWalk = 0,
	jump = 1
}*/
