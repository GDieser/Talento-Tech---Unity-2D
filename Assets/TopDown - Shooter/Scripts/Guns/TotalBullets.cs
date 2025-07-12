 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class TotalBullets : MonoBehaviour
{
    private int totalBullets = 0;
    private int bullets;
    private TextMeshProUGUI textMesh;


    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = (bullets.ToString() + "/" + totalBullets.ToString());
    }

    public void AddBullets(int add)
    {
        totalBullets += add;
    }

    public void setBullets(int bullet)
    {
        bullets = bullet;
    }

    public void setTotalBullets(int totalBullet)
    {
        totalBullets = totalBullet;
    }
}
