using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour {

    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_StartPositionX;
    [SerializeField]
    private float m_ClampPoint;

    private float m_Distance;

    private void Start()
    {
        m_Distance = Mathf.Abs(m_ClampPoint - m_StartPositionX);
    }

    public void Move()
    {
        transform.Translate(-Time.deltaTime * m_Speed, 0, 0);

        if(transform.position.x <= m_ClampPoint)
        {
            transform.localPosition += new Vector3(m_Distance, 0, 0);
        }
    }
}
