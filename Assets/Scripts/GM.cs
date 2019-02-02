﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BasicCamera;
using BasicAudio;
using BasicDialog;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(DialogManager))]
[RequireComponent(typeof(CameraShake))]
public class GM : MonoBehaviour
{
    public static GM instance { get; private set; }

    public static int MENU_SCENE { get; private set; } = 0;
    public static int CONTROLS_SCENE { get; private set; } = 1;
    public static int INIT_GAME_SCENE { get; private set; } = 2;

    public AudioSource levelBgMusic;


    public int currentLevel { get; private set; } = 0;
    public CameraShake cameraShake { get; private set; }
    public AudioManager audioManager { get; private set; }

    public DialogManager dialogManager { get; private set; }
    public bool paused { get; private set; }

    void Awake(){
        instance = this;
        audioManager = GetComponent<AudioManager>();
        cameraShake = GetComponent<CameraShake>();
        dialogManager = GetComponent<DialogManager>();
    }

    void OnDestroy() {
        instance = null;    
    }

    void GoToNextLevel(){
        currentLevel ++;
        var levelSceneNum = INIT_GAME_SCENE + currentLevel;
        if(levelSceneNum < SceneManager.sceneCount){
            SceneManager.LoadScene(levelSceneNum, LoadSceneMode.Single);
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(INIT_GAME_SCENE, LoadSceneMode.Single);
    }

    public void GoToMenu(){
        SceneManager.LoadScene(MENU_SCENE, LoadSceneMode.Single);
    }

    public void GoToControls(){
        SceneManager.LoadScene(CONTROLS_SCENE, LoadSceneMode.Single);
    }

    public void ExitGame(){
        Application.Quit();
    }


    public void TogglePause(){
        paused = !paused;
        if(paused){
            print("Game Paused");
            // pause music
            audioManager.Pause();
            levelBgMusic.Pause();
            print("bg music isPlaying => " + levelBgMusic.isPlaying);
            // pause time
        } else {
            print("Game Unpaused");
            // unpause music
            audioManager.Play();
            levelBgMusic.Play();
            print("bg music isPlaying => " + levelBgMusic.isPlaying);
            // unpause time
        }
    }

}
