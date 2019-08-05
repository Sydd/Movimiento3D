using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBullet : MonoBehaviour//, IBullet
{
    public WeaponName weaponName;

    bool readyToUse;

    public float timeAlive = 3f;

    float timeshooted;

    public EventManager.Explode bulletExplode;
    void Awake()
    {
        Reset();
    }
    public void Shoot(Vector3 origin, Vector3 dir)
    {
       // BulletsPool.Instance.RemoveBullet(this);

        gameObject.SetActive(true);

        readyToUse = false;

        transform.position = origin + dir;
    }

    public WeaponName GetWeapon()
    {
        return weaponName;
    }

    public void Reset()
    {
        readyToUse = true;

       // BulletsPool.Instance.AddBullet(this);

        timeshooted = 0;

        gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
        if (timeshooted > timeAlive)
        {
            Explode();
        }
        else
        {
            timeshooted += Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    public void Explode()
    {
        if (bulletExplode != null)
        {
            bulletExplode();
        }
    }

}
