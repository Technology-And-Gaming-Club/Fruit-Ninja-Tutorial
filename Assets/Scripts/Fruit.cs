using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : SliceableObject
{
    public override void OnSlice()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        gameManager.UpdateScore(score);
    }
}
