using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BasicCamera;
using BasicAudio;

[RequireComponent(typeof(AudioManager))]
[RequireComponent(typeof(CameraShake))]
public class GM : MonoBehaviour
{
    public static GM instance { get; private set; }

    public static int MENU_SCENE { get; private set; } = 0;
    public static int CONTROLS_SCENE { get; private set; } = 1;
    public static int CREDITS_SCENE { get; private set; } = 2;
    public static int INIT_GAME_SCENE { get; private set; } = 3;

    public Image panel;
    public AudioSource levelBgMusic;

    public CameraShake cameraShake { get; private set; }
    public AudioManager audioManager { get; private set; }
    public bool paused { get; private set; }

    public Player player { get; private set; }
    void Awake(){
        instance = this;
        audioManager = GetComponent<AudioManager>();
        cameraShake = GetComponent<CameraShake>();
        player = FindObjectOfType<Player>();
    }

    private void Start() {
        TogglePanel(false);
    }
    void OnDestroy() {
        instance = null;    
    }

    public void GoToNextLevel(){
        var levelSceneNum = SceneManager.GetActiveScene().buildIndex + 1;
        if(levelSceneNum < SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(levelSceneNum, LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene(CREDITS_SCENE, LoadSceneMode.Single);
        }
    }

    IEnumerator restartLevel(){
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    
    public void RestartLevel(){
        audioManager.PlaySound("ChannelChange");
        StartCoroutine(restartLevel());
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

    public void TogglePanel(bool value){
        if(panel != null)
            panel.enabled = value;
    }

}
