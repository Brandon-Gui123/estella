﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissureScript : MonoBehaviour {
    public float riseTime = 2;

    const float TOTAL_HEIGHT = 2;
    Vector3 direction;
    float internalCounter = 0;
    // Start is called before the first frame update
    void Start() {
        Vector3 properPosition = transform.position + transform.up * TOTAL_HEIGHT;
        direction = (properPosition - transform.position) / riseTime;

    }

    // Update is called once per frame
    void Update() {
        if (internalCounter <= riseTime) {
            transform.position += direction * Time.deltaTime;
            internalCounter += Time.deltaTime;
        }
    }
}
