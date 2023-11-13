using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RaycastWeapon;

public class RaycastWeapon : Weapon
{
    [System.Serializable]
    public class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        public ParticleSystem hitEffect;
    }

    
    public GunSO weapon;

    bool isReloading = false;

    public ParticleSystem shotFlash;


    public Transform raycastOrigin;

    Ray ray;
    public RaycastHit hit;

    float accumulatedTime;

    private List<Bullet> bullets = new List<Bullet>();

    float maxLifeTime = 2f;

    public GameObject magazine;

    public AudioClip reloadSFX;
    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
    }
    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * weapon.bulletDrop;
        return (bullet.initialPosition) + (bullet.initialVelocity * bullet.time) + 0.5f * gravity * bullet.time *bullet.time;
    }
    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.hitEffect = Instantiate(weapon.hitEffect, position, Quaternion.identity);
        bullet.tracer = Instantiate(weapon.shotTrail, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);

        return bullet;
    }
    public override void StartAttack()
    {
        isFiring = true;
        FireBullet();
    }
    public override void UpdateAttack(float deltatime)
    {
        if (!weapon.automatic) return;
        accumulatedTime += deltatime;
        float fireInterval = 1.0f / weapon.fireRate;
        while (accumulatedTime >= 0.0f) 
        { 
            FireBullet();
            accumulatedTime -= fireInterval;
        }

    }
    private void Update()
    {
        ray.origin = raycastOrigin.position;
        ray.direction = raycastTarget.position - raycastOrigin.position;

        UpdateBullets(Time.deltaTime);
    }
    public void UpdateBullets(float deltatime)
    {
        SimulateBullets(deltatime);
        if (bullets.Count == 0) return;
        DestroyBullets();
    }
    void SimulateBullets(float deltatime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltatime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });

    }
    void DestroyBullets()
    {
       bullets.RemoveAll(bullet => bullet.time >= maxLifeTime );
    }
    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hit, distance))
        {
            bullet.hitEffect.transform.forward = hit.normal;
            bullet.hitEffect.transform.position = hit.point;

            bullet.time = maxLifeTime;

            end = hit.point;
            if (hit.transform.tag == "Target")
            {
                hit.transform.GetComponentInParent<BonecoAlvo>().Down();
            }

            var rb2d = hit.collider.gameObject.GetComponent<Rigidbody>();
            if (rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hit.point, ForceMode.Impulse);
            }
            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnHit(this, ray.direction, hit.point, hit.normal);
            }
            else
            {
                bullet.hitEffect.Play();
            }
        }
    }
    private void FireBullet()
    {if (actualAmmo <= 0 || isReloading) return;
        actualAmmo--;
        shotFlash.Play();

        audioSource.PlayOneShot(RandomAudioClip(attackSFX));
        Vector3 velocity = (raycastTarget.position - raycastOrigin.position).normalized * weapon.bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);

        bullets.Add(bullet);
        recoil.GenerateRecoil(weaponName.ToString());
    }

    public override void StopAttack ()
    {
        isFiring = false;
    }
    public override void AddAmmo(int ammo)
    {
        holsterAmmo += ammo;
    }
    public void Reload()
    {
        if (holsterAmmo > maxAmmo)
        {
            SetActualAmmo(maxAmmo);
        }
        else
        {
            SetActualAmmo(holsterAmmo);
        }
    }
    public void SetActualAmmo(int value)
    {
        holsterAmmo -= value;
        actualAmmo = value;
    }
    public void SetIsReloading(bool reloading)
    {
        isReloading = reloading;
    }
    public bool GetIsReloading()
    {
        return isReloading;
    }
    public AudioClip RandomAudioClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public override void equipSound()
    {

    }
}
