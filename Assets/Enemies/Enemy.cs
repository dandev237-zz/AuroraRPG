using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealthPoints = 100.0f;
    [SerializeField] private float minHealthPoints = 0.0f;
    private float currentHealthPoints;

    private AICharacterControl aiCharacterControl = null;
    private GameObject player = null;
    [SerializeField] private float attackRadius = 5.0f;
    [SerializeField] private float aggroRadius = 7.5f;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius)
        {
            print(gameObject.name + " attacking Player!");
            //TODO spawn projectile
        }

        if (distanceToPlayer <= aggroRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    void IDamageable.TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, minHealthPoints, maxHealthPoints);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, aggroRadius);
    }
}