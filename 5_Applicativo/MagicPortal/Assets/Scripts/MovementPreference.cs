using UnityEngine;

public class MovementPreference : MonoBehaviour
{
    public void HandleDataInput(int val)
    {
        switch (val)
        {
            case 0:
                PlayerPrefs.SetInt("DefaultMovement", 1);
                break;
            case 1:
                PlayerPrefs.SetInt("DefaultMovement", 0);
                break;
        }
    }
}
