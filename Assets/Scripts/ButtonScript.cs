using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TopDownController controller = other.GetComponent<TopDownController>();

            controller.CanPressButton = true;

            controller.ButtonPressed += OnButtonPressed;
            Debug.Log("Entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TopDownController controller = other.GetComponent<TopDownController>();

            controller.CanPressButton = false;

            controller.ButtonPressed -= OnButtonPressed;
        }
    }

    private void OnButtonPressed(object sender, ButtonPressedEventArgs input)
    {
        Debug.Log("kappa");
    }
}
