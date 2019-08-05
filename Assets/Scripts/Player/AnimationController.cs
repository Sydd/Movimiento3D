using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

// TODO: Cambiar las strings de animacion a hash o desharcodearlos de alguna forma.
public class AnimationController : MonoBehaviour
{
    public WeaponName WeaponName;

    WeaponController weaponController;
    PlayerMovement playerMovent;
    public SkeletonAnimation skeletonAnimation;

    public PlayerState actualState;


    [SerializeField]
    //Dictionary<WeaponName, List<AnimationZombie>> animsByWeapon = new Dictionary<WeaponName, List<AnimationZombie>>();

    public List<AnimationZombie> AnimationList = new List<AnimationZombie>();

    bool ChanginWeapon;

    // Update is called once per frame
    void Awake()
    {
        actualState = PlayerState.Idle;

        weaponController = GetComponent<WeaponController>();
        playerMovent = GetComponent<PlayerMovement>();


        weaponController.onWeaponChanged += onWeaponChanged;
        weaponController.onWeaponWasShooted += onWeaponShooted;

        playerMovent.onFlip += onFlip;
        playerMovent.onPlayerRun += OnPlayerRun;
        playerMovent.onPlayerWalk += OnPlayerWalk;
        playerMovent.onPlayerStop += OnPlayerStop;

    }
    void AnimationCompleteHandler(TrackEntry track)
    {
        // Reiniciamos la animacion que termina, solo si esta completada y si esta en el track 1( armas)
        if (track.TrackIndex == 1 && track.IsComplete)
        {
            skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0.5f);
            // state.ClearTrack(1);
        }

        if (ChanginWeapon)
        {
            TrackEntry auxTrack = SetAnimation(0, GetAnimation(actualState), true);
            auxTrack.MixDuration = 0;
            ChanginWeapon = false;
        }
    }
    void Start()
    {
        skeletonAnimation.AnimationState.Complete += AnimationCompleteHandler;
        skeletonAnimation.AnimationState.Start += AnimationStartedHandler;
    }
    void AnimationStartedHandler(TrackEntry track)
    {
        // if (ChanginWeapon)
        // {
        //     Debug.Log("changin weapon");
        //     track.MixDuration = 1;
        //     ChanginWeapon = false;
        // }
    }
    void onWeaponChanged(Weapon weapon)
    {
        this.WeaponName = weapon.Config.WeaponName;
        //skeletonAnimation.AnimationState.SetEmptyAnimations(0);
        // TODO: Aca hay que arreglar que cuando llegue el evento se seteen las animaciones correctamente.
        ChanginWeapon = true;
    }

    void onWeaponShooted()
    {
        Debug.Log("Shoot");
        SetAnimation(1, GetAnimation(PlayerState.Shoot), false);
    }

    TrackEntry SetAnimation(string animation, bool loop)
    {
        return skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
    }

    TrackEntry SetAnimation(int layer, string animation, bool loop)
    {
        return skeletonAnimation.AnimationState.SetAnimation(layer, animation, loop);

    }

    public TrackEntry AddAnimation(int layer, string animation, bool loop)
    {
        return skeletonAnimation.AnimationState.AddAnimation(layer, animation, loop, 0f);
    }
    public void onFlip(bool facingRight)
    {
        skeletonAnimation.Skeleton.ScaleX = facingRight ? 1f : -1f;
    }

    void OnPlayerStop()
    {
        //Debug.Log("OnPlayerStop");
        ChangeState(PlayerState.Idle);
    }

    void OnPlayerWalk()
    {
        //Debug.Log("OnPlayerWalk");
        ChangeState(PlayerState.Walk);
    }

    void OnPlayerRun()
    {
        //Debug.Log("OnPlayerRun");
        ChangeState(PlayerState.Run);
    }

    void ChangeState(PlayerState newState)
    {
        //Debug.Log("oldState: " + actualState + " newState: " + newState);
        if (newState != actualState)
        {
            //Debug.Log("Enter: " + newState);
            actualState = newState;

            //TODO: PASAR LOGICA DE ANIMACION AL CONTROLADOR
            string anim = GetAnimation(newState);

            SetAnimation(anim, true);
        }
    }

    public string GetAnimation(PlayerState state)
    {

        foreach (AnimationZombie animAux in AnimationList)
        {
            if (animAux.weapon == WeaponName && animAux.state == state)
                return animAux.animation.Animation.Name;
        }
        Debug.LogError("ERRROR: Animation not found !");
        return "ERRROR: Animation not found !";
    }


    [System.Serializable]
    public class AnimationZombie
    {
        public AnimationReferenceAsset animation;

        public WeaponName weapon;
        public PlayerState state;

    }
}
