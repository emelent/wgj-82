using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevel { get; private set; } = 0;

    public static GameManager instance { get; private set; }

    void Awake(){
        instance = this;
    }

    void OnDestroy() {
        instance = null;    
    }

    void GoToNextLevel(){
        currentLevel ++;
        var levelSceneNum = Menu.INIT_GAME_SCENE + currentLevel;
        if(levelSceneNum < SceneManager.sceneCount){
            SceneManager.LoadScene(levelSceneNum, LoadSceneMode.Single);
        }
    }

}
