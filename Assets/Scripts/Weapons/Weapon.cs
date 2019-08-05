using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon
{
    public WeaponConfig Config { get; private set; }
    public Action onAmmoDepleted;
    public Action onShoot;

    public int _ammoLeft;
    float _timeLastShoot;
    private Vector3 _facingDirection;

    public Weapon(WeaponConfig config)
    {
        this.Config = config;
        this._facingDirection = Vector3.right;
        Init();
    }

    private void Init()
    {
        _timeLastShoot = 0;
        _ammoLeft = Config.Magazine;
    }

    public void Reload(int ammoToAdd)
    {
        _ammoLeft += Mathf.Clamp(_ammoLeft + ammoToAdd, 0, Config.MagazineMax);
    }

    public void Shoot(Vector3 origin)
    {
        if (Math.Abs(_timeLastShoot - Time.realtimeSinceStartup) >= Config.CoolDown)
        {
            if (_ammoLeft > 0 || Config.IsMelee)
            {
                _timeLastShoot = Time.realtimeSinceStartup;

                //BulletsPool.Instance.GetBullet(Config.WeaponName).Shoot(origin, _facingDirection);

                _ammoLeft--;

                onShoot();

            }
            else
            {
                if (onAmmoDepleted != null)
                {
                    onAmmoDepleted();
                }
            }
        }
    }


    public void Flip(bool facingRight)
    {
        if (facingRight)
        {
            _facingDirection = Vector3.right;
        }
        else
        {
            _facingDirection = Vector3.left;
        }
    }
}
