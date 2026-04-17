// FaseNode.cs
using System.Collections.Generic;
using UnityEngine;

// Guarda internamente a dificuldade de cada nó (não exibida ao jogador)
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class FaseNode
{
    public int nivel;
    public Vector2 posicao;
    public List<FaseNode> filhos = new List<FaseNode>();
    public GameObject visual;

    // Novo campo para armazenar a dificuldade escolhida aleatoriamente
    public Difficulty dificuldade;

    public FaseNode(int nivel, Vector2 posicao)
    {
        this.nivel   = nivel;
        this.posicao = posicao;
    }

    public override string ToString()
    {
        return $"FaseNode(nível: {nivel}, dificuldade: {dificuldade}, posição: {posicao})";
    }
}
