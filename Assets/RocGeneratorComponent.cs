using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RocGeneratorComponent : MonoBehaviour
{
    [SerializeField] private Transform rocParent;

    [SerializeField] private float gameTime;
    [SerializeField] private float timer;

    [SerializeField] private AnimationCurve levelHardCurve;
    [SerializeField] private List<GameObject> rocPrefabs = new List<GameObject>();
    [SerializeField] private List<Transform> rocSpawnPoints = new List<Transform>();

    [SerializeField] private UnityEvent OnGameTimeEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateRoc());

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
        //Debug.Log(levelHardCurve.Evaluate(Time.time / gameTime));
    }

    private IEnumerator GenerateRoc()
    {
        var rocPrefab = rocPrefabs[Random.Range(0, rocPrefabs.Count)];
        var roc = Instantiate(rocPrefab, rocSpawnPoints[Random.Range(0 , rocSpawnPoints.Count)].position, Quaternion.identity);
        
        roc.transform.parent = rocParent;

        Destroy(roc, 40.0f);

        yield return new WaitForSeconds(levelHardCurve.Evaluate(Time.time / gameTime));

        StartCoroutine(GenerateRoc());
    }
}
