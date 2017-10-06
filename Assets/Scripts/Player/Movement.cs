using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {

	float 			x, z;
	public int 		lastX = 0, lastZ = 0;
	public float 	acceleration = 0f;
	public float	MAX_ACCELERATION;
	public float 	velocity;

	public GameObject levelManager;

	void Start () 
	{
		levelManager = GameObject.Find ("Level Manager").gameObject;
	}
	
	void Update () 
	{
		

	}

	void FixedUpdate()
	{
		//Movimiento básico
		x = Input.GetAxis ("Horizontal");
		z = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (x * velocity * Time.deltaTime, 0f, z * velocity * Time.deltaTime);

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
	}

}
