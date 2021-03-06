using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    public const float OFFSET = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //make a small floating-effect
        this.transform.Rotate(new Vector3(0, 0, OFFSET));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateNewPickup();
            Destroy(this.gameObject);
        }
    }

    public void CreateNewPickup()
    {
        float x_ViewPort = Random.Range(0.02f, 0.98f);
        float y_ViewPort = Random.Range(0.02f, 0.98f);

        Vector3 newPosition_ViewPort = new Vector3(x_ViewPort, y_ViewPort, 5);
        Vector3 newPosition_WorldPoint = Camera.main.ViewportToWorldPoint(newPosition_ViewPort);

        Instantiate(this.gameObject, newPosition_WorldPoint, Quaternion.identity);
    }
}
