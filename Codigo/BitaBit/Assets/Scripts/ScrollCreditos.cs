using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Importa pra carregar cenas

public class ScrollCreditos : MonoBehaviour
{
    public RectTransform creditsPanel; 
    public float scrollSpeed = 20f;    // Velocidade do movimento para cima
    public float endPositionY = 1600f; // Posição Y em que o crédito termina

    private Vector2 startPosition;
    private bool isScrolling = true;
    private float waitTime = 3f; // segundos para esperar no final
    private float timer = 0f;

    void Start()
    {
        startPosition = creditsPanel.anchoredPosition;
    }

   void Update()
    {
        if (isScrolling)
        {
            creditsPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (creditsPanel.anchoredPosition.y > endPositionY)
            {
                isScrolling = false; // para o movimento
                timer = 0f; 
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                SceneManager.LoadScene("Inicializadora");
            }
        }
    }
}