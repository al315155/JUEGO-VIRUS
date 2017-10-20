using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scape : SteerAgent {

	public Transform FleeFrom;

	public override void CalculateSteerStrenght(){
		Vector3 desiredVelocity = (transform.position-FleeFrom.position).normalized * MaxVelocity;
		SteerStrengh = desiredVelocity - Velocity;
	}
}
