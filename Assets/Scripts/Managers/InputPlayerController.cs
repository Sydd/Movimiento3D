using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerController : MonoBehaviour
{

    public GameObject Player;

    PlayerMovement playerMovement;
    WeaponController playerWeapon;


    void Awake()
    {
        if (Player != null)
        {
            playerMovement = Player.GetComponent<PlayerMovement>();
            playerWeapon = Player.GetComponent<WeaponController>();
        }
        else
        {
            Debug.Log("NO PLAYER ASSIGNED");
        }
    }
    void Start()
    {

    }

    public void PoolTrigger() { }

    public void ReleaseTrigger() { }

    void Update()
    {

#if UNITY_EDITOR
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical"));
        
        Vector3 vecMovement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        playerMovement.Move(vecMovement);

        if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
        {
            playerWeapon.Shoot();
        }
    }
#endif

}
