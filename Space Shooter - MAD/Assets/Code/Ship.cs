using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rigidbody2D; //allow applying force and shit, lowercase rb2d = name, which can be anything
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public Transform projectileSpawnPoint2;

    public float acceleration;
    public float maxSpeed;
    public int maxArmor;
    public float fireRate;
    public float projectileSpeed;

    [HideInInspector] public float currentSpeed;
    [HideInInspector] public int currentArmor;

    [HideInInspector] public bool canShoot; //[HideInInspector] hides variable from being changed in Inspector in Unity

    [HideInInspector] ParticleSystem thrustParticles;

    private void Awake()
    {
        canShoot = true;
        currentArmor = maxArmor;
        thrustParticles = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(FireRateBuffer());
    }
    private void FixedUpdate()
    {
        if(rigidbody2D.velocity.magnitude > maxSpeed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
        }
    }
    public void Thrust()
    {
        rigidbody2D.AddForce(transform.up * acceleration); //AddForce w/ direction and speed
        thrustParticles.Emit(1);
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
        projectile.GetComponent<Projectile>().GetFired(gameObject);
        Destroy(projectile, 4); //Destroy object after 4 seconds
        StartCoroutine(FireRateBuffer());
    }

    public void FireProjectile2()
    {
        GameObject projectile2 = Instantiate(projectilePrefab, projectileSpawnPoint2.position, transform.rotation);
        projectile2.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
        projectile2.GetComponent<Projectile>().GetFired(gameObject);
        Destroy(projectile2, 4);
        StartCoroutine(FireRateBuffer());
    }

    private IEnumerator FireRateBuffer()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void TakeDamage(int damageToGive)
    {
        //TODO: play getHitSound
        currentArmor -= damageToGive;
        if (currentArmor == 0)
        {
            Explode();
        }

        if (GetComponent<PlayerShip>())
        {
            HUD.Instance.DisplayHealth(currentArmor, maxArmor);
        }
    }

    public void Explode()
    {
        ScreenShakeManager.Instance.ShakeScreen();
        //TODO: play deathSound
        Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
        Destroy(gameObject); //refers to THIS game object
        FindObjectOfType<EnemyShipSpawner>().CountEnemyShips();
        //my milikshake brings all the boys to the yard, and they're like, it's better than yours, damn right, it's better than yours. I could teach ya, but I'd have to charge.
    }
}
