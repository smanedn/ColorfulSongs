using UnityEngine;

public class PortalGenerator : MonoBehaviour
{
    [SerializeField] private GameObject goodPortalPrefab;
    [SerializeField] private GameObject badPortalPrefab;
    [SerializeField] private int numberOfPortals;
    [SerializeField] private Vector3[] portalPositions;
    [SerializeField] private Vector3[] portalRotations;
    [SerializeField] private AudioClip goodSFX;
    [SerializeField] private AudioClip badSFX;

    private GameObject[] portalsArray;
    private int goodPortal;

    void Start()
    {
        if (portalPositions.Length < numberOfPortals || portalRotations.Length < numberOfPortals)
        {
            Debug.LogError("Le posizioni o le rotazioni dei portali sono insufficienti per il numero di portali configurati.");
            return;
        }

        portalsArray = new GameObject[numberOfPortals];
        goodPortal = Random.Range(0, numberOfPortals);

        for (int i = 0; i < numberOfPortals; i++)
        {
            GameObject portalPrefab = (i == goodPortal) ? goodPortalPrefab : badPortalPrefab;
            GameObject newPortal = Instantiate(portalPrefab, portalPositions[i], Quaternion.Euler(portalRotations[i]));
            newPortal.name = "Portal" + i;

            AudioSource audioSource = newPortal.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = (i == goodPortal) ? goodSFX : badSFX;
        }
    }
}
