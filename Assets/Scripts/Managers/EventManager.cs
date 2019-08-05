using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    static EventManager instance;

    public delegate void PressTouch();

    public delegate void OnTouch();

    public delegate void UpTouch();
    public delegate void ReleaseKey();

    public delegate void Shoot(Vector3 origin, Vector3 dir);

    public delegate void LoadWeapon(Weapon weapon);
    public delegate void inputFire();

    public delegate void Explode();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public static EventManager GetInstance()
    {
        if (instance == null)
        {
            instance = new EventManager();
        }

        return instance;
    }


}
