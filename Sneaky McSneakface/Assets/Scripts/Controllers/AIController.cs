using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller {

    public float roamDistance = 3;
    public float fov = 45;

    public float aiSenseRadius;
    private string aiState;
    
    private float maxX, maxY, minX, minY, countDown, startPoint;
    private bool canHear, canSee;
    private PlayerController target;

	// Use this for initialization
	void Start () {
        maxX = transform.position.x + roamDistance;
        maxY = transform.position.y + roamDistance;
        minX = transform.position.x - roamDistance;
        minY = transform.position.y - roamDistance;
        aiState = "idle";
        startPoint = 5;
        countDown = startPoint;
    }
	
	// Update is called once per frame
	void Update () {
        //countDown -= Time.deltaTime;
        //if (countDown < 0) {
        //    int randomNum = Random.Range(0, 5);

        //    MoveInPlace(randomNum);
        //    countDown = startPoint;
        //}

        //if (canHear) {
        //    pawn.DisplayMessage("What was that");
        //}
        //if (canSee) {
        //    pawn.DisplayMessage("I can see you");
        //}
        Debug.Log(aiState);

        if (aiState == "idle") {
            //Do Action
            pawn.DoIdle();

            //Check for transitions
            if (canSee) {
                aiState = pawn.ChangeState("seek");
            } else if (canHear) {
                aiState = pawn.ChangeState("turn towards");
            }
        } else if (aiState == "seek") {
            // Do Action
            pawn.DoSeek(target.gameObject, speed);

            //Check for transitions
            if (!canSee) {
                aiState = pawn.ChangeState("idle");
            } else if (canHear) {
                aiState = pawn.ChangeState("turn towards");
            }
        } else if (aiState == "turn towards") {
            pawn.LookFor(target.gameObject);

            //Check for transitions
            if (canSee) {
                aiState = pawn.ChangeState("seek");
            } else if (!canSee) {
                aiState = pawn.ChangeState("idle");
            }
        }
	}



    void MoveInPlace(int num) {
        if (transform.position.x < maxX && num == 1) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (transform.position.x > minX && num == 2) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (transform.position.y < maxY && num == 3) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (transform.position.y > minY && num == 4) {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.transform.parent.tag == "Player") {
            target = collision.gameObject.transform.parent.GetComponent<PlayerController>();

            if (target != null) {
                canHear = pawn.CanHear(collision.gameObject, target.volume);
                canSee = pawn.CanSee(collision.gameObject, fov);
            } else {
                Debug.LogWarning("No PlayerController on an object with a player tag");
            }
        }
    }
}
