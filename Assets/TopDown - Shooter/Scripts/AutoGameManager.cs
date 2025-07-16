using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGameManager : MonoBehaviour
{
    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameObject gm = new GameObject("GameManager");
            gm.AddComponent<GameManager>();
        }
    }

}
