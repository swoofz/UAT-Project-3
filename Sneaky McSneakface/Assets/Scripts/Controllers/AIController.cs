using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {

    public float fov = 45;          // Create a variable for the AI field of view
    
    private PlayerController target;    // Create a variable to get the target

	// Use this for initialization
	void Start () {
        GameManger.instance.AIList.Add(this);                           // Add this component to a GameManger list
        GameManger.instance.startLocactions.Add(transform.position);    // Add this transform position component to a GameManger list
        GameManger.instance.allController.Add(this);                    // Add this component to a GameManger list
    }

    // Update is called once per frame
    void Update () {
        // TODO:: Add message to the game world canvas element when can see or can hear the target 
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.transform.parent != null) {
            if (collision.gameObject.transform.parent.tag == "Player") {
                target = collision.gameObject.transform.parent.GetComponent<PlayerController>();
                GameManger.instance.playerTarget = target;
                GameManger.instance.target = this;

                if (target != null) {
                    GameManger.canHear = pawn.CanHear(collision.gameObject, target.volume);
                    GameManger.canSee = pawn.CanSee(collision.gameObject, fov);
                } else {
                    Debug.LogWarning("No PlayerController on an object with a player tag");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.transform.parent != null) {
            if (collision.gameObject.transform.parent.tag == "Player") {
                GameManger.canSee = false;
            }
        }
    }
}
