using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {

    public virtual Vector3 MoveY(float speed) {
        int timesBy = -1;
        if (speed > 0) { timesBy = 1; }
        Vector3 moveY = Vector3.up * speed * Time.deltaTime;
        Quaternion target = Quaternion.Euler(0, 0, 90 * timesBy);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 100f);
        return moveY;
    }

    public virtual Vector3 MoveX(float speed) {
        int timesBy = -1;
        if (speed > 0) { timesBy = 0; }
        Vector3 moveX = Vector3.right * speed * Time.deltaTime;
        Quaternion target = Quaternion.Euler(0, 0, 180 * timesBy);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 100f);
        return moveX;
    }

    public virtual float Walk(float speed) {
        return speed / 2;
    }

    public virtual int Attack(int damage) {
        // TODO:: Set attacking animation
        Debug.Log("I am attacking from the parent component");
        return damage;
    }

    public virtual bool CanSee(GameObject target, float fieldOfView) {
        Vector3 targetPos = target.transform.position;

        Vector3 agentToTargetVector = targetPos - transform.position;

        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.right);

        if (angleToTarget <= fieldOfView) {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToTargetVector);

            if (hitInfo.collider.gameObject == target) {
                return true;
            }
        }

        return false;
    }

    public virtual bool CanHear(GameObject target, float volume) {
        if (target != null) {
            volume -= Vector3.Distance(target.transform.position, transform.position);

            if (volume > 0) {
                return true;
            }
        }
        return false;
    }

    public virtual void DisplayMessage(string message) {
        Debug.Log(message);
    }

    public virtual float DoIdle(float aiSpeed=0, float aiNum=0) {
        // Do Nothing
        return 0;
    }

    public virtual void DoSeek(GameObject target, float speed) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.parent.position += vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public virtual void LookFor(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.right = vectorToTarget;
    }

    public virtual string ChangeState(string newState) {
        // Change our state
        return newState;
    }
}
