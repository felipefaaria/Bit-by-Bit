// FaseManager.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseManager : MonoBehaviour
{
    public static FaseManager Instance;

    [Header("Estado atual do mapa (qual período (nível do grafo) estamos no jogo)")]
    public int nivelAtual = 1;      // começa em 1 para existir nó nesse nível
    public FaseNode noAtual;
    public List<FaseNode> mapa;

    [Header("Configuração de níveis")]
    private const int MAX_LEVELS = 5;

    [Header("Contagem de nós por nível interno (níveis 2 a MAX_LEVELS-1)")]
    [SerializeField] private int minNodesPerLevel = 2;
    [SerializeField] private int maxNodesPerLevel = 5;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateMap();
        // define nó atual como o primeiro, se ainda não houver
        if (noAtual == null && mapa.Count > 0)
            noAtual = mapa[0];
    }

    /// <summary>
    /// Gera o grafo com MAX_LEVELS níveis:
    /// níveis 1 e MAX_LEVELS têm 1 nó;
    /// níveis intermediários têm quantidade aleatória entre min e max.
    /// </summary>
    private void GenerateMap()
    {
        mapa = new List<FaseNode>();

        // 1) Criação dos nós
        for (int lvl = 1; lvl <= MAX_LEVELS; lvl++)
        {
            int count;
            if (lvl == 1 || lvl == MAX_LEVELS)
            {
                count = 1;
            }
            else
            {
                // sorteia inclusivo entre minNodesPerLevel e maxNodesPerLevel
                count = Random.Range(minNodesPerLevel, maxNodesPerLevel + 1);
            }

            for (int i = 0; i < count; i++)
            {
                Vector2 pos = CalculateNodePosition(lvl, count, i);
                FaseNode node = new FaseNode(lvl, pos);
                node.dificuldade = GetRandomDifficulty(lvl);
                mapa.Add(node);
            }
        }

        // 2) Conexão entre níveis adjacentes (lvl → lvl+1)
        foreach (var node in mapa)
        {
            node.filhos = new List<FaseNode>();
            if (node.nivel < MAX_LEVELS)
            {
                foreach (var nxt in mapa)
                {
                    if (nxt.nivel == node.nivel + 1)
                        node.filhos.Add(nxt);
                }
            }
        }
    }

    /// <summary>
    /// Sorteia a dificuldade de acordo com o nível:
    /// 1 → Easy; MAX_LEVELS → Hard; intermediários com probabilidade.
    /// </summary>
    private Difficulty GetRandomDifficulty(int lvl)
    {
        if (lvl == 1) return Difficulty.Easy;
        if (lvl == MAX_LEVELS) return Difficulty.Hard;

        float easyChance = 0.6f - (lvl - 2) * 0.1f;    // lvl2:0.6, lvl3:0.5, lvl4:0.4
        float hardChance = 0.2f + (lvl - 2) * 0.1f;    // lvl2:0.2, lvl3:0.3, lvl4:0.4
        float r = Random.value;

        if (r < easyChance)        return Difficulty.Easy;
        if (r > 1 - hardChance)    return Difficulty.Hard;
        return Difficulty.Medium;
    }

    /// <summary>
    /// Calcula a posição em pixels (UI) de cada nó,
    /// centralizando count nós no nível lvl.
    /// </summary>
    private Vector2 CalculateNodePosition(int lvl, int count, int index)
    {
        // espacemento fixo em pixels (ajuste conforme sua UI)
        float gapX = 200f;  // 200px entre colunas
        float gapY = 100f;  // 100px entre linhas

        float x = (index - (count - 1) / 2f) * gapX;
        float y = ((MAX_LEVELS + 1) / 2f - lvl) * gapY;
        return new Vector2(x, y);
    }

    /// <summary>
    /// Volta do combate para o mapa, avançando nivelAtual.
    /// </summary>
    public void VoltarParaMapa()
    {
        nivelAtual++;
        SceneManager.LoadScene("Mapa");
    }

    /// <summary>
    /// Vai para a cena de luta do nó selecionado e avança o nível.
    /// </summary>
    public void IrParaNode(FaseNode proximo)
    {
        noAtual = proximo;
        nivelAtual++;
        SceneManager.LoadScene("CenaLuta");
    }
}
