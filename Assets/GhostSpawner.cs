using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public float spawnTimer = 1f; // Timer for spawning
    public GameObject prefabToSpawn; // The ghost prefab to spawn
    public float spawnRadius = 30f; // The radius within which ghosts can spawn

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnTimer)
        {
            spawnGhost();
            timer -= spawnTimer;
        }
    }

    // Spawns a ghost at a random position within the spawn radius
    public void spawnGhost()
    {
        // Generate a random position within a sphere around the spawner
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius * 2.5f;
        randomPosition.y = 10f; // Start above ground to ensure raycasting down

        // Raycast down from the random position to find the terrain or ground
        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit))
        {
            // Adjust the y position of the ghost to the surface of the terrain
            randomPosition.y = hit.point.y + 6.5f;
        }

        // Spawn the ghost at the calculated position with no rotation
        GameObject spawnedGhost = Instantiate(prefabToSpawn, randomPosition, Quaternion.Euler(-90, 0, 0));
        

        // Optionally, if you want to make sure the ghost stays grounded, you can use a script on the ghost to adjust its position during movement
        spawnedGhost.AddComponent<GroundLock>(); // Adding the GroundLock script for terrain locking
    }
}

public class GroundLock : MonoBehaviour
{
    private void Update()
    {
        // Raycast down to find the ground beneath the ghost every frame
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Lock the ghost's y position to the ground level
            Vector3 newPosition = transform.position;
            newPosition.y = hit.point.y +3.5f;
            transform.position = newPosition;
        }
    }
}