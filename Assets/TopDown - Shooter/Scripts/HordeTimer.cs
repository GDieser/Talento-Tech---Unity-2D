using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HordeTimer : MonoBehaviour
{
    private int segundos;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMesh.text = (segundos.ToString());
    }

    public void setTimer(int seg)
    {
        segundos = seg;
    }

}
