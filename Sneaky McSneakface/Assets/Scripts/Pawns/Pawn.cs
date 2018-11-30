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
}
