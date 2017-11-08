using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

	float 			x, z;
	public int 		lastX = 0, lastZ = 0;
	public float 	acceleration = 1;
    private float   acceleration_ratio = 2;
    private float   acceleration_buffed = 0.5f;
	public float	MAX_ACCELERATION;
	public float 	velocity;
    public bool     Running;
	public float 	turnSpeed;
	float yRot = 0f;

	public GameObject levelManager;

	public bool MovementTwoHands;

	void Start () 
	{
		if (MovementTwoHands) {
		} else {
		}

		levelManager = GameObject.Find ("Level Manager").gameObject;
        Running = false;
	}

	public void OneHandMovement(){
		
	}

	public void TwoHandMovement(){
		
	}

	void FixedUpdate()
	{
		//Movimiento básico
		//x = Input.GetAxis ("Horizontal");
		//z = Input.GetAxis ("Vertical");

		if (Input.GetKey (KeyCode.A)) {
			x = -1;
		} else if (Input.GetKey (KeyCode.D)) {
			x = 1;
		} else {
			x = 0f;
		}

		if (Input.GetKey (KeyCode.S)) {
			z = -1;
		} else if (Input.GetKey (KeyCode.W)) {
			z = 1;
		} else {
			z = 0f;
		}



		//Rotación
		/*if (Input.anyKeyDown) {*/

			if ((Input.GetKey(KeyCode.LeftArrow))) {
				yRot += turnSpeed;
				Quaternion newRotation = Quaternion.Euler (new Vector3(-50f, yRot, 0f));
				transform.rotation = Quaternion.Slerp (transform.localRotation, newRotation, turnSpeed);
			}

			if ((Input.GetKey(KeyCode.UpArrow))) {
				yRot += turnSpeed;
				Quaternion newRotation = Quaternion.Euler (new Vector3(-50f, yRot, 0f));
				transform.rotation = Quaternion.Slerp (transform.localRotation, newRotation, turnSpeed);
			}

			if ((Input.GetKey(KeyCode.DownArrow))) {
				yRot += turnSpeed;
				Quaternion newRotation = Quaternion.Euler (new Vector3(-50f, yRot, 0f));
				transform.rotation = Quaternion.Slerp (transform.localRotation, newRotation, turnSpeed);
			}

			if ((Input.GetKey(KeyCode.RightArrow))) {
				yRot += turnSpeed;
				Quaternion newRotation = Quaternion.Euler (new Vector3(-50f, yRot, 0f));
				transform.rotation = Quaternion.Slerp (transform.localRotation, newRotation, turnSpeed);
			}

		//}

        //SPRINT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!LifeManager.Instance.isBuffed)
            {
                acceleration = acceleration_ratio;
            }
            else
            {
                acceleration = acceleration_ratio / 2;
            }
            Running = true;
        }
        else
        {
            if (!LifeManager.Instance.isBuffed)
            {
                acceleration = 1;
            }
            else
            {
                acceleration = acceleration_buffed;
            }
            Running = false;
        }
			
		
		Vector3 movement = new Vector3 (x * velocity * acceleration * Time.deltaTime,
										0f,
										z * velocity * acceleration * Time.deltaTime);

		transform.position += movement;


		//Movimiento con efecto de frenazo
		/*x = Input.GetAxis("Horizontal");
		z = Input.GetAxis ("Vertical");

		Vector3 movement;
		float increase = Time.deltaTime * velocity;

		//Si muevo al personaje
		if (x != 0 || z != 0) {

			//Detecto la última dirección de movimiento para calcular el impulso final tras el mismo
			if (x != 0 && z != 0) {
				if (lastX == 0) lastX = (int)Math.Ceiling (x);
				if (lastZ == 0) lastZ = (int)Math.Ceiling (z);
			} else if (x != 0) {
				if (((int)Math.Ceiling (x) != 0 && lastX != (int)Math.Ceiling (x)) || lastX == 0) lastX = (int)Math.Ceiling (x);
				lastZ = 0;
			} else {
				if (((int)Math.Ceiling (z) != 0 && lastZ != (int)Math.Ceiling (z)) || lastZ == 0) lastZ = (int)Math.Ceiling (z);
				lastX = 0;
			}

			//Si ya se está moviendo, y no supera a la aceleración máxima
			if (acceleration < MAX_ACCELERATION) {
				//Aumento el valor de la aceleración
				acceleration += increase;
			}

			movement = new Vector3 (
				x * velocity * Time.deltaTime + x * acceleration*acceleration/2, 
				0f, 
				z * velocity * Time.deltaTime + z * acceleration*acceleration/2);
		} 
		else {
			//Si no se mueve al jugador, pero tiene aceleración anterior
			if (acceleration > 0) {
				//Disminuyo la aceleración
				acceleration -= increase;

				movement = new Vector3 (
					lastX * velocity * Time.deltaTime + lastX * acceleration*acceleration/2, 
					0f, 
					lastZ * velocity * Time.deltaTime + lastZ * acceleration*acceleration/2);
			} 
			//Si ya no existe aceleración
			else {
				acceleration = 0f;
				lastX = 0;
				lastZ = 0;
				movement = new Vector3 (0f, 0f, 0f);
			}
		}

		//Finalmente, se actualiza la posición del jugador
		transform.position += movement;*/
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Exit") 
		{
			levelManager.GetComponent<LevelManager> ().changeLevel ();
		}

		else if(col.tag == "Enemy")
		{
			
		}
	}

	void onTriggerExit(Collider col)
	{
		
	}

}
