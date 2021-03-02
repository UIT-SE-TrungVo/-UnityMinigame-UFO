using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUFOBehaviour : MonoBehaviour
{
    public const float MAX_DISTANCE = 50;
    public const float SPEED = 20f;
    public Vector3 direction = new Vector3(0, 0, 0);

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        float x = 0, y = 0;
        while (x == 0 && y == 0)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
        }
        direction = new Vector3(x, y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTooFar())
        {
            Respawn();
        }
        else
        {
            this.transform.Translate(direction * SPEED * Time.deltaTime);
        }
    }

    bool isTooFar()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > MAX_DISTANCE) return true;
        else return false;
    }

    void Respawn()
    {
        float x = Random.Range(-0.2f, 1.2f);
        float y = Random.Range(-0.2f, 1.2f);
        int i = Random.Range(0, 1); //decide what side will be chosen, 0 mean choose horizontal, 1 mean choose vertical

        if (i == 0)
        {
            y = Random.Range(0, 1);
        }
        else
        {
            x = Random.Range(0, 1);
        }

        Vector3 newPosition_ViewPort = new Vector3(x, y, -5);
        Vector3 newPosition_WorldPoint = Camera.main.ViewportToWorldPoint(newPosition_ViewPort);

        this.transform.position = newPosition_WorldPoint;

        Vector3 randomPointInScreen = RandomPointInScreen();
        direction = (randomPointInScreen - this.transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Respawn();
        }
    }

    private Vector3 RandomPointInScreen()
    {
        float x_ViewPort = Random.Range(0.3f, 0.7f);
        float y_ViewPort = Random.Range(0.2f, 0.7f);

        Vector3 newPosition_ViewPort = new Vector3(x_ViewPort, y_ViewPort, 5);
        Vector3 newPosition_WorldPoint = Camera.main.ViewportToWorldPoint(newPosition_ViewPort);

        return newPosition_WorldPoint;
    }
}
