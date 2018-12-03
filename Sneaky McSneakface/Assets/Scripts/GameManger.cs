using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour {

    public static GameManger instance;          // Create a singleton
    public GameObject startMenu;                // Create a variable to hold the Start menu
    public bool gameIsRuning;                   // Create a variable to know when the game is running
    public GameObject winScreen, LoseScreen;    // Create variable to hold menu screens

    public Controller playerTarget, target;                     // Create variables to store controller information
    public static bool canHear, canSee;                         // Create variables to add to the Finite State Machine logic
    public List<Controller> AIList = new List<Controller>();    // Create a List to of all AIs in the scene
    public List<Vector3> startLocactions;                       // Create a List to store the location of all controller components
    public List<Controller> allController;                      // Create a List of all Controller components in the scene
    public Slider playerHealth;                                 // Create a variable to hold a slider
    public bool win, lose;                                      // Create variables to check state of the game

    private string aiState;         // Create a variable to store an AI state
    private float countDown;        // Create a variable to make a timer count down
    private float startPoint;       // Create a variable to hold the value of the count down start point
    private int playerHp;           // Create a variable to hold the start value of the player's health

    private void Awake() {
        if (instance != null) {                                                     // If there is already a gameManger in the scene
            Destroy(gameObject);                                                    // Destory this one
            Debug.LogWarning("A second game manager was detected and destoryed.");  // Let the design know that their was an extra gameManger
        } else {                                                                    // Otherwise
            instance = this;                                                        // Make this the gameManger
            DontDestroyOnLoad(gameObject);                                          // Don't destory when going to a new scene
        }
    }

    // Use this for initialization
    void Start () {
        startMenu.SetActive(true);          // Start Menu to active
        Time.timeScale = 0;                 // Freeze motion
        startPoint = 3;                     // Set a count down start point
        playerHp = (int)playerHealth.value; // Get the player hp 
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) {                                                   // if their is no target
            for (int i = 0; i < AIList.Count; i++) {                            // find all AI
                countDown = AIList[i].pawn.DoIdle(AIList[i].speed, countDown);  // change look direction after count down
                if (countDown < 0) {                                            // if countdown is less than 0
                    countDown = startPoint;                                     // reset countdown
                }
            }
        }

        if (target != null) {                                       // if their is a target
            if (target.GetComponent<AIController>() != null) {      // make sure the target has an AI component
                FSM();                                              // run Finite State Mahcine
            }
        }

        if (playerHealth.value <= 0 && gameIsRuning) {      // if the player's health reaches 0 and game is running
            lose = true;                                    // Game is over and player lost
        }

        if (win || lose) {          // if lose or win condition is meet
            Time.timeScale = 0;     // freeze motion
            gameIsRuning = false;   // game is not runnign

            if (win) {                      // if win
                winScreen.SetActive(true);  // show win screen
                win = false;                // Reset win to false
            } else {                        // otherwise if lose
                LoseScreen.SetActive(true); // show lose screen
                lose = false;               // Reset lose to false

            }
        }

    }

    // Finite State Machine
    public void FSM() {
        if (aiState == "idle") {        // Is the AI state is idle
            // Do Action
            // Check for transitions

            if (canSee) {                                           // If AI can see the target
                aiState = target.pawn.ChangeState("seek");          // Change AI state to seek
            } else if (canHear) {                                   // If AI can hear the target
                aiState = target.pawn.ChangeState("turn towards");  // Change AI state to turn towards
            }

            target = null;      // If no change happened set target to null

        } else if (aiState == "seek") {                                 // If AI state is seek
            // Do Action
            target.pawn.DoSeek(playerTarget.gameObject, target.speed);  // Move toward the target that can see
            target.pawn.LookFor(playerTarget.gameObject);               // Rotate to face the target

            //Check for transitions
            if (!canSee) {                                          // If the AI can't see a target
                aiState = target.pawn.ChangeState("idle");          // Change AI state to idle
            } else if (canHear) {                                   // If the AI can hear a target
                aiState = target.pawn.ChangeState("turn towards");  // Change AI state to turn towards
            }
        } else if (aiState == "turn towards") {             // If AI stat is turn towards
            target.pawn.LookFor(playerTarget.gameObject);   // Rotate in the direction of the target

            //Check for transitions
            if (canSee) {                                   // If AI can see the target
                aiState = target.pawn.ChangeState("seek");  // Change AI state to seek
            } else if (!canSee) {                           // If AI can't see the target
                aiState = target.pawn.ChangeState("idle");  // Change AI state to idle
            }
        }
    }
    
    // Menu Controls Below
    public void PlayerGame(GameObject menu) {
        ResetGame();                            // Make sure game is at a default state
        menu.SetActive(false);                  // Deactive the menu that is being shown
        gameIsRuning = true;                    // Let game input be used
        Time.timeScale = 1;                     // Let objects move
        aiState = "idle";                       // Set AI state equal to idle on game start
    }

    public void ResetGame() {
        for (int i = 0; i < allController.Count; i++) {                 // Find all controller component
            allController[i].transform.position = startLocactions[i];   // Reset their position to starting locations
        }

        playerHealth.value = playerHp;  // Reset the player hp
    }

    public void TogglePauseMenu(GameObject pause) {
        if (gameIsRuning) {                             // if the game is running
            if (pause.activeSelf) {                     // if we are paused
                pause.SetActive(false);                 // unpaused
                Time.timeScale = 1;                     // add motion to normal
            } else {                                    // Otherwise
                pause.SetActive(true);                  // pause
                Time.timeScale = 0;                     // freeze motion
            }
        }
    }

    public void GoToStartMenu(GameObject menu) {
        menu.SetActive(false);                      // Deactive menu
        startMenu.SetActive(true);                  // Active Start menu
        gameIsRuning = false;                       // game is no longer running
    }

    public void QuitGame() {
        // Quit the Application
        Application.Quit();
    }
}
