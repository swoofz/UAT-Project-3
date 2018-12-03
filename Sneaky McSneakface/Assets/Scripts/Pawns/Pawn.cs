using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {

    public virtual Vector3 MoveY(float speed) {
        int timesBy = -1;                                                                   // Create a variable to change value to negative
        if (speed > 0) { timesBy = 1; }                                                     // if speed is greater than 0 change variable to positive
        Vector3 moveY = Vector3.up * speed * Time.deltaTime;                                // store a transform for movement
        Quaternion target = Quaternion.Euler(0, 0, 90 * timesBy);                           // Rotate up or down
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 100f);    // Rotate
        return moveY;                                                                       // Return the updated transform
    }

    public virtual Vector3 MoveX(float speed) {
        int timesBy = -1;                                                                   // Create a variable to change value to negative
        if (speed > 0) { timesBy = 0; }                                                     // if speed is greater than 0 change variable to 0
        Vector3 moveX = Vector3.right * speed * Time.deltaTime;                             // store a transform for movement
        Quaternion target = Quaternion.Euler(0, 0, 180 * timesBy);                          // Rotate right or left
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 100f);    // Rotate
        return moveX;                                                                       // Return the updated transform
    }

    public virtual float Walk(float speed) {
        return speed / 2;                       // return speed divided by 2
    }

    public virtual int Attack(int damage) {
        // TODO:: Set attacking animation                       // Set up animation if have animations
        Debug.Log("I am attacking from the parent component");  // Defualt if no override
        return damage;                                          // Return damage
    }

    public virtual bool CanSee(GameObject target, float fieldOfView) {
        Vector3 targetPos = target.transform.position;                                  // set the target position into a variable for easy access
        Vector3 agentToTargetVector = targetPos - transform.position;                   // find the direction the target is in
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.right);      // get the angle from the looking point to the target position

        if (angleToTarget <= fieldOfView) {                                                     // if the angle to the target is less than or equal to the field of view
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToTargetVector);  // Make sure that your target is the first thing you detect

            if (hitInfo.collider.gameObject == target) {    // if collide with target
                return true;                                // return true
            }
        }

        return false;   // return false
    }

    public virtual bool CanHear(GameObject target, float volume) {
        if (target != null) {                                                           // if there is a target
            volume -= Vector3.Distance(target.transform.position, transform.position);  // lower the volume basic on the distance it coming from

            if (volume > 0) {   // if volume equals 0
                return true;    // return true
            }
        }
        return false;       // Else return false
    }

    public virtual void DisplayMessage(string message) {
        // TODO:: display a message to in the game world
        Debug.Log(message);
    }

    public virtual float DoIdle(float aiSpeed=0, float aiNum=0) {
        // Do Nothing
        return 0;
    }

    public virtual void DoSeek(GameObject target, float speed) {
        Vector3 vectorToTarget = target.transform.position - transform.position;            // Find the direction to go
        transform.parent.position += vectorToTarget.normalized * speed * Time.deltaTime;    // Move to target
    }

    public virtual void LookFor(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;    // Find location to look
        transform.right = vectorToTarget;                                           // Set direction to look
    }

    public virtual string ChangeState(string newState) {
        // Change our state
        return newState;
    }
}
