using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float RotationSpeed;
	public float x, y, z;
	public int dirX, dirY, dirZ;

	void Awake()
	{
		Vector3 angles = transform.eulerAngles;

		if (Random.Range (-1, 1) < 0) {dirX = -1;} else {dirX = 1;}
		if (Random.Range (-1, 1) < 0) {dirY = -1;} else {dirY = 1;}
		if (Random.Range (-1, 1) < 0) {dirZ = -1;} else {dirZ = 1;}

		x = angles.x * dirX;
		y = angles.y * dirY;
		z = angles.z * dirZ;
	}

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		RotateInPlace ();
       
	}

	public void RotateInPlace()
	{
		x += Time.deltaTime * RotationSpeed * dirX;
		y += Time.deltaTime * RotationSpeed * dirY;
		z += Time.deltaTime * RotationSpeed * dirZ;

		if (Mathf.Abs(x) > 360f) x /= 360f;
		if (Mathf.Abs(y) > 360f) y /= 360f;
		if (Mathf.Abs(z) > 360f) z /= 360f;


		transform.rotation = Quaternion.Euler (x, y, z);
	}
}



