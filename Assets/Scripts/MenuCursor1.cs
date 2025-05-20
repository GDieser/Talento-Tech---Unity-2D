using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCursor1 : MonoBehaviour
{
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 cursorHotspotDefault;

    private void Start()
    {
        Cursor.SetCursor(cursorDefault, cursorHotspotDefault, CursorMode.Auto);
    }

}
