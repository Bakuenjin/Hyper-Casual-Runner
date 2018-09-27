using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicManager : MonoBehaviour {

    [SerializeField]
    private GroundMovement m_Ground;

	// Update is called once per frame
	public void ManageGraphics () {
        m_Ground.Move();
	}
}
