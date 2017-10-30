using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

	public override bool Decide (StateController controller){
		bool chaseTargetisActive = controller.chaseTarget.gameObject.activeSelf;;
		return chaseTargetisActive; 
		}
}
