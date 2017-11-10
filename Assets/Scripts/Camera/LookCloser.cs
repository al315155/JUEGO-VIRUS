using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class LookCloser : ICameraMovement{

	//xRotation = 40f	yRotation = 0f	zRotation = 0f
	//offset = 10

	public void Move(float speed, Transform camera, Transform player){
		Vector3 newPos;
		if (camera.GetComponent<CamaraMov> ().DistanceFromPlayer () >
		    camera.GetComponent<CamaraMov> ().Offset) {
			newPos = new Vector3 (player.position.x, camera.position.y, 
				player.position.z - camera.GetComponent<CamaraMov> ().Offset);
		} else if (camera.GetComponent<CamaraMov> ().DistanceFromPlayer () <
				camera.GetComponent<CamaraMov> ().Offset) {
			newPos = new Vector3 (player.position.x, camera.position.y, 
				player.position.z - camera.GetComponent<CamaraMov> ().Offset);
		}else{
			newPos = new Vector3 (player.position.x, camera.position.y, 
				camera.position.z);
		}
			camera.position = newPos;
		}
}

