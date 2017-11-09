using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;


[CreateAssetMenu (menuName = "AI/Actions/PatrolInPlace")]
public class PatrolnPlaceAction : Action 
{
	public override void Act (StateController controller){
		
		if (controller.destination == controller.leftVector) {
			if (Quaternion.Euler(controller.leftVector) == controller.transform.rotation){
				controller.AddPass ();
				controller.destination = controller.rightVector;
			} else {
				controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, Quaternion.Euler(controller.leftVector), 3f);
			}
		}

		if (controller.destination == controller.rightVector) {
			if (Quaternion.Euler(controller.rightVector) == controller.transform.rotation){
				controller.AddPass ();
				controller.destination = controller.leftVector;
			} else{
				controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, Quaternion.Euler(controller.rightVector), 3f);
			}
		}
	}
}


