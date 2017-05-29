using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour {

    public float speed = 100.0F;
    public float LRSpeed = 100.0F;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxis("Vertical") * speed;
        float leftRight = Input.GetAxis("Horizontal") * LRSpeed;
        translation *= Time.deltaTime;
        leftRight *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Translate(leftRight, 0, 0);

    }
}
