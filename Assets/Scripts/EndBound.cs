using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBound : MonoBehaviour
{
    private static ClassicGameManager gameManager;

    private void Start()
    {
        gameManager = ClassicGameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.DestroyOutBounds(other.gameObject);
    }
}
