﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Electricity")]
public class Electricity : Element {
    public override string ElementName { get { return "Electricity"; } }

    public override void DoBasic(ElementControl agent, Hand hand) {
        //instantiate chain lightning
        GameObject instance = Instantiate(Resources.Load<GameObject>("Elements/Electricity/Shock_Chain"), hand.handPos);
        instance.GetComponent<ChainLightningScript>().lockOn = agent.lockOn;
        instance.GetComponent<ChainLightningScript>().hand = hand;

        //start animation
        //agent.anim.SetBool("IsShocking", true);
    }

    public override void DoBig(ElementControl agent, Hand hand) {
        //flash
        /* TODO
             IEnumerator DoFlash() {
        float delay = 0.25f;
        //dash forward 
        //sof
        float range = 2.5f;
        //raycast in circle
        RaycastHit[] hitInfo = Physics.CapsuleCastAll(transform.position + transform.forward * range, transform.position + transform.forward * range, range, transform.forward, range, 1 << Layers.Enemy);
        foreach (RaycastHit hit in hitInfo) {
            Vector2 randomPos = Random.insideUnitCircle;
            transform.position = hit.transform.position + new Vector3(randomPos.x, transform.position.y, randomPos.y);
            // Vector3 dir = hit.transform.position - transform.position;
            //transform.rotation = Quaternion.LookRotation(dir, transform.up);
            yield return new WaitForSeconds(delay);
        }
    }
         * */
    }
}
