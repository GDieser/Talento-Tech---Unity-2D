using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCursorZombie : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;

    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 cursorHotspotDefault;


    private void Start()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
        if(Time.timeScale > 0f)
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        if (Time.timeScale > 0f)
            Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);
    }
}
