using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IndicadorDeFase : MonoBehaviour
{
    public GameObject setaPrefab;
    private GameObject setaInstanciada;

    private List<FaseNode> nosDoNivelAtual = new();
    private int indiceAtual = 0;

    IEnumerator Start() // ? AGORA é uma corrotina!
    {
        // Espera o FaseManager estar pronto e os visuais gerados
        yield return new WaitUntil(() =>
            FaseManager.Instance != null &&
            FaseManager.Instance.mapa != null &&
            FaseManager.Instance.mapa.Count > 0 &&
            FaseManager.Instance.mapa[0].visual != null
        );

        setaInstanciada = Instantiate(setaPrefab, transform);

        foreach (var no in FaseManager.Instance.mapa)
        {
            if (no.nivel == FaseManager.Instance.nivelAtual)
                nosDoNivelAtual.Add(no);
        }

        if (nosDoNivelAtual.Count == 0)
        {
            yield break;
        }

        FaseManager.Instance.noAtual = nosDoNivelAtual[0];
        AtualizarSeta();
    }

    void Update()
    {
        if (nosDoNivelAtual.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            indiceAtual = (indiceAtual + 1) % nosDoNivelAtual.Count;
            FaseManager.Instance.noAtual = nosDoNivelAtual[indiceAtual];
            AtualizarSeta();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            indiceAtual = (indiceAtual - 1 + nosDoNivelAtual.Count) % nosDoNivelAtual.Count;
            FaseManager.Instance.noAtual = nosDoNivelAtual[indiceAtual];
            AtualizarSeta();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            FaseManager.Instance.IrParaNode(FaseManager.Instance.noAtual);
        }
    }

    void AtualizarSeta()
    {
        var pos = nosDoNivelAtual[indiceAtual].visual.transform.position;
        setaInstanciada.transform.position = pos + Vector3.up * 1f;
    }
}
