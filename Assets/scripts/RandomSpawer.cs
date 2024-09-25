using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawer : MonoBehaviour
{
    public GameObject starPrefab; // The star prefab to spawn
    public int starsToSpawnAtStart = 3; // Number of stars to spawn at the start
    public float spawnInterval = 2f; // Time between spawns

    [Header("Randomization Settings")]
    public Vector2 spawnRangeX = new Vector2(-10f, 10f); // X-axis range for random position
    public Vector2 spawnRangeY = new Vector2(-5f, 5f);   // Y-axis range for random position
    public Vector2 scaleRange = new Vector2(0.5f, 2f);   // Min/Max scale range

    void Start()
    {
        // Start auto-spawning stars after a delay
        StartCoroutine(SpawnStarsPeriodically());
    }

    // Coroutine to auto-spawn stars at intervals
    IEnumerator SpawnStarsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomStar();
        }
    }

    // Function to spawn a star with random position and scale
    void SpawnRandomStar()
    {
        // Generate random position within the given range
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0);

        // Instantiate the star at the random position
        GameObject newStar = Instantiate(starPrefab, randomPosition, Quaternion.identity);

        // Randomize the scale of the star
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        newStar.transform.localScale = new Vector3(randomScale, randomScale, 1);
        Destroy(newStar, 1f);
    }
}
