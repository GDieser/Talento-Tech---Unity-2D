using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupItemMission : MonoBehaviour
{
    [SerializeField] private AudioClip audioItem;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMission item = (PlayerMission)collision.gameObject.GetComponent<PlayerMission>();

            if (this.gameObject.CompareTag("Item"))
            {
                item.itemMission.Add(true);
                SoundController.instance.PlaySound(audioItem, 0.8f);
            }
            else if (this.gameObject.CompareTag("Tools"))
            {
                item.tools = true;
                SoundController.instance.PlaySound(audioItem, 0.8f);
            }
            //item.ActiveImage();
            gameObject.SetActive(false);
        }
    }
}
