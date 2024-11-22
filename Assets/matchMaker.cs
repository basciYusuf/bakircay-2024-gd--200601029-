using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchMaker : MonoBehaviour
{
    public List<GameObject> PlacedObject = new List<GameObject>();
    public GameObject PointA;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlacedObject.Count == 0)
        {
            // Ýlk objeyi PointA konumuna taþý
            other.gameObject.transform.position = PointA.transform.position;
            other.gameObject.transform.rotation = PointA.transform.rotation;
            PlacedObject.Add(other.gameObject);
        }
        else
        {
            if (other.gameObject.name == PlacedObject[0].name)
            {
                Debug.Log("Same objects, destroying...");
                Destroy(other.gameObject);
                Destroy(PlacedObject[0]);
                PlacedObject.Clear();
            }
            else
            {
                Debug.Log("Different objects, pushing back...");
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 pushDirection = Vector3.forward;
                    rb.AddForce(pushDirection * 8f, ForceMode.Impulse); // Kuvvet uygula
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlacedObject.Contains(other.gameObject))
        {
            PlacedObject.Remove(other.gameObject);
        }
    }
}
