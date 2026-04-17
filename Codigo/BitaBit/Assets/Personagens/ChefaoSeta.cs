using UnityEngine;

public class ChefaoSeta
    : PersonagemBase
{
    [SerializeField] private Sprite[] frameArray;
    private int currentFrame;
    private float timer;
    private float frameRate = 0.2f;
    private SpriteRenderer spriteRenderer;
    private int direction = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        nome = "Seta Parada";
        vidaAtual = 8;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrame += direction;

            if (currentFrame >= frameArray.Length)
            {
                currentFrame = frameArray.Length - 1;
                direction = -1;
            }
            else if (currentFrame < 0)
            {
                currentFrame = 0;
                direction = 1;
            }

            spriteRenderer.sprite = frameArray[currentFrame];

        }
    }
}
