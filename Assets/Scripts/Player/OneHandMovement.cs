using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp;


public class OneHandMovement : MonoBehaviour, IMovement
{
	Player player;
	void Awake(){
		player = this.GetComponent<Player> ();
	}

	public void Move(float acceleration){
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x * player.speed_const * acceleration * Time.deltaTime, 0f,
			z * player.speed_const * acceleration * Time.deltaTime);

		if (z != 0 || x != 0) {
			Debug.Log ("Changing rotation");

			float yRot = transform.rotation.y;

			if (z != 0 && x != 0) {
				yRot = (z * x > 0) ? ((x > 0) ? 45f : 225f) : ((x > 0) ? 135f : 315f);
		
			} else {
				if (x == 0) {
					yRot = (z < 0) ? 180f : 0f;
				}

				if (z == 0) {
					yRot = (x < 0) ? 270f : 90f;
				}
			}

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (new Vector3 (0f, yRot, 0f)), 2f);
		}

		transform.position += movement;
	}

	public void Run(bool buffed, out bool running, out float acceleration){

		if (Input.GetKey (KeyCode.LeftShift)) {
			if (!buffed) {
				acceleration = player.acceleration_const * 2f;
			} else {
				acceleration = player.acceleration_const;
			}

			running = true;
		} else {
			if (!buffed) {
				acceleration = player.acceleration_const;
			} else {
				acceleration = player.acceleration_const / 2f;
			}

			running = false;
		}
	}
}


