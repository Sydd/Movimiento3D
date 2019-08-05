using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILife
{
    float HealthInitial { get; set; }
    float Health { get; set; }
    bool isDead { get; set; }

    void ReceiveDamage(float amount);
    void Heal(float amount);

    Action onReceiveDamage { get; set; }
    Action onHeal { get; set; }
    Action onDead { get; set; }
}
