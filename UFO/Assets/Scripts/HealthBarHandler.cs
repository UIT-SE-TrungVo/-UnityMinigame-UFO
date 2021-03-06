using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    private static Image HealthBarImage;
    GameObject player;

    void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthBarImage = GetComponent<Image>();
        SetHealthBarValue(1.0f);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float playerHealthPercent = Mathf.Max(0, player.GetComponent<DamageMechanicBehaviour>().GetHealthPoint() / player.GetComponent<DamageMechanicBehaviour>().START_HEALTH);
        SetHealthBarValue(playerHealthPercent);
    }
}
