using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Pawn {

    public override float DoIdle(float speed, float num) {
        float countDown = CountDown(num);
        if (countDown < 0) {
            LookAround(speed, Random.Range(1, 5));
        }
        return countDown;
    }

    public float CountDown(float num) {
        float countDown = num;
        countDown -= Time.deltaTime;
        return countDown;
    }

    public void LookAround(float speed, int num) {
        if (num == 1) {
            MoveX(speed);
        }
        if (num == 2) {
            MoveX(-speed);
        }
        if (num == 3) {
            MoveY(speed);
        }
        if (num == 4) {
            MoveY(-speed);
        }
    }
}
