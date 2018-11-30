using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour {

    public static GameManger instance;
    public GameObject startMenu;
    public bool gameIsRuning;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            Debug.LogWarning("A second game manager was detected and destoryed.");
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        startMenu.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reset() {
        // TODO:: reset the game stats to starting
        Debug.Log("reset game");
    }

    public void TogglePauseMenu(GameObject pause) {
        if (gameIsRuning) {
            if (pause.activeSelf) {
                pause.SetActive(false);
                Time.timeScale = 1;
            } else {
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
