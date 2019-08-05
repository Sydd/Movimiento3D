using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour, IBullet
{
    Rigidbody body;

    WeaponConfig weaponConfig;
    //PlayerMovement movementController;


    Vector3 direction = Vector3.zero;

    public Vector3 offSet;
    public float velocity;


    private Action _onHit;

    public Action onFinishToUse { 
        get { return _onHit; }
        set { _onHit = value; }
    }

    public Action onHit { 
        get { return _onHit; }
        set { _onHit = value; }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        Reset();
    }

    public void Explode()
    {
        throw new NotImplementedException();
    }

    public WeaponName GetWeapon()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(Vector3 origin, Vector3 dir)
    {
        direction = dir;

        //movementController.Move(dir);
        transform.position = origin + new Vector3(offSet.x * dir.x, offSet.y, 0);

        gameObject.SetActive(true);
    }

    void OntriggerEnter(Collider other)
    {
        ILife lifeable = other.GetComponent<ILife>();
        if (lifeable != null)
        {
            lifeable.ReceiveDamage(weaponConfig.Damage);
        }

        if(_onHit != null){
            _onHit();
        }
    }

    void FixedUpdate()
    {
        body.MovePosition(Vector3.Lerp(body.position, body.position + direction, velocity));
    }
}
