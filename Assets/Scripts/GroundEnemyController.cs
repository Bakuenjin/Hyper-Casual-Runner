using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour, IEnemyController {

    [SerializeField]
    private Vector3 m_StartPosition;
    [SerializeField]
    private float m_Speed;

    private void Start()
    {
        transform.position = m_StartPosition;
    }

    public void UpdatePosition()
    {
        Vector3 translation = new Vector3(-Time.deltaTime * m_Speed, 0, 0);
        transform.Translate(translation);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public float GetCurrentPositionX()
    {
        return transform.position.x;
    }
}
