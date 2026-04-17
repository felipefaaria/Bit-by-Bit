using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popUpVitoria;

    public void MostrarVitoria()
    {
        if (popUpVitoria != null)
        {
            popUpVitoria.SetActive(true);
        }
        else
        {
        }
    }
}
