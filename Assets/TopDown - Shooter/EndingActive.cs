using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingActive : MonoBehaviour
{
    [SerializeField] private GameObject ending;
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ending.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
