using UnityEngine;

public class CursorMenu : MonoBehaviour
{
    public Texture2D cursorSprite;
    public Vector2 hotspot = Vector2.zero; 

    void Start()
    {
        Cursor.SetCursor(cursorSprite, hotspot, CursorMode.Auto);
    }

    void OnDestroy()
    {
        // Restaura o cursor padrão ao sair da cena
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
