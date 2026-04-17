using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseInicial : MonoBehaviour
{
    void Start()
    {
        // Aguarda um frame para garantir que o FaseManager esteja inicializado
        StartCoroutine(CarregarMapa());
    }

    private System.Collections.IEnumerator CarregarMapa()
    {
        yield return null; // aguarda um frame
        SceneManager.LoadScene("Mapa");
    }
}
