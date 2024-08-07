using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ShipGeneratorComponent : MonoBehaviour
{
    [SerializeField] private Transform shipParent;

    [SerializeField] private float gameTime;
    [SerializeField] private float timer = 0;

    [SerializeField] private AnimationCurve levelHardCurve;
    [SerializeField] private List<GameObject> shipPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> shipSpawnPoints = new List<Transform>();


    [SerializeField] private UnityEvent OnGameTimeEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateShip());

        OnGameTimeEnd.AddListener(() =>
        {
            StartCoroutine(NextStage());

            Debug.Log("Next Stage");

            IEnumerator NextStage()
            {
                yield return new WaitForSeconds(3.0f);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        });
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= gameTime)
        {
            OnGameTimeEnd.Invoke();
            timer = 0;
        }
    }

    private IEnumerator GenerateShip()
    {
        var shipPrefab = shipPrefabs[Random.Range(0, shipPrefabs.Count)];
        var ship = Instantiate(shipPrefab, shipSpawnPoints[Random.Range(0 , shipSpawnPoints.Count)].position, Quaternion.identity);
        
        ship.transform.parent = shipParent;

        yield return new WaitForSeconds(levelHardCurve.Evaluate(Time.time / gameTime));

        StartCoroutine(GenerateShip());
    }
}
