using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AssemblyCSharp;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
	public float acceleration_const = 1f;
	public float speed_const = 10f;
	public float turnSpeed_const = 0.5f;

	public bool TypeOfMovement;
	public String hint = "TypeOfMovement true if you want to play with one hand and" +
	                     " the character will turn for itself, if you want to control its rotation, " +
	                     "TypeOfMovement false!";
	
	GameObject levelManager;
	IMovement movement;
	//public float speed;
	public float acceleration;
	public float rotat;
	public int lastRotation;
	public int forward = 1;
	public int backward = -1;


	public bool running;
	LifeManager lifeManager;

	public EnemyDetection enemyDetection;


	void Awake(){

		levelManager = GameObject.Find ("LevelManager");
		lifeManager = GameObject.Find ("LifeManager").GetComponent<LifeManager> ();

		if (TypeOfMovement) {
			gameObject.AddComponent<OneHandMovement> ();
			movement = GetComponent<OneHandMovement> ();
		} else {
			gameObject.AddComponent<TwoHandMovement> ();
			movement = GetComponent<TwoHandMovement> ();
			rotat = 0f;
			lastRotation = 1;
		}
	}

	void Start(){
		
	}

	void Update(){
	}

	void FixedUpdate(){
		movement.Run (lifeManager.isBuffed, out running, out acceleration);
		movement.Move (acceleration);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Exit") 
		{
			SceneManager.LoadScene("JerryHaMuerto");
		}
	}

}


