using UnityEngine;
using UnityEngine.Tilemaps;

public class GeradorDeTiles : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private TileBase tileCampo;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap tilemapUsuario;
    [SerializeField] private Tilemap tilemapInimigo;

    [Header("Configuração de campo")]
    [SerializeField] private Vector2Int tamanhoCampo = new Vector2Int(3, 3);
    [SerializeField] private int distanciaEntreCampos = 4;

    void Start()
    {
        Vector3Int centro = new Vector3Int(-5, -2, 0);

        Vector3Int origemUsuario = centro + new Vector3Int(-tamanhoCampo.x - distanciaEntreCampos / 2, -3, 0);
        Vector3Int origemInimigo = centro + new Vector3Int(distanciaEntreCampos / 2, -5, 0);

        CriarCampo(tilemapUsuario, origemUsuario, "USUÁRIO");
        CriarCampo(tilemapInimigo, origemInimigo, "INIMIGO");
    }

    private void CriarCampo(Tilemap tilemap, Vector3Int origem, string nome)
    {
        if (tileCampo == null)
        {
            return;
        }
        for (int x = 0; x < tamanhoCampo.x; x++)
        {
            for (int y = 0; y < tamanhoCampo.y; y++)
            {
                Vector3Int pos = new Vector3Int(origem.x + x, origem.y + y, 0);
                tilemap.SetTile(pos, tileCampo);

            }
        }
    }
}
