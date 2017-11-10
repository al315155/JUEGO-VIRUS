using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class LookAtPlayer : ICameraMovement {

	//xRotation = 0f	yRotation = 0f	zRotation = 0f
	//cameraHeight = 25f

	public void Move(float speed, Transform camera, Transform player){

		if (camera.GetComponent<CamaraMov> ().DistanceFromPlayer () >
		    camera.GetComponent<CamaraMov> ().Offset) {
			camera.position += Vector3.forward * speed * Time.deltaTime;
		} else if (camera.GetComponent<CamaraMov> ().DistanceFromPlayer () <
			camera.GetComponent<CamaraMov> ().Offset) {
			camera.position -= Vector3.forward * speed * Time.deltaTime;
		}
		camera.LookAt (player.position);
	}
}
