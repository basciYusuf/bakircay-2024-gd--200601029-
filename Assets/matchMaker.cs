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
        // E�er hen�z bir obje yerle�tirilmemi�se
        if (PlacedObject.Count == 0)
        {
            // �lk objeyi PointA konumuna ta��
            other.gameObject.transform.position = PointA.transform.position;
            other.gameObject.transform.rotation = PointA.transform.rotation;
            PlacedObject.Add(other.gameObject);
        }
        else
        {
            // E�er gelen obje ayn� tag'e sahipse, e�le�ti olarak kabul et
            if (other.gameObject.name == PlacedObject[0].name)
            {
                Debug.Log("Same objects, destroying...");
                Destroy(other.gameObject); // Gelen objeyi yok et
                Destroy(PlacedObject[0]); // Yerle�ik olan objeyi yok et
                PlacedObject.Clear(); // Listeyi temizle
            }
            else
            {
                // E�er tag farkl�ysa objeyi geri it
                Debug.Log("Different objects, pushing back...");
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Objeyi geriye do�ru it (z ekseni boyunca geri itebiliriz)
                    Vector3 pushDirection = other.transform.position - PointA.transform.position;
                    rb.AddForce(pushDirection.normalized * 5f, ForceMode.Impulse); // Kuvvet uyguluyoruz
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
