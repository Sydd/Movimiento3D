using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBullet : MonoBehaviour//, IBullet
{

    public WeaponName weaponName;

    Rigidbody body;
    EventManager.Explode Exploded;

    public float timeToExplode;

    Vector3 direction = Vector3.zero;

    public Vector3 offSet;
    public float velocity;
    float elapsedTime;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        Reset();
    }
    
    public void Reset()
    {
//        BulletsPool.Instance.AddBullet(this);

        elapsedTime = 0;

        gameObject.SetActive(false);
    }

    public void Shoot(Vector3 origin, Vector3 dir)
    {

   //     BulletsPool.Instance.RemoveBullet(this);

        direction = dir;

        transform.position = origin + new Vector3(offSet.x * dir.x, offSet.y, 0);

        gameObject.SetActive(true);

    }

    public void Explode()
    {
        if (Exploded != null)
        {
            Exploded();
        }
    }

    public WeaponName GetWeapon()
    {
        return weaponName;
    }

    void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (elapsedTime > timeToExplode)
        {
            Explode();
        }
        else
        {
            body.MovePosition(Vector3.Lerp(body.position, body.position + direction, velocity));

            elapsedTime += Time.fixedDeltaTime;
        }
    }
}

