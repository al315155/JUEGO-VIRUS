using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteerAgent : MonoBehaviour {

	// 	Estudios IA Craig Reynolds
	//	Ricardo David AI programmation

	public 	float	Mass			= 1f;
	public	float	MaxStrengh		= 0.1f;
	public	float	MaxVelocity		= 1f;
	public	float	TickFixedTime 	= 0.01f;

	//[System.NonSerializedAttribute]
	public 	Vector3 Velocity;
	//[System.NonSerializedAttribute]
	public 	Vector3	SteerStrengh;

	private Vector3 maxVector(Vector3 v, float c){
		if (v.magnitude > c)
			return v.normalized * c;
		return v;
	}

	public IEnumerator UpdateObjective(){
		while (true) {
			CalculateSteerStrenght ();

			yield return new WaitForSeconds (TickFixedTime);
		}
	}

	public IEnumerator UpdatePosition(){
		while (true) {
			SteerStrengh = maxVector (SteerStrengh, MaxStrengh);
			Velocity = maxVector (Velocity + SteerStrengh * (1.0f / Mass), MaxVelocity);

			transform.position += Velocity;

			LookAtUpdate ();

			yield return new WaitForFixedUpdate ();
		}
	}

	public virtual void LookAtUpdate(){
		if (Velocity.sqrMagnitude > 0.1f)
			transform.forward = Velocity;
	}

	public abstract void CalculateSteerStrenght ();
}
