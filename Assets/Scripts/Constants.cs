using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {

    public static readonly int LINECAST_LAYERS = LayerMask.GetMask("Background", "EnemyHitbox");
    public static readonly int MATERIAL_FLASHAMOUNT_ID = Shader.PropertyToID("_FlashAmount");
    public static readonly int MATERIAL_FLASHCOLOR_ID = Shader.PropertyToID("_FlashColor");
}
