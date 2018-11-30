using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    public GameObject pauseMenu;
    public float volume;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame(pauseMenu);
        }
	}

    void Movement() {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += pawn.MoveY(speed);
        }
        else if (Input.GetKey(KeyCode.S)) {
            transform.position += pawn.MoveY(-speed);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.position += pawn.MoveX(speed);
        }
        else if (Input.GetKey(KeyCode.A)) {
            transform.position += pawn.MoveX(-speed);
        }
    }

    void PauseGame(GameObject pause) {
        GameManger.instance.TogglePauseMenu(pause);
    }
}
