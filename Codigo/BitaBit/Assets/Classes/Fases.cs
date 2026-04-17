using System.Collections.Generic;
using UnityEngine;

public class Fases
{
    public int nivelAtual = 1;
    public List<List<FaseNode>> niveis = new(); 

    public FaseNode noAtual;
}
