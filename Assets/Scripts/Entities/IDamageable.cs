using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void Damage(DamageInfo damageInfo);

}

public struct DamageInfo {
    public float damage;
}
