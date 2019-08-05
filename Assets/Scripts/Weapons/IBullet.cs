using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBullet : IPooleable
{
    WeaponName GetWeapon();
    void Shoot(Vector3 origin, Vector3 dir);
    void Explode();
    Action onHit { get; set; }
}
