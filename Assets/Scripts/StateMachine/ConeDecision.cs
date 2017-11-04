using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decision/Cone")]
public class ConeDecision : Decision {

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.isPlayerOnSight)
        {
            controller.chaseTarget = controller.player;
        }
        return controller.isPlayerOnSight;
    }


}
