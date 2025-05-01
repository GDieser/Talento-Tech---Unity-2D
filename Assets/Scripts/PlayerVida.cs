using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVida : MonoBehaviour
{
    [SerializeField] public int vida = 4;
    public bool muerto;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void RecibeDamage(int damage)
    {
        vida -= damage;
    }
}
