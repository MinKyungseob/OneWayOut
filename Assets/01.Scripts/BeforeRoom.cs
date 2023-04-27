using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeRoom : MonoBehaviour
{
    public static bool trip = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trip = true;
            FindObjectOfType<GameManager>().BeforeStage();
        }
    }
}
