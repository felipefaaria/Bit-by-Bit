using UnityEngine;
using System.Collections.Generic;

public class GeradorDeArvore : MonoBehaviour
{
    public GameObject prefabNode;
    public RectTransform container;
    public GameObject prefabLinha;

    public int quantidadeNiveis = 4;
    public int larguraPorNivel = 3;

    private void Start()
    {
        GerarArvore();
    }

    public void GerarArvore()
    {
        var fases = new List<List<FaseNode>>();
        float larguraTotal = container.rect.width;
        float alturaTotal = container.rect.height;


        for (int nivel = 0; nivel < quantidadeNiveis; nivel++)
        {
            int qtdNoNivel = (nivel == 0 || nivel == quantidadeNiveis - 1) ? 1 : larguraPorNivel;
            var lista = new List<FaseNode>();


            for (int i = 0; i < qtdNoNivel; i++)
            {
                float x = ((i + 1f) / (qtdNoNivel + 1f)) * larguraTotal - larguraTotal / 2f;
                float y = -((nivel + 1f) / (quantidadeNiveis + 1f)) * alturaTotal + alturaTotal / 2f;

                var node = new FaseNode(nivel, new Vector2(x, y));
                lista.Add(node);
            }

            fases.Add(lista);
        }

        // Conectar pais e filhos
        for (int nivel = 0; nivel < fases.Count - 1; nivel++)
        {
            var pais = fases[nivel];
            var filhos = fases[nivel + 1];

            foreach (var pai in pais)
            {
                foreach (var filho in filhos)
                {
                    if (!pai.filhos.Contains(filho))
                    {
                        pai.filhos.Add(filho);
                    }
                }
            }
        }

        // Instanciar visuais
        foreach (var nivel in fases)
        {
            foreach (var node in nivel)
            {
                GameObject go = Instantiate(prefabNode, container);
                var rt = go.GetComponent<RectTransform>();
                rt.anchoredPosition = node.posicao;

                node.visual = go;
                var visual = go.GetComponent<NodeVisual>();
                visual.Configurar(node);

            }
        }

        // Criar linhas
        foreach (var nivel in fases)
        {
            foreach (var pai in nivel)
            {
                foreach (var filho in pai.filhos)
                {
                    CriarLinhaEntre(pai, filho);
                }
            }
        }

        // Atualizar FaseManager
        if (FaseManager.Instance != null)
        {
            FaseManager.Instance.mapa = new List<FaseNode>();
            foreach (var nivel in fases)
            {
                FaseManager.Instance.mapa.AddRange(nivel);
            }

            if (FaseManager.Instance.nivelAtual >= fases.Count)
            {
                FaseManager.Instance.nivelAtual = 0;
            }

            FaseManager.Instance.noAtual = fases[FaseManager.Instance.nivelAtual][0];
        }
        else
        {
        }

    }

    private void CriarLinhaEntre(FaseNode pai, FaseNode filho)
    {
        GameObject linha = Instantiate(prefabLinha, container);
        RectTransform rt = linha.GetComponent<RectTransform>();

        Vector2 origem = pai.posicao;
        Vector2 destino = filho.posicao;
        Vector2 direcao = destino - origem;
        float comprimento = direcao.magnitude;

        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = origem + direcao / 2f;
        rt.sizeDelta = new Vector2(comprimento, 5f);
        rt.localRotation = Quaternion.FromToRotation(Vector3.right, direcao);

    }
}
