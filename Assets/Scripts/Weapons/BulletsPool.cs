using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour
{

    static public BulletsPool Instance;

    public List<IBullet> bulletListEnabled = new List<IBullet>();
    public GameObject clubBulletPrefab;
    public GameObject SprayBulletPrefab;
    public GameObject WaterPistolBulletPrefab;
    void Awake()
    {
        Instance = this;
        LoadBullets();
    }
    
    public IBullet GetBullet(WeaponName wep)
    {
        IBullet aux = null;

        foreach (IBullet a in bulletListEnabled)
        {
            if (a.GetWeapon() == wep)
            {
                aux = a;
            }
        }

        if (aux == null)
        {
            aux = CreateBullet(wep);
        }

        aux.Reset();
        return aux;
    }

    public void AddBullet(IBullet toAdd)
    {
        bulletListEnabled.Add(toAdd);
    }

    public void RemoveBullet(IBullet toRemove)
    {
        bulletListEnabled.Remove(toRemove);
    }
    IBullet CreateBullet(WeaponName bulletType)
    {
        GameObject aux;
        switch (bulletType)
        {
            case WeaponName.Spray:
                aux = Instantiate(SprayBulletPrefab, Vector3.zero, Quaternion.identity, this.transform);
                break;
            case WeaponName.Club:
                aux = Instantiate(clubBulletPrefab, Vector3.zero, Quaternion.identity, this.transform);
                break;
            case WeaponName.WaterGun:
                aux = Instantiate(WaterPistolBulletPrefab, Vector3.zero, Quaternion.identity, this.transform);
                break;
            default:
                Debug.LogError("Se esta instanciando un prefab que no existe");
                aux = Instantiate(SprayBulletPrefab, Vector3.zero, Quaternion.identity, this.transform);
                break;
        }
        return aux.GetComponent<IBullet>();
    }

    void LoadBullets()
    {

        for (int x = 0; x < System.Enum.GetValues(typeof(WeaponName)).Length; x++)
        {
            for (int i = 0; i < 3; i++)
            {
                CreateBullet((WeaponName)x);
            }

        }

    }
}
