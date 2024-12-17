using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodPrefabs;

    [SerializeField] private List<GameObject> spawnPoints;
    void Start()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Instantiate(RandomFood(), spawnPoints[i].transform.position, Quaternion.identity, transform);
        }
    }

    private GameObject RandomFood()
    {
        return foodPrefabs[Random.Range(0, foodPrefabs.Count)];
    }
}
