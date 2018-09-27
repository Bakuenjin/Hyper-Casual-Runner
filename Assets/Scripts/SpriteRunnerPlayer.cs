using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRunnerPlayer : MonoBehaviour {

    [SerializeField] private float m_WaitTime;
    [SerializeField] private Sprite[] m_Sheet;

    private SpriteRenderer m_SpriteRenderer;
    private int m_Index;
    private bool m_InAnim;

	// Use this for initialization
	void Start () {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_Sheet[0];
        StartCoroutine(Animation());

        ObserverManager.m_Instance.AddObserver(ObserveType.OnJump, PauseAnim);
        ObserverManager.m_Instance.AddObserver(ObserveType.OnJumpFinished, ResumeAnim);
    }

    private void OnDestroy()
    {
        ObserverManager.m_Instance.RemoveObserver(ObserveType.OnJump, PauseAnim);
        ObserverManager.m_Instance.RemoveObserver(ObserveType.OnJumpFinished, ResumeAnim);
    }

    private void PauseAnim(object o)
    {
        m_InAnim = false;
    }

    private void ResumeAnim(object o)
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        if (m_InAnim)
            yield break;

        m_InAnim = true;
        while(m_InAnim)
        {
            m_SpriteRenderer.sprite = m_Sheet[m_Index];

            m_Index++;
            if (m_Index >= m_Sheet.Length) m_Index = 0;
            yield return new WaitForSeconds(m_WaitTime);
        }
    }
}
