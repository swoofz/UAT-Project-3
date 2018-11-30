using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour {

    public virtual Vector3 MoveY(float speed) {
        Vector3 moveY = Vector3.up * speed * Time.deltaTime;
        return moveY;
    }

    public virtual Vector3 MoveX(float speed) {
        Vector3 moveX = Vector3.right * speed * Time.deltaTime;
        return moveX;
    }

    public virtual float Walk(float speed) {
        return speed / 2;
    }

    public virtual int Melee(int damage) {
        // TODO:: Set attacking animation
        Debug.Log("I am attacking from the parent component");
        return damage;
    }

    public virtual void Shoot(GameObject bullet, Transform location) {
        Instantiate(bullet, location.position, transform.rotation);
    }

    public virtual bool CanSee(GameObject target, float fieldOfView) {
        Vector3 targetPos = target.transform.position;

        Vector3 agentToTargetVector = targetPos - transform.position;

        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.up);
        //Debug.Log(angleToTarget);

        if (angleToTarget <= fieldOfView) {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, agentToTargetVector);

            if (hitInfo.collider.gameObject == target) {
                return true;
            }
        }

        return false;
    }

    public virtual bool CanHear(GameObject target, float volume) {
        Transform TargetNM = target.GetComponent<Transform>();  // find noiseMaker

        if (TargetNM != null) {
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

    public virtual void DoIdle() {
        // Do Nothing
    }

    public virtual void DoSeek(GameObject target, float speed) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.position += vectorToTarget.normalized * speed * Time.deltaTime;
    }

    public virtual void LookFor(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        transform.up = vectorToTarget;
    }

    public virtual string ChangeState(string newState) {
        // Change our state
        return newState;
    }
}
