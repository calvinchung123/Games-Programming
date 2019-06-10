using UnityEngine;
using System.Collections;

public class Red_Turret : MonoBehaviour {

    private Transform target;

    [Header("Turret Stats")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCoolDown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
	public float burnDamage = 100f;
    public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

    [Header("Setup Fields")]
    public string enemyTag = "RedEnemy";
    public Transform rotationPoint;
    public float turnSpeed = 10f;
    
    public GameObject muzzleFire;
    public Transform firePoint;

    // Use this for initialization
    void Start ()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
					impactEffect.Stop ();
					impactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        if(useLaser) {
            Laser();
        }
        else {
            if (fireCoolDown <= 0f)
            {
                Fire();
                fireCoolDown = 1f / fireRate;
            }

            fireCoolDown -= Time.deltaTime;
        }

	}

    void LockOnTarget() {
        // Target lock-on system
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotationPoint.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotationPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
		target.GetComponent<Enemy> ().TakeDamage (burnDamage * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
			impactEffect.Play();
			impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;

		impactEffect.transform.position = target.position + dir.normalized * 1.75f;

		impactEffect.transform.rotation = Quaternion.LookRotation (dir);
    }

    void Fire()
    {
        GameObject bulletFired = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletFired.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.ChaseTarget(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
