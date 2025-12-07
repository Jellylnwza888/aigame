using System.Collections.Generic;
using UnityEngine;

public class maincontroller : MonoBehaviour
{

    public List<GameObject> Object;
    public UDPReceiver udpReceiver;
    HashSet<string> spawnedObjects = new HashSet<string>();



    public void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (udpReceiver.addobj.Contains("scissors"))
        {
            spawned();
        }
    }

    public void spawned()
    {
        for (int i = 0; i < udpReceiver.addobj.Count; i++)
        {
            string mostDetected = udpReceiver.addobj[i];

                if (spawnedObjects.Contains(mostDetected)) 
                    continue;

            foreach (var obj in Object)
            {
                if (obj.name == mostDetected)
                {
                    Instantiate(obj, new Vector3(0, 1, 0), Quaternion.identity);
                    spawnedObjects.Add(mostDetected);
                    return;
                    
                }
            }



            
        }
    }

    
    
}