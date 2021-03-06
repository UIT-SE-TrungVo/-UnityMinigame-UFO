using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerBehaviour : MonoBehaviour
{
    const float MAX_DISTANCE = 40;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] arrGameObject = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in arrGameObject)
        {
            if (obj.CompareTag("UI")) continue;
            if (!obj.CompareTag("Wall") && isTooFar(obj))
            {
                if (obj.CompareTag("PickUp"))
                    obj.GetComponent<PickupBehaviour>().CreateNewPickup();
                Destroy(obj.gameObject);
            }
        }
    }

    bool isTooFar(GameObject obj)
    {
        if (Vector3.Distance(obj.transform.position, player.transform.position) > MAX_DISTANCE) return true;
        else return false;
    }
}
