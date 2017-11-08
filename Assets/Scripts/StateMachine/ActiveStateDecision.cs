﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

	public override bool Decide (StateController controller){

        if (controller.pState == StateController.pursuitState.SCAPED)
        {
            Debug.Log("Estoy en false");
			controller.pathfining.SetPlayerGone (true);
            controller.pState = StateController.pursuitState.PATROL;
            return false;
        }
			
		//que es esto
        //bool chaseTargetisActive = controller.chaseTarget.gameObject.activeSelf;
		//return chaseTargetisActive; 

		return true;
	}
}
