using UnityEngine;

public class AjustadorDePlanoFundo : MonoBehaviour
{
    void Start()
    {
        Ajustar();
    }

    private void Ajustar()
    {
        Camera cam = Object.FindAnyObjectByType<Camera>();

        if (cam == null)
        {
            return;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            return;
        }

        float distanciaZ = Mathf.Abs(cam.transform.position.z);

        Vector3 cantoInferiorEsquerdo = cam.ViewportToWorldPoint(new Vector3(0f, 0f, distanciaZ));
        Vector3 cantoSuperiorDireito = cam.ViewportToWorldPoint(new Vector3(1f, 1f, distanciaZ));

        Vector3 centro = (cantoInferiorEsquerdo + cantoSuperiorDireito) / 2f;
        float larguraVisivel = cantoSuperiorDireito.x - cantoInferiorEsquerdo.x;
        float alturaVisivel = cantoSuperiorDireito.y - cantoInferiorEsquerdo.y;

        Vector3 tamanhoOriginal = renderer.bounds.size;

        float escalaX = larguraVisivel / tamanhoOriginal.x;
        float escalaY = alturaVisivel / tamanhoOriginal.y;

        transform.position = new Vector3(centro.x, centro.y, 0f);
        transform.localScale = new Vector3(escalaX, escalaY, 1f);
    }
}
