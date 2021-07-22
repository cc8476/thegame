﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioMixer AudioMixer;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    //public void UIEnable()
    //{
    //    GameObject.Find().SetActive(true);
    //}
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;//暂停
    }
    public void BackGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void SetVolume(float Value)
    {
        AudioMixer.SetFloat("MainVolume", Value);
    }
}
