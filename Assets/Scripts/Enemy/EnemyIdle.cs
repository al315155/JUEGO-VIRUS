using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : MonoBehaviour {

	Enemy enemy; 

	void Awake()
	{
		enemy = GetComponent<Enemy> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		enemy.RotateInPlace ();
	}


}
