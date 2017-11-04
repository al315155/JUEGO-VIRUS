using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour {

    [Tooltip("Indica si el jugador ha entrado en el campo visual del enemigo")]
    public bool m_jugadorLocalizado;
    [Tooltip("Última posición donde se vio al jugador")]
    public Vector3 m_ultimaPosicionJugador;

    public NavMeshAgent m_nav;
    [Tooltip("Esfera exterior del campo auditivo. Detecta a jugador si corre")]
    public SphereCollider m_esferaExterior;
    [Tooltip("Esfera exterior del campo auditivo. Detecta a jugador por proximidad")]
    private SphereCollider m_esferaInterior;
    [Tooltip("Jugador")]
    public GameObject player;

    public GameObject conoDeVision;


}
