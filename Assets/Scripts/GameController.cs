using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public WeaponConfig[] WeaponsSelected;
    public Weapon[] Weapons;
    public Weapon DefaultWeapon;
    public WeaponName DefaultWeaponName;


    public WeaponController WeaponController;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Weapons = new Weapon[WeaponsSelected.Length];

        for(int i=0; i < WeaponsSelected.Length; i++){
            Weapons[i] = new Weapon(WeaponsSelected[i]);
            if(WeaponsSelected[i].WeaponName.Equals(DefaultWeaponName)){
                DefaultWeapon = Weapons[i];
                _actualWeapon = i;
            }
        }
        // TODO: Esto hay que mejorarlo o crear una funcion para setear, ademas seguro que el weapon controller tiene que ir dentro de un script que sea player.
        WeaponController.Init(DefaultWeapon);
    }


    
    int _actualWeapon;
    void FixedUpdate()
    
    {
        if(Input.GetKeyDown(KeyCode.N)){
            int index = (++_actualWeapon) % Weapons.Length;
            WeaponController.ChangeWeapon(Weapons[index]);
        }
    }
}
