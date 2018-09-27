using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyController : MonoBehaviour, IEnemyController {

    [SerializeField]
    private Vector3 m_StartPosition;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_YDamp;

    private float m_Timer;

    // Use this for initialization
    void Start ()
    {
        transform.position = m_StartPosition;
    }

    public void UpdatePosition()
    {
        m_Timer += Time.deltaTime;
        Vector3 newPos = transform.position;
        newPos.y = Mathf.Sin(m_Timer * m_Speed) / m_YDamp + m_StartPosition.y;
        newPos.x -= Time.deltaTime * m_Speed;
        transform.position = newPos;
    }

    public float GetCurrentPositionX()
    {
        return transform.position.x;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
