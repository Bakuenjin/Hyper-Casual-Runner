using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip[] m_Sounds;
    [SerializeField] private GameObject m_AudioHolder;

    private List<AudioSource> m_AudioHolders;
    private List<AudioSource> m_DestroyableAudios;

    private void Awake()
    {
        m_DestroyableAudios = new List<AudioSource>();
        m_AudioHolders = new List<AudioSource>();
    }

    // Use this for initialization
    private void Start()
    {
        ObserverManager.m_Instance.AddObserver(ObserveType.OnJump, PlayAudio);
        ObserverManager.m_Instance.AddObserver(ObserveType.OnDeath, PlayAudio);
	}

    private void Update()
    {
        if(m_AudioHolders.Count > 0)
        {
            foreach(AudioSource aSrc in m_AudioHolders)
            {
                if(!aSrc.isPlaying)
                {
                    m_DestroyableAudios.Add(aSrc);
                }
            }

            foreach(AudioSource aSrc in m_DestroyableAudios)
            {
                m_AudioHolders.Remove(aSrc);
                Destroy(aSrc.gameObject);
            }

            m_DestroyableAudios = new List<AudioSource>();
        }
    }

    // Update is called once per frame
    void PlayAudio(object o)
    {
        GameObject go = Instantiate(m_AudioHolder, Camera.main.transform.position, Quaternion.identity);
        AudioSource aSrc = go.GetComponent<AudioSource>();
        aSrc.clip = m_Sounds[(int)o];
        aSrc.Play();

        m_AudioHolders.Add(go.GetComponent<AudioSource>());
    }
}
