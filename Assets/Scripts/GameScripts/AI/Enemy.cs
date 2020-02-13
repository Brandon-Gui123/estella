﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {
    //element stuff
    public enum Effects { None, Stun, Burn, Freeze, Slow }
    public ParticleSystem onFirePs;
    public float debuffTimer = 0;
    public Effects currentDebuff = Effects.None;

    //stats
    public float health = 10;
    public float speed = 12;
    public float resistanceLevel = 1;
    public float unfreezeThreshold = 2;

    public Rigidbody rb;
    public Animator anim;
    public PlayerControl player;
    [HideInInspector]
    public float currentSpeed = 12;
    [HideInInspector]
    public float currentResistance = 1;
    [HideInInspector]
    public float currentFreezeThreshold = 2;

    //pathfinding
    public MapGrid map { get; private set; }

    public void Start() {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetComponentInChildren<Animator>();
        player = GameObject.FindObjectOfType<PlayerControl>();
        currentSpeed = speed;
        currentResistance = resistanceLevel;
        currentFreezeThreshold = unfreezeThreshold;
    }

    public void ReferenceMap(MapGrid _map) {
        map = _map;
        _map.enemies.Add(this);
    }

    public void Update() {
        float deltaTime = Time.deltaTime;

        if (debuffTimer <= 0) {
            currentDebuff = Effects.None;
        } else {
            debuffTimer = Mathf.Clamp(debuffTimer - deltaTime, 0, 100);
        }

        //check debuffs
        switch (currentDebuff) {
            case Effects.None:
                currentSpeed = speed;
                currentResistance = resistanceLevel;
                break;
            case Effects.Stun:
                //cannot move
                currentSpeed = 0;
                break;
            case Effects.Burn:
                TakeDamage(deltaTime * (1 / currentResistance));
                break;
            case Effects.Freeze:
                //cannot move
                currentSpeed = 0;
                break;
            case Effects.Slow:
                currentSpeed *= 0.7f;
                break;
            default:
                break;
        }
    }

    public void DebuffEnemy(float duration, Effects effect) {
        debuffTimer = duration;
        currentDebuff = effect;
    }

    public void TakeDamage(float damage) {
        if (currentDebuff == Effects.Freeze) {
            currentFreezeThreshold -= damage;
            if (currentFreezeThreshold <= 0) {
                currentFreezeThreshold = unfreezeThreshold;
                currentDebuff = Effects.None;
            }
        } else {
            health -= damage;
            if (health <= 0) {
                anim.SetTrigger("WhenDie");
            }
        }
    }

    /// <summary>
    /// Damage done to player should route through here
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="needRange">put 1 if need to be in range</param>
    public void DealDamage(float amount, int needRange) {
        if ((player.transform.position - transform.position).magnitude <= 1.5f || needRange != 1) {
            if (player.TakeDamage(amount, transform.position)) {
                StartCoroutine(TriggerAfterDelay(1));
            }
        }
    }

    /// <summary>
    /// Damage done to player should route through here, assuming no range constaint
    /// </summary>
    /// <param name="amount"></param>
    public void DealDamage(float amount) {
        if (player.TakeDamage(amount, transform.position)) {
            StartCoroutine(TriggerAfterDelay(1));
        }
    }

    IEnumerator TriggerAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        anim.SetTrigger("WhenPlayerDie");
    }
}
