using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Unidades por segundo.")]
    [Tooltip("Cuantas unidades se mueve por segundo en X & en Z")]
    public Vector3 speed;
    [Header("Cuantos segundos tienen que pasar para que termine corriendo a la velocidad full")]
    public float TimeToAchieveFullSpeed;
    [Header("En funcion de _timeMoving / TimeToAchieveFullSpeed TimeToAchieveFullSpeed")]
    public AnimationCurve speedCurve;
    public float PercentOfVelocityToRun;

    Rigidbody body;

    bool facingRight = true;
    Vector3 vecMovementFactor = Vector3.zero;

    // TODO: Esto hay que mandarlo al player
    // y desde aca solo hay que ejecutar el evento onMoving.
    public Action onPlayerWalk;
    public Action onPlayerRun;
    public Action onPlayerStop;
    public Action<bool> onFlip;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private float _timeMoving;
    void FixedUpdate()
    {
            
        if (vecMovementFactor.magnitude > 0)
        {
            if (vecMovementFactor.x > 0 && !facingRight)
            {
                facingRight = true;
                if (onFlip != null)
                {
                    onFlip(true);
                }

            }
            else if (vecMovementFactor.x < 0 && facingRight)
            {
                facingRight = false;
                if (onFlip != null)
                {
                    onFlip(false);
                }
            }
            
            _timeMoving += Time.fixedDeltaTime;
            Vector3 actualSpeed = Vector3.Scale(vecMovementFactor, speed * speedCurve.Evaluate(_timeMoving / TimeToAchieveFullSpeed) );
            Vector3 newPosition = body.position + (actualSpeed * Time.fixedDeltaTime);
            body.MovePosition(newPosition);
            
            // Me fijo si esta corriendo o caminando y aviso a los suscriptores
            if(actualSpeed.magnitude > (speed * PercentOfVelocityToRun).magnitude){
                if(onPlayerRun!=null){
                    onPlayerRun();
                }
            }else{
                if(onPlayerWalk!=null){
                    onPlayerWalk();
                }
            }

           // Debug.Log("actualSpeed x : " + actualSpeed);
        }else{

            _timeMoving = 0;
            if( onPlayerStop !=null){
                onPlayerStop();
            }
        }
    }

    public void Move(Vector3 vecMovementFactor)
    {
        this.vecMovementFactor = vecMovementFactor;
    }

    public void StopMove(){
        this.vecMovementFactor = Vector3.zero;
    }
}
