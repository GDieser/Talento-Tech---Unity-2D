using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    [SerializeField] private AudioClip audioGun;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(this.gameObject.CompareTag("Rifle"))
            {
                collision.GetComponent<PlayerShotRifle>().active = true;
                SoundController.instance.PlaySound(audioGun, 0.8f);
                //PlayerShotRifle rifle = GetComponent<PlayerShotRifle>();
                //rifle.active = true;
            }
            else if(this.gameObject.CompareTag("Shotgun"))
            {
                collision.GetComponent<PlayerShotShotGun>().SetActive(true);
                SoundController.instance.PlaySound(audioGun, 0.8f);
            }
            
            gameObject.SetActive(false);
        }
    }
}
