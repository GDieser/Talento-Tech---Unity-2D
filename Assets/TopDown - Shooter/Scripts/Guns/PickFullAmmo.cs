using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickFullAmmo : MonoBehaviour
{

    [SerializeField] private PlayerShotRevolver revolver;
    [SerializeField] private PlayerShotRifle rifle;
    [SerializeField] private PlayerShotShotGun shotGun;

    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private bool isRevolver = false;
    [SerializeField] private bool isRifle = false;
    [SerializeField] private bool isShotGun = false;

    public bool IsActive = false;
    private bool isInRange = false;
    private bool recarga = false;


    void Start()
    {

    }


    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            RecargarTotalBullets();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;

            if (isRevolver)
            {
                revolver = collision.GetComponent<PlayerShotRevolver>();

                if (revolver.GetTotalBullets() < 48)
                {
                    IsActive = true;
                    textMesh.text = "Presiona E para recargar todas las balas";
                }
                else
                {
                    IsActive = false;
                    textMesh.text = "";
                }

            }
            else if (isRifle)
            {
                rifle = collision.GetComponent<PlayerShotRifle>();

                if (rifle.GetTotalBullets() < 240)
                {
                    IsActive = true;
                    textMesh.text = "Presiona E para recargar todas las balas";
                }
                else
                {
                    IsActive = false;
                    textMesh.text = "";
                }
            }
            else if (isShotGun)
            {
                shotGun = collision.GetComponent<PlayerShotShotGun>();

                if (shotGun.GetTotalBullets() < 32)
                {
                    IsActive = true;
                    textMesh.text = "Presiona E para recargar todas las balas";
                }
                else
                {
                    IsActive = false;
                    textMesh.text = "";
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            textMesh.text = "";
        }
    }

    public void RecargarTotalBullets()
    {
        if (IsActive && revolver != null && isRevolver)
        {
            revolver.AddBullet(48);
            textMesh.text = "";
            IsActive = false;
        }
        else if(IsActive && rifle != null && isRifle)
        {
            rifle.AddBullet(240);
            textMesh.text = "";
            IsActive = false;
        }
        else if (IsActive && shotGun != null && isShotGun)
        {
            shotGun.AddBullet(32, 32);
            textMesh.text = "";
            IsActive = false;
        }

    }

}
