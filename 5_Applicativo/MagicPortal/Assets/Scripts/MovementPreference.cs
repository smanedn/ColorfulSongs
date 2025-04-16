using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementPreference : MonoBehaviour

{

    public void MovementData()
    {
        int pickedMovementIndex = GetComponent<TMP_Dropdown>().value;
        var select = 0;

        switch (pickedMovementIndex)
        {
            case 0:
                select = 1;
                PlayerPrefs.SetInt("DefaultMovement", select);
                break;
            case 1:
                select = 0;
                PlayerPrefs.SetInt("DefaultMovement", select);
                break;
        }


        //Debug.Log(select);
    }
}

