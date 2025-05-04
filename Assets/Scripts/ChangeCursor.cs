using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
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
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);
    }

}
