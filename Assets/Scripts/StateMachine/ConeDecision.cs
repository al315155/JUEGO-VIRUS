using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Cone")]
public class ConeDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Check(controller);
        return targetVisible;
    }

    private bool Check(StateController controller)
    {
        if (controller.isPlayerOnSight)
        {
            controller.chaseTarget = controller.player;
        }
        if (controller.pState == StateController.pursuitState.SCAPED)
        {
            Debug.Log("Estoy en false");
            controller.pState = StateController.pursuitState.PATROL;
            return false;
        }
        return controller.isPlayerOnSight || controller.isPlayerHeard;
    }


}
