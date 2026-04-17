using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuSelector : MonoBehaviour
{
    [SerializeField] private List<Button> botoes = new();
    [SerializeField] private Image botaoVerde; 
    [SerializeField] private Image botaoAzul;  
    [SerializeField] private Image botaoRoxo;  

    private int indiceAtual = 0;

    private Color corOriginalVerde;
    private Color corOriginalAzul;
    private Color corOriginalRoxo;

    // Cores alteradas ao passar o mouse sobre os botões
    private Color corVerdeClara = new Color(0.35f, 0.8f, 0.35f, 1f); 
    private Color corAzulClara = new Color(0.5f, 0.75f, 1f, 1f); 
    private Color corRoxoClara = new Color(0.75f, 0.5f, 1f, 1f); 

    void Start()
    {
        if (botoes.Count > 0)
        {
            SelecionarBotao(indiceAtual);
        }

        if (botaoVerde != null) corOriginalVerde = botaoVerde.color;
        if (botaoAzul != null) corOriginalAzul = botaoAzul.color;
        if (botaoRoxo != null) corOriginalRoxo = botaoRoxo.color;

        // Botao Iniciar
        if (botoes.Count > 0 && botoes[0] != null)
        {
            AddEventTriggers(botoes[0].gameObject, MudarCorVerdeParaClara, RestaurarCorOriginalVerde);
        }

        // Botao Configurações
        if (botoes.Count > 1 && botoes[1] != null)
        {
            AddEventTriggers(botoes[1].gameObject, MudarCorAzulParaClara, RestaurarCorOriginalAzul);
        }

        // Botao Sair
        if (botoes.Count > 2 && botoes[2] != null)
        {
            AddEventTriggers(botoes[2].gameObject, MudarCorRoxoParaClara, RestaurarCorOriginalRoxo);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceAtual = (indiceAtual + 1) % botoes.Count;
            SelecionarBotao(indiceAtual);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceAtual = (indiceAtual - 1 + botoes.Count) % botoes.Count;
            SelecionarBotao(indiceAtual);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            botoes[indiceAtual].onClick.Invoke();
        }
    }

    private void SelecionarBotao(int indice)
    {
        EventSystem.current.SetSelectedGameObject(botoes[indice].gameObject);
    }

    private void AddEventTriggers(GameObject obj, UnityEngine.Events.UnityAction<BaseEventData> onEnter, UnityEngine.Events.UnityAction<BaseEventData> onExit)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null) trigger = obj.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener(onEnter);
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener(onExit);
        trigger.triggers.Add(entryExit);
    }

    // Métodos para BotaoVerde
    private void MudarCorVerdeParaClara(BaseEventData eventData)
    {
        if (botaoVerde != null) botaoVerde.color = corVerdeClara;
    }

    private void RestaurarCorOriginalVerde(BaseEventData eventData)
    {
        if (botaoVerde != null) botaoVerde.color = corOriginalVerde;
    }

    // Métodos para BotaoAzul
    private void MudarCorAzulParaClara(BaseEventData eventData)
    {
        if (botaoAzul != null) botaoAzul.color = corAzulClara;
    }

    private void RestaurarCorOriginalAzul(BaseEventData eventData)
    {
        if (botaoAzul != null) botaoAzul.color = corOriginalAzul;
    }

    // Métodos para BotaoRoxo
    private void MudarCorRoxoParaClara(BaseEventData eventData)
    {
        if (botaoRoxo != null) botaoRoxo.color = corRoxoClara;
    }

    private void RestaurarCorOriginalRoxo(BaseEventData eventData)
    {
        if (botaoRoxo != null) botaoRoxo.color = corOriginalRoxo;
    }
}