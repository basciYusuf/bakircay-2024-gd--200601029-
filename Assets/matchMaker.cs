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
        // Eðer henüz bir obje yerleþtirilmemiþse
        if (PlacedObject.Count == 0)
        {
            // Ýlk objeyi PointA konumuna taþý
            other.gameObject.transform.position = PointA.transform.position;
            other.gameObject.transform.rotation = PointA.transform.rotation;
            PlacedObject.Add(other.gameObject);
        }
        else
        {
            // Eðer gelen obje ayný tag'e sahipse, eþleþti olarak kabul et
            if (other.gameObject.name == PlacedObject[0].name)
            {
                Debug.Log("Same objects, destroying...");
                Destroy(other.gameObject); // Gelen objeyi yok et
                Destroy(PlacedObject[0]); // Yerleþik olan objeyi yok et
                PlacedObject.Clear(); // Listeyi temizle
            }
            else
            {
                // Eðer tag farklýysa objeyi geri it
                Debug.Log("Different objects, pushing back...");
                Rigidbody rb = other.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Objeyi geriye doðru it (z ekseni boyunca geri itebiliriz)
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
