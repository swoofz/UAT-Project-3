using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour {

    public static GameManger instance;
    public GameObject startMenu;
    public bool gameIsRuning;

    public Controller playerTarget, target;
    public static bool canHear, canSee;
    public List<Controller> AIList = new List<Controller>();
    public List<Vector3> startLocactions;
    public List<Controller> allController;

    private string aiState;
    private float countDown;
    private float startPoint;

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
        Time.timeScale = 0;
        startPoint = 3;
        aiState = "idle";
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) {
            for (int i = 0; i < AIList.Count; i++) {
                countDown = AIList[i].pawn.DoIdle(AIList[i].speed, countDown);
                if (countDown < 0) {
                    countDown = startPoint;
                }
            }
        }

        if (target != null) {
            if (target.GetComponent<AIController>() != null) {
                FSM();
            }
        }
    }

    public void Reset() {
        for (int i = 0; i < allController.Count; i++) {
            allController[i].transform.position = startLocactions[i];
        }
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

    public void FSM() {
        if (aiState == "idle") {
            // Do Action
            // Check for transitions

            if (canSee) {
                aiState = target.pawn.ChangeState("seek");
            } else if (canHear) {
                aiState = target.pawn.ChangeState("turn towards");
            }

            target = null;
        } else if (aiState == "seek") {
            // Do Action
            target.pawn.DoSeek(playerTarget.gameObject, target.speed);
            target.pawn.LookFor(playerTarget.gameObject);

            //Check for transitions
            if (!canSee) {
                aiState = target.pawn.ChangeState("idle");
            } else if (canHear) {
                aiState = target.pawn.ChangeState("turn towards");
            }
        } else if (aiState == "turn towards") {
            target.pawn.LookFor(playerTarget.gameObject);

            //Check for transitions
            if (canSee) {
                aiState = target.pawn.ChangeState("seek");
            } else if (!canSee) {
                aiState = target.pawn.ChangeState("idle");
            }
        }
    }
}
