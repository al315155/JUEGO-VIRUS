using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMov : MonoBehaviour
{
    public Transform player;
  //  public Vector3 offset;
    public float rotationX, rotationY, rotationZ;

	public Grid grid;
	private float gridSizeX, gridSizey;
	public float ZAdjust;
	public float YDst;
	public float offset;
	public float cameraSpeed;

	void Awake(){
		grid = GameObject.Find ("A*").GetComponent<Grid>();
		gridSizeX = grid.gridWorldSize.x;
		gridSizey = grid.gridWorldSize.y;

		transform.position = new Vector3 (0, YDst, -gridSizey / 2 + ZAdjust);
	}

    // Use this for initialization
    void Start()
    {
		//ansform.position = player.transform.position - new Vector3 (0f, 0f, 50f);
        //Obtenemos el offset de la cámara mientras sigue al prota.
	

		offset = Mathf.Abs(transform.position.z - player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
		//transform.position = player.transform.position + offset;
		if (DistanceFromPlayer () > offset) {
			Vector3 updatePos;
			if (transform.position.z > player.position.z) {
				updatePos = new Vector3 (0f, 0f, -Time.deltaTime * cameraSpeed);
			} else {
				updatePos = new Vector3 (0f, 0f, Time.deltaTime * cameraSpeed);
			}
			transform.position += updatePos;
		}

    }

	private float DistanceFromPlayer(){
		return Mathf.Abs(transform.position.z - player.position.z);
	}
}