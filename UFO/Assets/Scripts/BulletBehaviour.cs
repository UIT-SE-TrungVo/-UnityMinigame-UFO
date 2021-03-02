using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public const float MAX_DISTANCE = 200;
    public const float SPEED = 100.0f;
    Vector3 direction;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTooFar()) Destroy(this.gameObject);
        this.transform.Translate(direction * Time.deltaTime * SPEED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) Destroy(this.gameObject);
    }

    private void GetDirection()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePoint - this.transform.position).normalized;
    }

    bool isTooFar()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > MAX_DISTANCE) return true;
        else return false;
    }
}
