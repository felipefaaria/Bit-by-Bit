using UnityEngine;

public class PersonagemBase : MonoBehaviour, IPersonagem
{
    [SerializeField] protected string nome = "Personagem";
    [SerializeField] protected int vidaAtual = 10;
    [SerializeField] protected bool ehAliado = true;
    public bool estaVivo = true;

    public string Nome => nome;
    public int VidaAtual => vidaAtual;
    public bool EhAliado => ehAliado;


    public void SetValores(string nome, int vida, bool aliado)
    {
        this.nome = nome;
        this.vidaAtual = vida;
        this.ehAliado = aliado; 
    }

}
