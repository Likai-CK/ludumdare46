﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float currenthealth = 25f;
    private float maxhealth = 25f;
    private float regenRate = 2f;
    private float regenAmt = 1f;
    private Rigidbody2D _rigidBody;
    private Renderer _renderer;
    public Rigidbody2D _shipRigidBody;
    private bool isRegenerating;
    private float regenDelayAfterBreaking = 3000f;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currenthealth <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            _renderer.material.SetColor("_Color", new Color(_renderer.material.color.r, _renderer.material.color.g, _renderer.material.color.b, 0.0f));

        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            _renderer.material.color = RenderHealthColor(currenthealth);
            // _renderer.material.SetColor("_Color", RenderHealthColor(currenthealth)); // set color of the wall to the generated color
        }

    }

    /* broken
    private void delayRegen()
    {
        float timer = 0f;
        regen(false);
        while (timer < regenDelayAfterBreaking)
        {
            timer += Time.deltaTime;
        }
        regen(true);
    }

    public void regen(bool bl)
    {
        Debug.Log("regen2");
        if (bl)
        {
            isRegenerating = true;
            StartCoroutine(regen());
        }
        else
        {
            isRegenerating = false;
            StopCoroutine(regen());
        }

    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy1")
        {
            //Debug.Log("colliding with enemy. Wall health:" + currenthealth.ToString());
            currenthealth -= 1;
            //create a new shipsection, attach to nearest module
        }
    }

    // this will return an appropriate color for the walls based on its current health.
    // at max, color is (0,1,0)
    // at 50% max, color is (0.5,0.5,0)
    // at death, color is (1,0,0)
    private Color RenderHealthColor(float health)
    {
        float greenvalue = (currenthealth / maxhealth);
        float redvalue = ((maxhealth - currenthealth) / maxhealth);
        //Debug.Log("GREEN: " + greenvalue.ToString() + "RED: " + redvalue.ToString());
        return new Color(redvalue, greenvalue, 0f, 1f);

    }

    IEnumerator regen()
    {
        Debug.Log("adding health");
        currenthealth += regenAmt;
        yield return new WaitForSeconds(regenRate);
    }
}
