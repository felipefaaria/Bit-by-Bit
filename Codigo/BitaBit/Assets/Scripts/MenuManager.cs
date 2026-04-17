using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void IniciarJogo()
    {
        SceneManager.LoadScene("Mapa");
    }

    public void AbrirCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void SairDoJogo()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para parar no editor
#else
        Application.Quit(); // Fecha o jogo na build
#endif
    }
}
