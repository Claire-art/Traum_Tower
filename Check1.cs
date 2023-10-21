using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Check1 : MonoBehaviour
{
    public static int key1 = 0;
    public GameObject pass;
    public GameObject triggerObject;

    // Assuming this is your input field
    public InputField myInputField;

    void Update()
    {
        // If the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Call the check function to check the answer
            check();
        }

        // If the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Set the focus to the input field
            myInputField.Select();
            myInputField.ActivateInputField();
        }
    }

    public void check()
    {
        if (myInputField.text == "물병자리")
        {
            key1 = 1;
            print("정답");
            Destroy(pass);
            Destroy(triggerObject);

            // After checking, clear the input field and set focus back to it for next input 
            myInputField.text = "";
            myInputField.Select();
            myInputField.ActivateInputField();
        }

        else
        {
            print("틀린 답");
            pass.SetActive(true);

            // If the answer is wrong, clear the input field and set focus back to it for next try 
            myInputField.text = "";
            myInputField.Select();
            myInputField.ActivateInputField();
        }
    }
}
