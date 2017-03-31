using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIWiring : MonoBehaviour {

    public Selectable firstSelectable;

    private void OnEnable() {
        if (firstSelectable != null) {
            firstSelectable.Select();
        }
    }

    private Dictionary<string, Slider> sliders;
    private Dictionary<string, Button> buttons;

    private void Awake() {
        sliders = new Dictionary<string, Slider>();
        Slider[] slidersA = GetComponentsInChildren<Slider>();
        foreach (Slider slider in slidersA) {
            sliders.Add(slider.name, slider);
        }
        buttons = new Dictionary<string, Button>();
        Button[] buttonsA = GetComponentsInChildren<Button>();
        foreach (Button button in buttonsA) {
            buttons.Add(button.name, button);
        }
    }

    public void AddSliderCallback(string key, UnityAction<float> action) {
        Slider slider;
        if (sliders.TryGetValue(key, out slider)) {
            slider.onValueChanged.AddListener(action);
        }
    }
    public void AddButtonCallback(string key, UnityAction action) {
        Button button;
        if (buttons.TryGetValue(key, out button)) {
            button.onClick.AddListener(action);
        }
    }
}
