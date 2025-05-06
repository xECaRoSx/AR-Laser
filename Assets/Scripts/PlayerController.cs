using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 200;
        }
    }
}
