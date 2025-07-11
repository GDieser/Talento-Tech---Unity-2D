using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class TotalHelthPack : MonoBehaviour
{
    private int totalPack = 0;
    private TextMeshProUGUI textMeshPro;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = ("x " + totalPack.ToString());
    }

    public void AddPack(int add)
    {
        totalPack += add;
    }

    public void setPack(int pack)
    {
        totalPack = pack;
    }

}
