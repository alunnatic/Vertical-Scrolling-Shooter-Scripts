using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Move { Down, Up, Left, Right, RotationCounter, Clockwise, CounterClockwise, Transition, Done }

public class CallMoves : MonoBehaviour {

    public List<MoveList> moveList = new List<MoveList>();
    public Transform enemy1;
    public Transform border;
    public float screenWidth;
    public float screenHeight;
    public float leftSide;
    public float rightSide;
    public float bottom;
    public float top;

    // Use this for initialization
    void Start()
    {
        setBorder();
    }

    /* direction, speed, vertical distance to move (using vert method), horizontal distance (using horiz method) to move
     * When using rotationCounter the 2nd argument to MoveList needs to be the number of rotations to perform (int only)
     *  and the 3rd argument needs to be radius
     */
    public void Attack1()
    {
        moveList.Add(new MoveList(Move.RotationCounter, 2, 0, 0));
        moveList.Add(new MoveList(Move.Clockwise, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Down, 5f, Vert(.9f), 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Up, 5f, Vert(.1f), 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0)); 
        moveList.Add(new MoveList(Move.Left, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Right, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0));
        moveList.Add(new MoveList(Move.RotationCounter, 3, 0, 0));
        moveList.Add(new MoveList(Move.Clockwise, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Transition, 5f, 0, 0));
        moveList.Add(new MoveList(Move.RotationCounter, 3, 0, 0));
        moveList.Add(new MoveList(Move.CounterClockwise, 5f, 0, 0));
        moveList.Add(new MoveList(Move.Done, 0, 0, 0));

        var enemyMove = Instantiate(enemy1);
        enemyMove.position = new Vector3(Vert(.1f), .3f, Horiz(.9f));
        enemyMove.GetComponent<MoveEnemy>().order = moveList;
    }
    
    // uses the border object to figure out the boundaries
    void setBorder()
    {
        leftSide = border.position.z;
        rightSide = border.GetComponentInChildren<Border_Right>().transform.position.z;
        bottom = border.GetComponentInChildren<Border_Bottom>().transform.position.x;
        top = border.GetComponentInChildren<Border_Top>().transform.position.x;
        screenWidth = leftSide - rightSide;
        screenHeight = top - bottom;
    }

    // When given a decimal percentage it will return the horizontal coordinate for use in a Vector3
    float Horiz(float percentFromLeft)
    {
        float changeByAmount = percentFromLeft * screenWidth;
        float horiz = leftSide - changeByAmount;
        return horiz;
    }

    // When given a decimal percentage it will return the vertical coordinate for use in a Vector3
    float Vert(float percentFromTop)
    {
        float changeByAmount = percentFromTop * screenHeight;
        float vert = top - changeByAmount;
        return vert;
    }
    
}
