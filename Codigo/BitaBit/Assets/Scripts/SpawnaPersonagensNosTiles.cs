using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;



public class SpawnaPersonagensNosTiles : MonoBehaviour
{
    [SerializeField] private List<GameObject> personagensPrefabs = new();
    [SerializeField] private Tilemap tilemap; 
    public List<GameObject> inimigosInstanciados = new();

    private void Start()
    {
        Invoke(nameof(SpawnarPersonagens), 0.1f);
    }

    private void SpawnarPersonagens()
    {

        if (personagensPrefabs.Count == 0)
        {
            return;
        }

        List<Vector3Int> celulasValidas = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                celulasValidas.Add(pos);
            }
        }

        if (celulasValidas.Count == 0)
        {
            return;
        }

        int quantidadeParaSpawnar = Mathf.Min(personagensPrefabs.Count, celulasValidas.Count);
        List<Vector3Int> posicoesEscolhidas = new List<Vector3Int>();

        while (posicoesEscolhidas.Count < quantidadeParaSpawnar)
        {
            Vector3Int pos = celulasValidas[Random.Range(0, celulasValidas.Count)];
            if (!posicoesEscolhidas.Contains(pos))
                posicoesEscolhidas.Add(pos);
        }

        for (int i = 0; i < quantidadeParaSpawnar; i++)
        {
            GameObject prefab = personagensPrefabs[Random.Range(0, personagensPrefabs.Count)];
            if (prefab == null)
            {
                continue;
            }

            Vector3Int celula = posicoesEscolhidas[i];
            Vector3 posMundo = tilemap.GetCellCenterWorld(celula);
            posMundo.z = -1;
            posMundo += new Vector3(0, 0.5f, 0);

            GameObject personagem = Instantiate(prefab, posMundo, Quaternion.identity);
            inimigosInstanciados.Add(personagem); 

            int ordem = Mathf.RoundToInt(-posMundo.y * 100);
            personagem.GetComponent<SpriteRenderer>().sortingOrder = ordem;

            var dados = personagem.GetComponent<PersonagemBase>();
            if (dados == null)
            {
                continue;
            }

            dados.SetValores(dados.Nome, dados.VidaAtual, false);
            string nome = dados.Nome + $" ({celula.x}, {celula.y})";
            personagem.name = nome;
        }
        FindObjectOfType<ControladorDeSelecao>()?.DefinirInimigos(inimigosInstanciados);


    }
}
