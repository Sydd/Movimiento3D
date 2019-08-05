using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New WeaponDataBase", menuName = "Weapon DataBase", order = 51)]
public class WeaponsDataBase : ScriptableObject
{
    public List<WeaponConfig> Weapons = new List<WeaponConfig>();
    // Start is called before the first frame update
    
    public WeaponConfig GetWeapon(WeaponName wep)
    {
        WeaponConfig aux = Weapons.FirstOrDefault( w => w.WeaponName.Equals(wep));
        
        if (!aux)
        {
            aux = Weapons[0];
            Debug.LogError("NO SE ENCONTRO EL ARMA " + wep.ToString());
        }
        return aux;
    }

}
