using UnityEngine;
using System.Collections.Generic;

public class SeletorDeFase : MonoBehaviour
{
    public GameObject setaPrefab;
    private GameObject setaInstanciada;

    private List<FaseNode> nosDoNivelAtual;
    private int indiceAtual = 0;

    void Start()
    {
        nosDoNivelAtual = new List<FaseNode>();
        foreach (var no in FaseManager.Instance.mapa)
        {
            if (no.nivel == FaseManager.Instance.nivelAtual)
                nosDoNivelAtual.Add(no);
        }

        if (nosDoNivelAtual.Count == 0)
        {
            return;
        }

        indiceAtual = nosDoNivelAtual.IndexOf(FaseManager.Instance.noAtual);

        setaInstanciada = Instantiate(setaPrefab, transform);
        AtualizarSeta();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            indiceAtual = (indiceAtual + 1) % nosDoNivelAtual.Count;
            AtualizarSeta();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            indiceAtual = (indiceAtual - 1 + nosDoNivelAtual.Count) % nosDoNivelAtual.Count;
            AtualizarSeta();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            FaseManager.Instance.IrParaNode(nosDoNivelAtual[indiceAtual]);
        }
    }

    void AtualizarSeta()
    {
        if (setaInstanciada != null && nosDoNivelAtual.Count > 0)
        {
            setaInstanciada.transform.localPosition = nosDoNivelAtual[indiceAtual].posicao + Vector2.up * 60f;
        }
    }
}
