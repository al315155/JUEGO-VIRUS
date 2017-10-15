using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	private GameObject player;
	public bool IsAttacking;

	// Use this for initialization
	void Awake()
	{
		IsAttacking = false;
		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




}
