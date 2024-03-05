using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDisplay : MonoBehaviour
{
    public List<Slider> sliders;
    public PlayerState playerState => GameManager.Instance.saveData.playerState;
    private void Awake()
    {
        foreach (Slider slider in sliders)
        {
            switch (slider.gameObject.name)
            {
                case "HealthSlider":
                    slider.value = playerState.health / 100f;
                    break;
                case "MoralSlider":
                    slider.value = playerState.moral / 100f;
                    break;
                case "ReputationSlider":
                    slider.value = playerState.reputation / 100f;
                    break;
                default:
                    break;
            }
        }
    }
}
