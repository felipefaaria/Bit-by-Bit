using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ControladorDeSelecao : MonoBehaviour
{
    [SerializeField] private GameObject cursorPrefab;

    private List<GameObject> aliados = new();
    private List<GameObject> aliadosDisponiveis = new();
    private List<GameObject> inimigos = new();
    private List<(GameObject aliado, GameObject inimigo)> confrontos = new();
    private GameObject aliadoSelecionadoAtual = null;
    [SerializeField] private PopupManager popupManager;

    private int indexAtual = 0;
    private bool selecionandoAliado = true;
    private GameObject cursor;

    public void DefinirAliados(List<GameObject> novosAliados)
    {
        aliados = novosAliados;
        aliadosDisponiveis = new List<GameObject>(novosAliados);


        if (cursor == null)
        {
            cursor = Instantiate(cursorPrefab);
        }

        selecionandoAliado = true;
        indexAtual = 0;
        AtualizarCursor();
    }

    public void DefinirInimigos(List<GameObject> novosInimigos)
    {
        inimigos = novosInimigos;
    }

    private void Update()
    {
        if ((selecionandoAliado ? aliadosDisponiveis.Count : inimigos.Count) == 0)
            return;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoverCursor(1);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoverCursor(-1);

        if (Input.GetKeyDown(KeyCode.A))
            TrocarLado();
    }

    private void MoverCursor(int direcao)
    {
        var lista = selecionandoAliado ? aliadosDisponiveis : inimigos;

        if (lista.Count == 0)
        {
            return;
        }

        indexAtual += direcao;

        if (indexAtual < 0)
            indexAtual = lista.Count - 1;
        else if (indexAtual >= lista.Count)
            indexAtual = 0;


        AtualizarCursor();
    }

    private void TrocarLado()
    {
        if (selecionandoAliado)
        {
            if (aliadosDisponiveis.Count == 0)
            {
                cursor.SetActive(false);
                return;
            }

            aliadoSelecionadoAtual = aliadosDisponiveis[indexAtual];

            aliadosDisponiveis.RemoveAt(indexAtual);

            if (indexAtual >= aliadosDisponiveis.Count)
                indexAtual = 0;

            selecionandoAliado = false;
            indexAtual = 0;
            AtualizarCursor();
            return;
        }

        if (!selecionandoAliado && inimigos.Count > 0 && aliadoSelecionadoAtual != null)
        {
            var inimigo = inimigos[indexAtual];
            confrontos.Add((aliadoSelecionadoAtual, inimigo));
            aliadoSelecionadoAtual = null;

            if (aliadosDisponiveis.Count == 0)
            {
                cursor.SetActive(false);
                Invoke(nameof(ExecutarCombate), 0.1f);
                return;
            }

            selecionandoAliado = true;
            indexAtual = 0;
            AtualizarCursor();
        }
    }

    private void AtualizarCursor()
    {
        var lista = selecionandoAliado ? aliadosDisponiveis : inimigos;
        if (lista.Count == 0)
        {
            return;
        }

        if (indexAtual >= lista.Count || lista[indexAtual] == null)
        {
            MoverCursor(1);
            return;
        }

        var alvo = lista[indexAtual];

        if (alvo == null)
        {
            return;
        }

        cursor.transform.position = alvo.transform.position + Vector3.up * 1f;
    }


    private void ReiniciarRodadaAliados()
    {
        aliadosDisponiveis = new List<GameObject>(aliados);
        indexAtual = 0;
        selecionandoAliado = true;
        cursor.SetActive(true);
        AtualizarCursor();

        foreach (var duelo in confrontos)
        {
        }

        confrontos.Clear(); 
    }

    public void ExecutarCombate()
    {
        StartCoroutine(ExecutarConfrontos());
    }

    private IEnumerator ExecutarConfrontos()
    {
        var confrontosCopia = new List<(GameObject aliado, GameObject inimigo)>(confrontos);

        foreach (var par in confrontosCopia)
        {
            var aliado = par.aliado;
            var inimigo = par.inimigo;

            if (aliado == null || inimigo == null)
            {
                continue;
            }

            PersonagemBase inimigoBase = null;
            try
            {
                inimigoBase = inimigo.GetComponent<PersonagemBase>();
            }
            catch
            {
                continue;
            }

            if (inimigoBase == null || !inimigoBase.estaVivo)
            {
                continue;
            }


            Vector3 posAliadoInicial = aliado.transform.position;
            Vector3 posInimigoInicial = inimigo.transform.position;
            Vector3 meio = Vector3.Lerp(posAliadoInicial, posInimigoInicial, 0.5f);

            float duracao = 0.5f;
            float tempo = 0f;

            while (tempo < duracao)
            {
                tempo += Time.deltaTime;
                float t = tempo / duracao;
                aliado.transform.position = Vector3.Lerp(posAliadoInicial, meio, t);
                inimigo.transform.position = Vector3.Lerp(posInimigoInicial, meio, t);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            tempo = 0f;
            while (tempo < duracao)
            {
                tempo += Time.deltaTime;
                float t = tempo / duracao;
                aliado.transform.position = Vector3.Lerp(meio, posAliadoInicial, t);
                inimigo.transform.position = Vector3.Lerp(meio, posInimigoInicial, t);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            if (Random.value < 0.5f)
            {
                inimigoBase.estaVivo = false;
                Destroy(inimigo);
                inimigos.Remove(inimigo);
            }
            else
            {
            }
        }

        confrontos.Clear();

        if (inimigos.Count == 0)
        {

            if (popupManager != null)
                popupManager.MostrarVitoria();
            else

            yield return new WaitForSeconds(3f);

          
            if (FaseManager.Instance.nivelAtual >= 3)
            {
                Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Para parar no editor
#endif
                yield break;
            }

            FaseManager.Instance.VoltarParaMapa();
            yield break;
        }



        aliadosDisponiveis = new List<GameObject>(aliados);
        indexAtual = 0;
        selecionandoAliado = true;
        cursor.SetActive(true);
        AtualizarCursor();
    }



}
