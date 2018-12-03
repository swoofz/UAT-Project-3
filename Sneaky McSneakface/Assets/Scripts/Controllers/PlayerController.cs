using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    public GameObject pauseMenu;        // Create a variable to store the pause menu
    public float volume;                // Create a variable to control player noise making
    public string messageToYell;        // Create a variable to set up a message to display

    private float playerSpeed;          // Create a variable to control player speed
    private bool isWalking;             // Create a variable to control if player is walking
    private float sprintVolume;         // Create a variable to change volume noise
    private float timer, timerStart;    // Create variable for a timer
    private bool talking;               // Create a variable for player chat

	// Use this for initialization
	void Start () {
        GameManger.instance.startLocactions.Add(transform.position);    // Add this transform position component to a GameManger list
        GameManger.instance.allController.Add(this);                    // Add this component to a GameManger list
        GameManger.instance.playerHealth.value = health;                // Set the players health to the gameManger slider
        playerSpeed = speed;                                            // Get the intial speed
        sprintVolume = volume;                                          // Get the intial volume
        timerStart = 2;                                                 // Set timer start values
    }

    // Update is called once per frame
    void Update () {
        if (GameManger.instance.gameIsRuning) {                       // if the game is running
            health = ( int )GameManger.instance.playerHealth.value;   // Keep same value of health as in the GameManger
        }

        Movement();                                 // Run movement function
        if (Input.GetKeyDown(KeyCode.Escape)) {     // If you press the escape key 
            PauseGame(pauseMenu);                   // pause the game
        }

        if (Input.GetKeyDown(KeyCode.Space) && !talking) {      // If you press the space key
            pawn.Attack(dealDamage);                            // Attack
            talking = true;                                     // talking is true
            timer = timerStart;                                 // Set timer equal to the start
            pawn.DisplayMessage(messageToYell);                 // Send a message to display
        }

        if (talking) {                  // if talking
            timer -= Time.deltaTime;    // start timer count down
            if (timer < 0) {            // if timer hits 0
                talking = false;        // talking is false
                volume = sprintVolume;  // volume set back to start volume
                pawn.DisableMessage();  // Disable the message that got displayed
            }
        }
	}

    void Movement() {
        if (Input.GetKey(KeyCode.LeftShift)) {  // if press left shift
            if (!isWalking) {                   // if not walking
                isWalking = true;               // set walking to true
                speed = pawn.Walk(speed);       // change the speed
                volume = sprintVolume / 2;      // change the volume
            }
        } else {                            // if shift is not pressed 
            if (isWalking) {                // if walking is set
                isWalking = false;          // set walking to false
                speed = playerSpeed;        // set the speed back to intial value
                volume = sprintVolume;      // set the volume back to intial
            }

        }

        if (Input.GetKey(KeyCode.W)) {                  // If the W is pressed
            transform.position += pawn.MoveY(speed);    // Move player up
        }
        else if (Input.GetKey(KeyCode.S)) {             // If the S is pressed
            transform.position += pawn.MoveY(-speed);   // Move player down
        }
        else if (Input.GetKey(KeyCode.D)) {             // If the D is pressed
            transform.position += pawn.MoveX(speed);    // Move player right
        }
        else if (Input.GetKey(KeyCode.A)) {             // If the A is pressed
            transform.position += pawn.MoveX(-speed);   // Move player left
        }
    }

    void PauseGame(GameObject pause) {
        GameManger.instance.TogglePauseMenu(pause);   // Send pause menu to GameManger to pause the game
    }
}
