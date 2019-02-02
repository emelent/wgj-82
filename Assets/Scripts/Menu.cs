using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public static int MENU_SCENE { get; private set; } = 0;
    public static int CONTROLS_SCENE { get; private set; } = 1;
    public static int INIT_GAME_SCENE { get; private set; } = 2;
    
    public void Play(){
        SceneManager.LoadScene(INIT_GAME_SCENE, LoadSceneMode.Single);
    }

    public void Controls(){
        SceneManager.LoadScene(CONTROLS_SCENE, LoadSceneMode.Single);
    }

    public void Exit(){
        Application.Quit();
    }
}
