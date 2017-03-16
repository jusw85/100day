using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {

    public int PIXELS_TO_UNITS = 32;

    private void Start() {
        float num1 = 480f;
        float num2 = 270f;
        float num3 = num1 / num2;
        float num4 = (float)Screen.width / (float)Screen.height;
        if ((double)num4 >= (double)num3) {
            Camera.main.orthographicSize = num2 / 1f / (float)this.PIXELS_TO_UNITS;
        } else {
            float num5 = num3 / num4;
            Camera.main.orthographicSize = num2 / 1f / (float)this.PIXELS_TO_UNITS * num5;
        }
    }

}
