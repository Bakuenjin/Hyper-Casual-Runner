using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private GameObject m_StartUI;
    [SerializeField] private GameObject m_GameplayUI;
    [SerializeField] private GameObject m_PauseUI;

	// Use this for initialization
	public void LoadStartUI()
    {
        m_StartUI.SetActive(true);
        m_GameplayUI.SetActive(false);
        m_PauseUI.SetActive(false);
	}
	
    public void LoadGameUI()
    {
        m_StartUI.SetActive(false);
        m_GameplayUI.SetActive(true);
        m_PauseUI.SetActive(false);
    }

    public void LoadPauseUI()
    {
        m_StartUI.SetActive(false);
        m_GameplayUI.SetActive(false);
        m_PauseUI.SetActive(true);
    }
}
