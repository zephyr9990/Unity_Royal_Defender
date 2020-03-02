using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangedWeapon
{
    void Fire(Animator shooterAnimator);
    void StopFiring();
}
