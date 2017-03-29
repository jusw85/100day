using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable {

    void Damage(DamageInfo damageInfo);

}

public class DamageInfo {
    public float damage;
}
