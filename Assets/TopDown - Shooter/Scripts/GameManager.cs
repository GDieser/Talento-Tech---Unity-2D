using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalRevolverBullets = 0;
    public int totalRifleBullets = 0;
    public int totalShotGunBullets = 0;
    public int totalPacks = 0;

    public bool IsLevel2 = false;

    public bool shotGun = false;

    public bool hablo, foto, sirena1, sirena2;

    public List<bool> itemMission;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetEstado()
    {
        totalRevolverBullets = 0;
        totalRifleBullets = 0;
        totalShotGunBullets = 0;
        totalPacks = 0;

        IsLevel2 = false;

        shotGun = false;

        hablo = foto = sirena1 = sirena2 = false;

        if(itemMission != null)
        {
            itemMission.Clear();
        }
    }

}
