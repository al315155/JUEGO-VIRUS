using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class CamaraMov : MonoBehaviour
{
    public Transform player;
    public float rotationX, rotationY, rotationZ;

	private float gridSizey;
	public CameraType cameraType;
	public ICameraMovement movement;

	public float CameraHeight;
	public float CameraSpeed;
	public float DstFromPlayer;
	public float Offset;

	public enum CameraType{LookAtPlayer, LookFromAbove, LookCloser};

    void Start()
    {
		gridSizey = GameObject.Find ("A*").GetComponent<Grid>().gridWorldSize.y;

		Vector3 position = Vector3.one; 
		Vector3 rotation = Vector3.one; 
			
		switch (cameraType) {
		case CameraType.LookAtPlayer:
			movement = new LookAtPlayer ();
			position = new Vector3 (0f, CameraHeight, -gridSizey / 2f);
			rotation = new Vector3 (rotationX, rotationY, rotationZ);
			break;
		case CameraType.LookFromAbove:
			movement = new LookFromAbove ();
			position = new Vector3 (0f, CameraHeight, -gridSizey / 2f);
			rotation = new Vector3 (rotationX, rotationY, rotationZ);
			break;
		case CameraType.LookCloser:
			movement = new LookCloser ();
			position = new Vector3 (0f, CameraHeight, player.position.z - Offset);
			rotation = new Vector3 (rotationX, rotationY, rotationZ);
			break;
		}

		transform.position = position;
		transform.rotation = Quaternion.Euler (rotation);
    }

    void Update()
    {
		movement.Move (CameraSpeed, transform, player);
    }

	public float DistanceFromPlayer(){
		Debug.Log(Mathf.Abs(Mathf.Abs(transform.position.z) - Mathf.Abs(player.position.z)));
		return Mathf.Abs(Mathf.Abs(transform.position.z) - Mathf.Abs(player.position.z));
	}
}