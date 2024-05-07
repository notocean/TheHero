using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderChange : MonoBehaviour
{
    public enum SliderType {
        Sound, Music
    }

    [SerializeField] private SliderType type;
    private Slider slider;

    private void Awake() {
        slider = GetComponent<Slider>();
    }

    private void Start() {
        switch (type) {
            case SliderType.Sound:
                slider.value = GameManager.Instance.soundFactor;
                break;
            case SliderType.Music:
                slider.value = GameManager.Instance.musicFactor;
                break;
        }
    }

    public void OnValueChanged() {
        switch (type) {
            case SliderType.Sound:
                GameManager.Instance.ChangeSoundFactor(slider.value);
                break;
            case SliderType.Music:
                GameManager.Instance.ChangeMusicFactor(slider.value);
                break;
        }
    }
}
