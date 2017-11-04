using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeConeMaterial : MonoBehaviour {

    private MeshRenderer m_meshRenderer;
    public Texture2D[] m_materials;

    private void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial(stateMaterial _state)
    {
        switch (_state)
        {
            case stateMaterial.PATROL:
                m_meshRenderer.materials[0].mainTexture = m_materials[0];
                break;
            case stateMaterial.PURSUIT:
                m_meshRenderer.materials[0].mainTexture = m_materials[1];
                break;
            default:
                m_meshRenderer.materials[0].mainTexture = m_materials[0];
                break;
        }
    }
}

public enum stateMaterial { PATROL, PURSUIT}
