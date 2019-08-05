using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPooleable
{
    void Reset();
    Action onFinishToUse { get; set; }
}
