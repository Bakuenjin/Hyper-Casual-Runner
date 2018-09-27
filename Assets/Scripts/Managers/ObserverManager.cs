using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObserveType
{
    OnDeath,
    OnJump,
    OnJumpFinished
}

public class ObserverManager : MonoBehaviour {

    public static ObserverManager m_Instance;

    private Dictionary<ObserveType, List<Action<object>>> m_Observers;

	// Use this for initialization
	void Awake () {
        if (m_Instance != null)
            Destroy(this);
        m_Instance = this;

        m_Observers = new Dictionary<ObserveType, List<Action<object>>>();
	}
	
	public void AddObserver(ObserveType type, Action<object> action)
    {
        if (!m_Observers.ContainsKey(type))
        {
            m_Observers[type] = new List<Action<object>>();
        }

        m_Observers[type].Add(action);
    }

    public void RemoveObserver(ObserveType type, Action<object> action)
    {
        if(m_Observers.ContainsKey(type))
        {
            m_Observers[type].Remove(action);
        }
    }

    public void InvokeObservers(ObserveType type, object value)
    {
        if(m_Observers.ContainsKey(type))
        {
            foreach (Action<object> action in m_Observers[type])
            {
                action(value);
            }
        }
    }
}
