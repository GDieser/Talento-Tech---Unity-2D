using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalRevolverBullets = 0;
    public int totalRifleBullets = 0;
    public int totalShotGunBullets = 0;
    public int totalPacks = 0;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
