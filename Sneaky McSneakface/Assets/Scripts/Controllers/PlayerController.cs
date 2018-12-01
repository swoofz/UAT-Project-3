using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    public GameObject pauseMenu;
    public float volume;

    private float playerSpeed;
    private bool isWalking;
    private float sprintVolume;

	// Use this for initialization
	void Start () {
        playerSpeed = speed;
        sprintVolume = volume;
        GameManger.instance.startLocactions.Add(transform.position);
        GameManger.instance.allController.Add(this);
    }

    // Update is called once per frame
    void Update () {
        Movement();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame(pauseMenu);
        }
	}

    void Movement() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (!isWalking) {
                isWalking = true;
                speed = pawn.Walk(speed);
                volume = sprintVolume / 2;
            }
        } else {
            if (isWalking) {
                isWalking = false;
                speed = playerSpeed;
                volume = sprintVolume;
            }

        }

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
