using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealthPoints = 100.0f;
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
}