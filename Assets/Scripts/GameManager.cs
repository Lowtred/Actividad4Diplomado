using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{
    public GameObject menuPusa;
    public GameObject interfaz;

    public AudioClip[] audios;
    public AudioSource audioSource;

    public AudioMixer masterMixer;
    public Slider sliderMaster;
    public Slider sliderMusica;
    public Slider sliderSonido;

    public void pausa(){
        audioSource.PlayOneShot(audios[0]);
        menuPusa.SetActive(true);
        interfaz.SetActive(false);
        Time.timeScale = 0;
    }

    public void Continuar()
    {
        audioSource.PlayOneShot(audios[0]);
        menuPusa.SetActive(false);
        interfaz.SetActive(true);
        Time.timeScale = 1;
    }

    private void Update()
    {
        masterMixer.SetFloat("VolMaster", sliderMaster.value);
        masterMixer.SetFloat("VolMusica", sliderMusica.value);
        masterMixer.SetFloat("VolSonido", sliderSonido.value);
    }
}
