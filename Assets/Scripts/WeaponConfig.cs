using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponConfig", menuName = "Weapon Config", order = 51)]
public class WeaponConfig : ScriptableObject {
    
    public WeaponName WeaponName;
    public GameObject BulletSpawnPoint;
    public GameObject BulletToUse;
    
    public bool IsUnlocked;
    //public bool Automatic; // Queda para otra version
    //public bool HasCasings; // Queda para otra version
    public int LevelToUnlock;
    public int CoinsToUnlock;
    public int Level;
    public int LevelMax;
    public int MaxPenetration;
    
    public float Damage;
    public float DamageBase;
    public float DamageMax;
    
    public float Range;
    //public float RangeBase; // Queda para otra version
    //public float RangeMax; // Queda para otra version
    
    public float CoolDown;
    public float CoolDownBase;
    public float CoolDownMax;
    
    public int Magazine;
    public int MagazineBase;
    public int MagazineMax;

    public bool IsMelee;

    public void Init(int Level, bool IsUnlocked){
        if (Level > LevelMax){
            Level = LevelMax;
        }

        this.Level = Level;	
        this.IsUnlocked = IsUnlocked;


		Damage = (((DamageMax - DamageBase) / LevelMax) * Level) + DamageBase;
		if(Damage>DamageMax)
		{
			Damage = DamageMax;
		}
       
        // Queda para otra version
		// Range = (((RangeMax - RangeBase) / LevelMax) * Level) + RangeBase;
        // if(Range > RangeMax)
		// {
		// 	Range = RangeMax;
		// }
       
		CoolDown = CoolDownBase - (((CoolDownBase - CoolDownMax) / LevelMax) * Level);
		if(CoolDown < CoolDownMax)
		{
			CoolDown = CoolDownMax;
		}
		 
		Magazine = (int) (((MagazineMax - MagazineBase) / LevelMax) * Level) + MagazineBase;
		if(Magazine > MagazineMax)
		{
			Magazine = MagazineMax;
		}
    }


}