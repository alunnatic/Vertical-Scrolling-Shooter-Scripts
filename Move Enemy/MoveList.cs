using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveList {

    public Move direction;
    public float speed;
    public float verticalDistance;
    public float horizontalDistance;

    public Transform border;
    public float screenWidth;
    public float screenHeight;
    public float leftSide;
    public float rightSide;
    public float bottom;
    public float top;

    public MoveList(Move dir, float sp, float vD, float hD)
    {
        direction = dir;
        speed = sp;
        verticalDistance = vD;
        horizontalDistance = hD;
    }
}
