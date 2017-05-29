using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

    public List<MoveList> order;
    public bool moving;
    public float speed;
    int index;
    public float startTime;
    public Vector3 rotation;
    public int timesToRotate;
    public int rotationCount;
    public bool rotationCountFlag;


    // Use this for initialization
    void Start() {
        index = 0;
        moving = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (moving)
        {
            CallMoveEnemy();
        }
    }

    public void CallMoveEnemy()
    {
        speed = order[index].speed;                 //set speed
        if (index >= order.Count) { Destroy(gameObject); }      //destroy enemy if at end of movement list

        // sets the number of times to rotate and then increments index
        if (order[index].direction == Move.RotationCounter)
        {
            rotation = transform.forward;
            timesToRotate = (int)order[index].speed;
            rotationCount = 0;
            index++;
        }
        else
        {
            switch (order[index].direction)
            {
                case Move.Down:
                    moveDown();
                    break;

                case Move.Up:
                    moveUp();
                    break;

                case Move.Left:
                    moveLeft();
                    break;

                case Move.Right:
                    moveRight();
                    break;

                case Move.Clockwise:
                    moveCircleClockwise();
                    break;

                case Move.CounterClockwise:
                    moveCircleCounterclockwise();
                    break;

                case Move.Done:
                    doneMoving();
                    break;

                case Move.Transition:
                    moveTransition();
                    break;

                default:
                    doneMoving();
                    break;
            }
        }
        if (index >= order.Count) { Destroy(gameObject); }
    }

    public void moveDown()
    {
        if (transform.position.x >= 3)
        {
            transform.forward = new Vector3(-90, 0, 0);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else { index++; }
    }

    void moveUp()
    {
        if (transform.position.x <= 17)
        {
            transform.forward = new Vector3(90, 0, 0);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else { index++; }
    }

    void moveLeft()
    {
        if (transform.position.z <= 5)
        {
            transform.forward = new Vector3(0, 0, 90);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else { index++; }
    }

    void moveRight()
    {
        if (transform.position.z >= -5.6f)
        {
            transform.forward = new Vector3(0, 0, -90);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else { index++; }
    }

    // Note that this method rotates number of revolutions until pointing in the same direction, NOT back to the start point
    // vert variable is only there so it can be used to track rotations (when the value is 1 it has rotated once)
    void moveCircleClockwise()
    {
        if (rotationCount < timesToRotate)
        {
            float rot = Time.time - startTime;          // Using Time.time-startTime because deltatime didn't seem to work
            float vert = Mathf.Sin(rot * speed);
            transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
            transform.position += transform.forward * Time.deltaTime * speed;

            if (vert > .95 && !rotationCountFlag)
            {
                rotationCount++;
                rotationCountFlag = true;
            }
            if (vert < .1 && rotationCountFlag) { rotationCountFlag = false; }
        }
        else { index++; }
    }

    void moveCircleCounterclockwise()
    {
        if (rotationCount < timesToRotate)
        {
            float rot = Time.time - startTime;
            float vert = Mathf.Sin(rot * speed);
            transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
            transform.rotation = Quaternion.Inverse(transform.rotation);
            transform.position += transform.forward * Time.deltaTime * speed;

            if (vert > .95 && !rotationCountFlag)
            {
                rotationCount++;
                rotationCountFlag = true;
            }
            if (vert < .1 && rotationCountFlag) { rotationCountFlag = false; }
        }
        else { index++; }
    }

    void moveTransition()
    {
        float rot = Time.time - startTime;
        float vert = Mathf.Sin(rot * speed);

        switch (order[index + 1].direction)
        {
            case Move.Down:
                if (transform.rotation.eulerAngles.y > 85f) { transClock(); }else { transCounterClock(rot); }
                if (transform.rotation.eulerAngles.y >= 265f && transform.rotation.eulerAngles.y <= 275f) { index++;  }                                                       
                break;
            case Move.Up:
                if (transform.rotation.eulerAngles.y > 175f) { transClock(); } else { transCounterClock(rot); }
                if (transform.rotation.eulerAngles.y >= 85f && transform.rotation.eulerAngles.y < 95f) { index++; }
                break;
            case Move.Left:
                if (transform.rotation.eulerAngles.y > 175f) { transClock(); } else { transCounterClock(rot); }
                if (transform.rotation.eulerAngles.y >= 355f || transform.rotation.eulerAngles.y <= 5f) { index++; }
                break;
            case Move.Right:
                if (transform.rotation.eulerAngles.y > 355f || transform.rotation.eulerAngles.y <180) { transClock(); } else { transCounterClock(rot); }
                if (transform.rotation.eulerAngles.y >= 175f && transform.rotation.eulerAngles.y <= 185f) { index++; }
                break;
            case Move.Clockwise:

                if (vert >= .25)
                {
                    transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
                    transform.position += transform.forward * Time.deltaTime * speed;
                }
                else { index++; }
                break;

            case Move.CounterClockwise:
                if (vert >= .25)
                {
                    transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
                    transform.rotation = Quaternion.Inverse(transform.rotation);
                    transform.position += transform.forward * Time.deltaTime * speed;
                }
                else { index++; }
                break;

            default:
                index++;
                break;
        }
    //    transform.Rotate(Vector3.up * rot);
      //  transform.position += transform.forward * Time.deltaTime * speed;
    }

    // float tT
    void transClock()
    {
        float rot = Time.time - startTime;          // Using Time.time-startTime because deltatime didn't seem to work
        float vert = Mathf.Sin(rot * speed);
        transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
        transform.position += transform.forward * Time.deltaTime * speed;
        // transform.Rotate(Vector3.up * tT);
        //transform.position += transform.forward * Time.deltaTime * speed;
    }

    void transCounterClock(float tT)
    {
        float rot = Time.time - startTime;
        float vert = Mathf.Sin(rot * speed);
        transform.forward = new Vector3(vert, 0, Mathf.Cos(rot * speed));
        transform.rotation = Quaternion.Inverse(transform.rotation);
        transform.position += transform.forward * Time.deltaTime * speed;
        // transform.Rotate(Vector3.up * tT);
        // transform.rotation = Quaternion.Inverse(transform.rotation);
        //  transform.position += transform.forward * Time.deltaTime * speed;
    }

void doneMoving()
        {
            Destroy(gameObject);
        }
        
}
