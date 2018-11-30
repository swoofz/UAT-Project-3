using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActions : MonoBehaviour {

    public void StartGame() {
        Reset();                // makes sure to start the game in a defualt state
    }

    public void ChangeActive() {
        gameObject.SetActive(!gameObject.activeSelf);   // Change active state
    }

    public void QuitGame() {
        Application.Quit();     // Close the application
    }

    public void ChangeMenus(GameObject menu) {
        gameObject.SetActive(false);            // deactive menu
        menu.SetActive(true);                   // active menu
        GameManger.instance.gameIsRuning = false;
    }

    public void Reset() {
        GameManger.instance.Reset();    // reset the game
        ChangeActive();                 // deactive menu
        GameManger.instance.gameIsRuning = true;
    }
}
