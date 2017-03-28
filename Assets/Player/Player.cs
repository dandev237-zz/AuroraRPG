using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealthPoints = 100.0f;
    [SerializeField] private float minHealthPoints = 0.0f;
    private float currentHealthPoints;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
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
        if(currentHealthPoints == minHealthPoints)
        {
            Destroy(gameObject);
        }
    }
}