using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipCollision : MonoBehaviour
{
    public GameObject partical;
    public int hp = 3;

    private void OnCollisionEnter(Collision collision)
    {
        var g = Instantiate(partical, collision.transform.position, collision.transform.rotation);

        Destroy(collision.gameObject);
        Destroy(g,2);

        hp--;

        if(hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
