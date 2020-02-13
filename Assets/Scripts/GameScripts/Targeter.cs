﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ray casts out of camera and determines what is being targeted
public class Targeter : Singleton<Targeter> {
    private GameObject m_target = null;
    public GameObject Target { get { return m_target; } }
    public Vector3 CollisionPoint { get; private set; }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity)) {
            m_target = hitInfo.collider.gameObject;
            CollisionPoint = hitInfo.point;
        }
    }

    public void LookAtTarget() {
        transform.LookAt(CollisionPoint);
        Vector3 rotationEuler = transform.rotation.eulerAngles;
        rotationEuler.x = 0;
        rotationEuler.z = 0;
        transform.rotation = Quaternion.Euler(rotationEuler);
    }
}
