using System.Collections;
using UnityEngine;

public class HitGUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject hitGUI;

    // Update is called once per frame
    public void showHitGUI()
    {
        hitGUI.gameObject.SetActive(true);
        StartCoroutine(wait());
        hitGUI.gameObject.SetActive(false);

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
