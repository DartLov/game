using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounce : MonoBehaviour
{
    public Vector3 startPos;
        void OnTriggerEnter (Collider other)
        {
            startPos = new Vector3(Random.Range(-2500, 2500), Random.Range(-1, 2500), Random.Range(-2500, 2500));
            other.transform.position = startPos;
        }
}