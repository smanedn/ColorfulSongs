using System.Runtime.InteropServices;
using UnityEngine;

public class PortalGenerator : MonoBehaviour
{
    [SerializeField] private GameObject endPortalPrefab;
    [SerializeField] private GameObject goodPortalPrefab;
    [SerializeField] private GameObject badPortalPrefab;
    [SerializeField] private GameObject parent;
    private int numberOfPortals = 4;
    private Vector3[] portalPositions;
    [SerializeField] private Vector3[] portalRotations;
    [SerializeField] private AudioClip goodSFX;
    [SerializeField] private AudioClip badSFX;
    [SerializeField] private GameObject generator;

    private GameObject[] portalsArray;
    private int goodPortal;

    private int endPortalX;
    private float y;
    private int endPortalZ;

    void Start()
    {
        int x = generator.GetComponent<TerrainGenerator>().getStartX("PortalGenerator")-1;
        int c = (generator.GetComponent<TerrainGenerator>().getEndZ("PortalGenerator") - generator.GetComponent<TerrainGenerator>().getStartZ("PortalGenerator")) / numberOfPortals;
        int z = generator.GetComponent<TerrainGenerator>().getStartZ("PortalGenerator");
        y = generator.GetComponent<TerrainGenerator>().getStartY("PortalGenerator")+1.5f;

        endPortalX = generator.GetComponent<TerrainGenerator>().getEndX("PortalGenerator");
        endPortalZ = z + c * numberOfPortals / 2;
        goodPortal = Random.Range(0, numberOfPortals);

        for (int i = 0; i < numberOfPortals; i++)
        {
            
            GameObject portalPrefab = (i == goodPortal) ? goodPortalPrefab : badPortalPrefab;
            if (i == 0)
            {
                GameObject endPortal = Instantiate(endPortalPrefab, new Vector3(endPortalX, y, endPortalZ), Quaternion.Euler(portalRotations[i]));
                endPortal.name = "EndPortal";
                endPortal.tag = "Untagged";
                endPortal.transform.SetParent(parent.transform);
            }
            GameObject newPortal = Instantiate(portalPrefab, new Vector3(x,y,z+c*i), Quaternion.Euler(portalRotations[i]));
            
            newPortal.name = "Portal" + i;
            newPortal.transform.SetParent(parent.transform);


            AudioSource audioSource = newPortal.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = (i == goodPortal) ? goodSFX : badSFX;
        }
    }

    public int getEndX()
    {
        return endPortalX;
    }

    public int getEndZ()
    {
        return endPortalZ;
    }
}
