using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementPreference : MonoBehaviour

{

    public void MovementData()
    {
        int pickedMovementIndex = GetComponent<TMP_Dropdown>().value;
        var select = 1;

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
        PlayerPrefs.Save();



        Debug.Log(select);
    }
}

