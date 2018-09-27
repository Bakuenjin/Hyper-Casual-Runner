using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private EnemyManager m_EnemyManager;
    [SerializeField] private GraphicManager m_GraphicManager;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private GameObject m_PlayerPrefab;

    private PlayerController m_Player;
    private bool m_GameRunning;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start () {
        m_UIManager.LoadStartUI();
	}
	
	// Update is called once per frame
	void Update () {
        if(m_GameRunning)
        {
            m_Player.CheckMovement();
            m_EnemyManager.ManageEnemies();
            m_GraphicManager.ManageGraphics();
        }
	}

    public void StartGame()
    {
        if(!m_GameRunning)
        {
            m_GameRunning = true;
            ObserverManager.m_Instance.AddObserver(ObserveType.OnDeath, GameOver);
            GameObject go = Instantiate(m_PlayerPrefab, Vector3.zero, Quaternion.identity);
            m_Player = go.GetComponent<PlayerController>();
            m_EnemyManager.StartManaging();
            m_UIManager.LoadGameUI();
        }
    }

    public void PauseGame()
    {
        m_GameRunning = false;
        m_EnemyManager.EndManaging(false);
        m_UIManager.LoadPauseUI();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        m_GameRunning = true;
        m_EnemyManager.StartManaging();
        m_UIManager.LoadGameUI();
        Time.timeScale = 1;
    }

    public void BackToTitle()
    {
        GameOver(true);
    }

    public void GameOver(object o)
    {
        m_GameRunning = false;
        m_EnemyManager.EndManaging(true);
        m_UIManager.LoadStartUI();
        Destroy(m_Player.gameObject);
        Time.timeScale = 1;
    }
}
