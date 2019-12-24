﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float speed;
    public float maxHealth = 100;
    private float currentHealth = 100;
    public Animator animator;

    public KeyCode leftHandButton = KeyCode.Mouse0, rightHandButton = KeyCode.Mouse1;   //activating elements
    public KeyCode swapLeft = KeyCode.Q, swapRight = KeyCode.E; //swapping elements

    private GameObject pivot;
    Rigidbody rb;
    float rotation = 0;

    //Radial Menu
    GameObject radialMenu1;
    GameObject radialMenu2;
    public bool isInRadialMenu;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        pivot = GameObject.Find("camera-pivot");
        radialMenu1 = GameObject.Find("RadialMenu 1");
        radialMenu2 = GameObject.Find("RadialMenu 2");
        radialMenu1.SetActive(false);
        radialMenu2.SetActive(false);

        //init health
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        //RotationUpdate();
        MovementUpdate();
    }

    void RotationUpdate() {
        //when mouse pressed
        if (Input.GetKey(leftHandButton) || Input.GetKey(rightHandButton)) {
            //look at same direction as camera
            //rot of cam
            Quaternion camRot = Camera.main.transform.rotation;
            //change player rotation
            transform.rotation = Quaternion.Euler(0, camRot.eulerAngles.y, 0);
        }
    }

    void MovementUpdate()
    {

        if (Input.GetKeyDown(KeyCode.Q) && !isInRadialMenu)
        {
            radialMenu1.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isInRadialMenu = true;
            Time.timeScale = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            radialMenu1.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            int index = radialMenu1.GetComponent<RMF_RadialMenu>().index;
            ChangeElementBasedOnIndex(false, index);
            isInRadialMenu = false;
            Time.timeScale = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !isInRadialMenu)
        {
            radialMenu2.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isInRadialMenu = true;
            Time.timeScale = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            radialMenu2.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            int index = radialMenu2.GetComponent<RMF_RadialMenu>().index;
            ChangeElementBasedOnIndex(true, index);
            isInRadialMenu = false;
            Time.timeScale = 1.0f;
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 && !isInRadialMenu && !animator.GetBool("IsDoingSomething"))
        {
            rotation = Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotAngle + pivot.transform.eulerAngles.y, transform.eulerAngles.z);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, rotation + pivot.transform.eulerAngles.y, transform.eulerAngles.z), Time.deltaTime * 15);
            Vector3 temp = transform.forward * speed;
            temp.y = rb.velocity.y;
            rb.velocity = temp;
            animator.SetBool("running", true);
        } else
        {
            animator.SetBool("running", false);
        }
    }

    public void ChangeElementBasedOnIndex(bool hand, int i)
    {
        int outi = 0;
        switch (i){
            case 0:
                outi = 1;
                break;
            case 1:
                outi = 2;
                break;
            case 2:
                outi = 4;
                break;
            case 3:
                outi = 8;
                break;
            case 4:
                outi = 16;
                break;
        }
        if (hand)
        {
            ChangeElement(outi);
        } else {
            ChangeElement2(outi);
        }
    }

    public void ChangeElement(int i)
    {
        //TODO Change Element Code Tiong ples do
        PlayerPrefs.SetInt("rHand", i);
    }

    public void ChangeElement2(int i)
    {
        PlayerPrefs.SetInt("lHand", i);
    }

    /// <summary>
    /// All damage taken would come through here
    /// </summary>
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            //die
        }
    }
}

