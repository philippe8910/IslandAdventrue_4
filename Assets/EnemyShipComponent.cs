using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipComponent : MonoBehaviour
{   
    [SerializeField] private GameObject effectPrefab;
    public GameObject effectPrefab2;

    [SerializeField] private Transform effectPos;

    [SerializeField] public float speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void delay()
    {
        var effect = Instantiate(effectPrefab, transform.position - Vector3.up * 3, Quaternion.identity);

        Destroy(gameObject);
        Destroy(effect, 3.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            var effect = Instantiate(effectPrefab , other.transform.position - Vector3.up * 3 , Quaternion.identity);

            other.gameObject.SetActive(false);
            effectPrefab2.SetActive(true);
            //Destroy(other.gameObject);
            
            GetComponent<BoxCollider>().enabled = false;
            
            Destroy(effect , 3.0f);
            Invoke("delay" , 1.5f);
        }
    }
}
