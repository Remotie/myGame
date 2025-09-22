using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public Stat stat;
    public Inventory inventory;
    public Skill[] skills;

    private void Awake()
    {
        stat = new Stat();
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