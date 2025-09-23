using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    // Identity
    public string characterName = "Character";
    public int level;
    public float currentEXP;
    public float EXP;

    // Stats
    public Stat stat = new Stat();

    // Inventory && Equiment
    public Inventory inventory;
    // TODO
    // public Equipment equipment;

    // Skills
    public Skill[] skills;

    // State
    public bool isAlive = true;

    // Controller

    // Animator / FX

    private void Awake()
    {
        //stat = new Stat();
        inventory = new Inventory();
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (skills.Length > 0)
            {
                Transform target = GameObject.FindWithTag("Enemy")?.transform;
                if (target != null)
                {
                    skills[0].Use(transform, target);
                }
            }
        }
    }
}