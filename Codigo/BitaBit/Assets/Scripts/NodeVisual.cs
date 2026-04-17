using UnityEngine;
using UnityEngine.UI;

public class NodeVisual : MonoBehaviour
{
    private FaseNode no;
    private Image imagem;

    public Color corAtual = Color.green;
    public Color corFutura = Color.red;

    private void Awake()
    {
        imagem = GetComponent<Image>();
    }

    public void Configurar(FaseNode fase)
    {
        no = fase;
        AtualizarCor();
    }

    private void Update()
    {
        AtualizarCor();
    }

    private void AtualizarCor()
    {
        if (FaseManager.Instance == null || no == null || imagem == null)
            return;

        if (FaseManager.Instance.noAtual == no)
            imagem.color = corAtual;
        else
            imagem.color = corFutura;
    }
}
