using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyController {
    
    void UpdatePosition();
    GameObject GetGameObject();
    float GetCurrentPositionX();
}
