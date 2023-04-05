using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderDisableInput : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == slider.gameObject)
        {
            Debug.Log("slider selected");
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
