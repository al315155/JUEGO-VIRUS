using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp;


public class TwoHandMovement : MonoBehaviour, IMovement
{
	Player player;
	void Awake(){
		player = this.GetComponent<Player> ();
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

	public void Move(float acceleration){

		//Movement

		Vector3 movement;
		bool turnAround = false;

		if (Input.GetKey (KeyCode.D)) {
			//x = 1;
			/*movement = new Vector3(player.forward * (transform.rotation.eulerAngles.x) * player.speed_const * acceleration * Time.deltaTime, 0f, 0f);
			transform.position += movement;

			if (player.lastRotation != 1) {
				player.lastRotation = 1;
				turnAround = true;
			}*/
		}
		if (Input.GetKey (KeyCode.A)) {
			/*movement = new Vector3(player.backward * (transform.rotation.eulerAngles.x) * player.speed_const * acceleration * Time.deltaTime, 0f, 0f);
			transform.position += movement;

			if (player.lastRotation != -1) {
				player.lastRotation = -1;
				turnAround = true;
			}*/
		}
		if (Input.GetKey (KeyCode.W)) {
			//z = 1;
			movement = player.forward * transform.forward * player.speed_const * acceleration * Time.deltaTime;
			transform.position += movement;

			if (player.lastRotation != 1) {
				player.lastRotation = 1;
				turnAround = true;
			}
		}
		if (Input.GetKey (KeyCode.S)) {
			movement = player.backward * transform.forward * player.speed_const * acceleration * Time.deltaTime;
			transform.position += movement;

			if (player.lastRotation != -1) {
				player.lastRotation = -1;
				turnAround = true;
			}
		}

		/*Vector3 movement = new Vector3 (x * player.speed_const * acceleration * Time.deltaTime, 0f,
			z * player.speed_const * acceleration * Time.deltaTime);*/

		//transform.position += movement;

		//Rotation
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.DownArrow)){
			player.rotat -= Time.deltaTime * player.turnSpeed_const;
		}
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow)){
			player.rotat += Time.deltaTime * player.turnSpeed_const;
		}	

		if (turnAround) {
			player.rotat -= 180f;
			player.forward = -player.forward;
			player.backward = -player.backward;
		}

		player.rotat = ClampAngle (player.rotat);
		Quaternion newRotation = Quaternion.Euler (0, player.rotat, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, player.turnSpeed_const);

	}

	private float ClampAngle(float angle){
		if (angle > 360f)
			angle -= 360f;

		if (angle < 0f)
			angle += 360f;

		return angle;
	}

}


