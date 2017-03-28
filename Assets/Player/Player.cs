using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private CameraRaycaster raycaster = null;

    [SerializeField] private float maxHealthPoints = 100.0f;
    [SerializeField] private float minHealthPoints = 0.0f;
    [SerializeField] private float meleeDamage = 20.0f;
    [SerializeField] private float timeBetweenHits = 0.5f;
    [SerializeField] private float meleeAttackRange = 2.0f;
    private float currentHealthPoints;
    private float lastHitTime = 0.0f;

    private GameObject currentTarget;

    private void Start()
    {
        currentHealthPoints = maxHealthPoints;
        raycaster = Camera.main.GetComponent<CameraRaycaster>();

        raycaster.notifyMouseClickObservers += OnMouseClick;
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    private void OnMouseClick(RaycastHit hit, int layer)
    {
        //Player attack
        if(layer == Utilities.EnemyLayer)
        {
            GameObject enemy = hit.collider.gameObject;
            currentTarget = enemy;

            Component damageableComponent = currentTarget.GetComponent(typeof(IDamageable));

            //Check if enemy is in range
            bool enemyIsInRange = Vector3.Distance(enemy.transform.position, transform.position) <= meleeAttackRange;
                
            if (damageableComponent && Time.time - lastHitTime > timeBetweenHits && enemyIsInRange)
            {
                (damageableComponent as IDamageable).TakeDamage(meleeDamage);
                lastHitTime = Time.time;
            }
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