using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvisoMision : MonoBehaviour
{
    [SerializeField] protected PlayerMission mision;

    [SerializeField] protected TextMeshProUGUI aviso;

    private bool isInRange = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isInRange = true;

            int cant = mision.itemMission.Count;

            if (cant < 3)
            {
                aviso.text = "Cuidado! Aún te faltan " + (3 - cant) + " Medikits";
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            aviso.text = "";
        }
    }


}
