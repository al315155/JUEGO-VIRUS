using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteerAgent {

	public Transform target;
	public Vector3 desiredVelocity;

	public override void CalculateSteerStrenght(){
		desiredVelocity = (target.position - transform.position).normalized * MaxVelocity;
		SteerStrengh = desiredVelocity - Velocity;
	}
}
