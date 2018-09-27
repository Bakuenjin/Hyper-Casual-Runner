using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float m_JumpHeight;
    [SerializeField]
    private float m_JumpDuration;
    [SerializeField]
    private Vector3 m_StartPosition;

    private bool m_InJump;
    private Action<bool> m_OnEnemyHit; 

    private void Start()
    {
        transform.position = m_StartPosition;
    }

    public void SetOnEnemyHit(Action<bool> action)
    {
        m_OnEnemyHit = action;
    }

    public void CheckMovement()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Jump());
        }
    }
    
    IEnumerator Jump()
    {
        if (m_InJump)
            yield break;

        m_InJump = true;
        
        float timer = 0;
        Vector3 newPos = m_StartPosition;
        ObserverManager.m_Instance.InvokeObservers(ObserveType.OnJump, 0);

        while (timer < m_JumpDuration)
        {
            timer += Time.deltaTime;
            newPos.y = Mathf.Sin(timer * Mathf.PI / m_JumpDuration) * m_JumpHeight;
            transform.position = newPos;

            yield return null;
        }

        transform.position = m_StartPosition;
        ObserverManager.m_Instance.InvokeObservers(ObserveType.OnJumpFinished, 0);

        m_InJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") ObserverManager.m_Instance.InvokeObservers(ObserveType.OnDeath, 1);
    }
}
