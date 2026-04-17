// ControladorDeMapa.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeMapa : MonoBehaviour
{
    [Header("Prefabs e Container para instanciar nós e arestas")]
    [SerializeField] private GameObject nodePrefab;        // Prefab do nó (UI Button/Image)
    [SerializeField] private GameObject edgePrefab;        // Prefab da aresta (UI Image 1×N)
    [SerializeField] private RectTransform mapContainer;   // RectTransform do MapContainer no Canvas

    private List<FaseNode> nosDoNivelAtual = new List<FaseNode>();
    private int indiceAtual = 0;

    IEnumerator Start()
    {
        // 1) Espera o FaseManager gerar o mapa
        while (FaseManager.Instance == null || FaseManager.Instance.mapa == null || FaseManager.Instance.mapa.Count == 0)
            yield return null;

        // 2) Filtra apenas nós do nível atual
        int nivel = FaseManager.Instance.nivelAtual;
        nosDoNivelAtual.Clear();
        foreach (var no in FaseManager.Instance.mapa)
            if (no.nivel == nivel)
                nosDoNivelAtual.Add(no);

        if (nosDoNivelAtual.Count == 0)
        {
            yield break;
        }

        // 3) Define o nó inicial caso seja a primeira vez
        if (FaseManager.Instance.noAtual == null)
        {
            indiceAtual = 0;
            FaseManager.Instance.noAtual = nosDoNivelAtual[0];
        }
        else
        {
            indiceAtual = nosDoNivelAtual.IndexOf(FaseManager.Instance.noAtual);
            if (indiceAtual < 0) indiceAtual = 0;
            FaseManager.Instance.noAtual = nosDoNivelAtual[indiceAtual];
        }

        // 4) Desenha o grafo e destaca o nó selecionado
        RenderMap();
        HighlightNode(indiceAtual);
    }

    void Update()
    {
        // Navegação horizontal entre nós do mesmo nível
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            indiceAtual = (indiceAtual + 1) % nosDoNivelAtual.Count;
            FaseManager.Instance.noAtual = nosDoNivelAtual[indiceAtual];
            HighlightNode(indiceAtual);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            indiceAtual = (indiceAtual - 1 + nosDoNivelAtual.Count) % nosDoNivelAtual.Count;
            FaseManager.Instance.noAtual = nosDoNivelAtual[indiceAtual];
            HighlightNode(indiceAtual);
        }
        // Seleção de fase
        else if (Input.GetKeyDown(KeyCode.A))
        {
            var node = FaseManager.Instance.noAtual;

            // Primeiro nível: tutorial
            if (node.nivel == 1)
            {
                SceneManager.LoadScene("PrimeiraLuta");
                return;
            }

            // Último nível (sem filhos): chefão
            if (node.filhos.Count == 0)
            {
                SceneManager.LoadScene("Chefao");
                return;
            }

            // Intermediários: conforme dificuldade
            switch (node.dificuldade)
            {
                case Difficulty.Easy:
                    SceneManager.LoadScene("LutaFacil");
                    break;
                case Difficulty.Medium:
                    SceneManager.LoadScene("LutaMedia");
                    break;
                case Difficulty.Hard:
                    SceneManager.LoadScene("Chefao");
                    break;
            }
        }
    }

    /// <summary>
    /// Desenha todos os nós e as arestas no MapContainer.
    /// </summary>
    private void RenderMap()
    {
        // Limpa instâncias anteriores
        foreach (Transform child in mapContainer)
            Destroy(child.gameObject);

        // Instancia nós
        foreach (var node in FaseManager.Instance.mapa)
        {
            var go = Instantiate(nodePrefab, mapContainer);
            var rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = node.posicao;
            node.visual = go;
        }

        // Instancia arestas entre cada nó e seus filhos
        foreach (var node in FaseManager.Instance.mapa)
        {
            foreach (var filho in node.filhos)
            {
                var edge = Instantiate(edgePrefab, mapContainer);
                var rtEdge = edge.GetComponent<RectTransform>();

                Vector2 a = node.visual.GetComponent<RectTransform>().anchoredPosition;
                Vector2 b = filho.visual.GetComponent<RectTransform>().anchoredPosition;
                Vector2 dir = b - a;

                float dist = dir.magnitude;
                float ang  = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                rtEdge.sizeDelta        = new Vector2(dist, rtEdge.sizeDelta.y);
                rtEdge.pivot            = new Vector2(0, 0.5f);
                rtEdge.anchoredPosition = a;
                rtEdge.localRotation    = Quaternion.Euler(0, 0, ang);
            }
        }
    }

    /// <summary>
    /// Pinta de verde o nó selecionado e de branco os demais.
    /// </summary>
    private void HighlightNode(int newIndex)
    {
        for (int i = 0; i < nosDoNivelAtual.Count; i++)
        {
            var no = nosDoNivelAtual[i];
            var img = no.visual.GetComponent<Image>();
            if (img != null)
                img.color = (i == newIndex) ? Color.green : Color.white;
        }
    }
}
