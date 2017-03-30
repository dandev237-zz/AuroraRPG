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
    [SerializeField] private float chaseRadius = 7.5f;

    [SerializeField] private GameObject projectileObject;
    [SerializeField] private GameObject projectileSpawn;
    [SerializeField] private float damagePerShot = 6.0f;
    [SerializeField] private float secondsBetweenShots = 0.5f;
    [SerializeField] private Vector3 aimOffset = Vector3.up;
    private bool isAttacking = false;

	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints / (float)maxHealthPoints;
		}
	}

	private void Start()
    {
        currentHealthPoints = maxHealthPoints;
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("SpawnProjectile", 0.0f, secondsBetweenShots);  //TODO switch to coroutine
        }

        if(distanceToPlayer > attackRadius)
        {
            CancelInvoke();
            isAttacking = false;
        }

        if (distanceToPlayer <= chaseRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }

    private void SpawnProjectile()
    {
        GameObject firedProjectile = Instantiate(projectileObject, projectileSpawn.transform.position, Quaternion.identity);

        //Set projectile damage
        Projectile projectileComponent = firedProjectile.GetComponent<Projectile>();
        projectileComponent.damageOnHit = damagePerShot;

        Vector3 direction = (player.transform.position + aimOffset - projectileSpawn.transform.position).normalized;
        float projectileSpeed = projectileComponent.projectileSpeed;

        firedProjectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
    }

    void IDamageable.TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, minHealthPoints, maxHealthPoints);
        if(currentHealthPoints == minHealthPoints)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}