using UnityEngine;

public class PortalGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject portals;
    [SerializeField] private GameObject portal;
    private GameObject[] portalsArray;
    private int goodPortal;
    [SerializeField] private AudioClip goodSFX;
    [SerializeField] private AudioClip badSFX;
    void Start()
    {
        goodPortal = Random.Range(0, 4);

        for (int i = 0; i < portalsArray.Length; i++)
        {
            
            AudioClip sfx = badSFX;
            string tag = "BadPortal";
            if (i == goodPortal)
            {
                sfx = goodSFX;
                tag = "Portal";
            }

            //portal1: -2.25,1,3
            //portal2: 1.25,1,3
            //portal3: 3,1,1.25
            //portal4: 3,1,-2.25
            var newPortal = Instantiate(portal, new Vector3(x, y, z), Quaternion.identity);
            newPortal.name = "Portal" + i;
            newPortal.tag = tag;

            AudioSource audioSource = newPortal.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load<AudioClip>(sfx);
            audioSource.loop = true;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
