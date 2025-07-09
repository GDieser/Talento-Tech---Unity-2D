using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class PlayerMission : MonoBehaviour
{
    [SerializeField] public List<bool> itemMission;
    [SerializeField] public bool tools = false;

    [SerializeField] private Image item1;
    [SerializeField] private Image item2;
    [SerializeField] private Image item3;
    [SerializeField] private Image tool;

    void Start()
    {
        itemMission = new List<bool>();
    }

    void Update()
    {
        Debug.Log(itemMission.Count);

        if (itemMission.Count == 1)
            item1.enabled = true;
        else if (itemMission.Count == 2)
            item2.enabled = true;
        else if (itemMission.Count == 3)
            item3.enabled = true;

        if (tools)
            tool.enabled = true;
    }

}
