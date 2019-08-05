using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Pensa que esta clase representa la parte del cerebro que aprendio a agarrar, cambiar y disparar armas
public class WeaponController : MonoBehaviour
{
    public WeaponsDataBase WeaponsDataBase;

    private Weapon _defaultWeapon;
    private Weapon _actualWeapon;
    
    public Action onWeaponWasShooted;
    public Action<Weapon> onWeaponChanged;

    public void Init(Weapon DefaultWeapon)
    {
        this._defaultWeapon = DefaultWeapon;
    }

    // Start is called before the first frame update
    void Awake()
    {
        // TODO: Esto no deberia ir aca.
        GetComponent<PlayerMovement>().onFlip += onFlip;

        //TODO: Cambiar el arma default segun el zombie, cargarlo desde el backend?
        ChangeWeapon(_defaultWeapon);
    }


    public void ChangeWeapon(Weapon weapon)
    {
        if(_actualWeapon != null){
            _actualWeapon.onAmmoDepleted -= onAmmoDepleted;
            _actualWeapon.onShoot -= onShoot;
        }
        _actualWeapon = weapon;
        _actualWeapon.onAmmoDepleted += onAmmoDepleted;
        _actualWeapon.onShoot += onShoot;
        onWeaponChanged(_actualWeapon);
    }

    void onShoot(){
        onWeaponWasShooted();
    }

    void onAmmoDepleted(){
        ChangeWeapon(_defaultWeapon);
    }

    public void Shoot()
    {
        _actualWeapon.Shoot(transform.position);
    }


    public void onFlip(bool facingRight)
    {
        _actualWeapon.Flip(facingRight);
    }
}
