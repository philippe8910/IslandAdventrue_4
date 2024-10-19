using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipComponent : MonoBehaviour
{   
    [SerializeField] private GameObject effectPrefab;

    [SerializeField] private Transform effectPos;

    [SerializeField] private float speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            var effect = Instantiate(effectPrefab , other.transform.position - Vector3.up * 3 , Quaternion.identity);

            other.gameObject.SetActive(false);

            Destroy(gameObject);
            //Destroy(other.gameObject);
            Destroy(effect , 3.0f);
        }
    }
}
