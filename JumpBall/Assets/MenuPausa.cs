using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject BotonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool IsMuted;

    public void Pausa()
    {
        Time.timeScale = 0f;
        BotonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        BotonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        Debug.Log("Cerrando juego");
        Application.Quit();
    }

    public void Mutear()
    {
        IsMuted = !IsMuted;
        AudioListener.pause = IsMuted;
    }
}
