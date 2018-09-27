using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private float m_Delay;
    [SerializeField]
    private float m_DeadZone;
    [SerializeField]
    private GameObject[] m_EnemyPrefabs;

    private List<IEnemyController> m_CurrentEnemies;
    private bool m_IsManaging;

    private void Awake()
    {
        m_CurrentEnemies = new List<IEnemyController>();
    }

    public void StartManaging()
    {
        if (!m_IsManaging)
        {
            m_IsManaging = true;
            StartCoroutine(SpawnEnemy());
        }
            
    }

    public void ManageEnemies()
    {
        if(m_IsManaging)
        {
            CheckEnemy();
        }
    }

    private void CheckEnemy()
    {
        List<GameObject> m_DestroyableEnemies = new List<GameObject>();
        foreach (IEnemyController ec in m_CurrentEnemies)
        {
            float posX = ec.GetCurrentPositionX();
            if(posX <= m_DeadZone)
            {
                m_DestroyableEnemies.Add(ec.GetGameObject());
            }
            else
            {
                ec.UpdatePosition();
            }
        }

        foreach (GameObject go in m_DestroyableEnemies)
        {
            m_CurrentEnemies.Remove(go.GetComponent<IEnemyController>());
            Destroy(go);
        }
    }

    public void EndManaging(bool destroyEnemies)
    {
        m_IsManaging = false;
        StopCoroutine(SpawnEnemy());

        if (destroyEnemies) DestroyEnemies();
    }

    private void DestroyEnemies()
    {
        foreach (IEnemyController e in m_CurrentEnemies)
        {
            Destroy(e.GetGameObject());
        }

        m_CurrentEnemies = new List<IEnemyController>();
    }

    IEnumerator SpawnEnemy()
    {
        float timer = 0;
        int rnd = 0;
        GameObject go;

        while (m_IsManaging)
        {
            timer += Time.deltaTime;

            if(timer >= m_Delay)
            {
                timer = 0;
                rnd = UnityEngine.Random.Range(0, m_EnemyPrefabs.Length);
                go = Instantiate(m_EnemyPrefabs[rnd], Vector3.zero, Quaternion.identity);
                m_CurrentEnemies.Add(go.GetComponent<IEnemyController>());
            }

            yield return null;
        }
    }
}
