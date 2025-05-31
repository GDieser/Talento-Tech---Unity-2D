using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HordeActive : MonoBehaviour
{
    [SerializeField] private HordeCanvaScript horde;
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            horde.HordeActive(true);
            this.gameObject.SetActive(false);
        }
    }

}
