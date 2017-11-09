using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour {

    private StateController controller;
    public Transform lastPoint;
    private float offset = 3f;
    public int damage;

    private void Start()
    {
        controller = GetComponent<StateController>();
        lastPoint = new GameObject().transform;
    }

    private void FixedUpdate()
    {
        if(controller.isPlayerHeard)
        {
            if (CheckIfImInArea())
            {
                controller.isPlayerHeard = false;
                controller.pState = StateController.pursuitState.SCAPED;
            }
        }
    }

    private bool CheckIfImInArea()
    {
        float minx = lastPoint.position.x - offset;
        float maxx = lastPoint.position.x + offset;
        float minz = lastPoint.position.z - offset;
        float maxz = lastPoint.position.z + offset;

        if(transform.position.x > minx && transform.position.x < maxx &&
            transform.position.z > minz && transform.position.z < maxz)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && controller.pState!=StateController.pursuitState.FOLLOWING)
        {
            //other.gameobject es nuestro jugador.
            if (other.gameObject.GetComponent<Player>().running)
            {
				Debug.Log ("Deberias oirme");
                //controller.isPlayerOnSight = true;
                controller.pState = StateController.pursuitState.ALERT;
                controller.isPlayerHeard = true;

                lastPoint.position = other.gameObject.transform.position;
                //controller.chaseTarget = lastPoint;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player" && controller.pState != StateController.pursuitState.FOLLOWING)
        {
            //other.gameobject es nuestro jugador.
            if (other.gameObject.GetComponent<Player>().running)
            {
                //controller.isPlayerOnSight = true;
                controller.pState = StateController.pursuitState.ALERT;
                controller.isPlayerHeard = true;

                lastPoint.position = other.gameObject.transform.position;
                //controller.chaseTarget = lastPoint;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
			Debug.Log ("pene");
            LifeManager.Instance.GetHit(damage);
            LifeManager.Instance.BuffPlayer();
            Destroy(this.gameObject);
        }
    }

}
