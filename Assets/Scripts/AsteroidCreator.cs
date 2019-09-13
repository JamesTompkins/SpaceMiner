using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCreator : MonoBehaviour {
    
    public int minPerCluster, maxPerCluster, clusters, clusterRadius, mapRadius;
    public GameObject[] asteroidPrefab;
    void Start() {
        CreateAsteroids();
    }

    public void CreateAsteroids() {
        //tracks number of asteroids created
        int counter = 0;

        for (int i = 0; i < clusters; i++) {
            //asteroids come in clusters centered around a point
            int clusterX = Random.Range(-mapRadius, mapRadius);
            int clusterZ = Random.Range(-mapRadius, mapRadius);
            int number = Random.Range(minPerCluster, maxPerCluster);

            for (int j = 0; j < number; j++) {
                int asteroidX = Random.Range(-clusterRadius, clusterRadius) + clusterX;
                int asteroidZ = Random.Range(-clusterRadius, clusterRadius) + clusterZ;
                Vector3 position = new Vector3(asteroidX, 0, asteroidZ);

                GameObject prefab = asteroidPrefab[Random.Range(0, asteroidPrefab.Length)];
                GameObject newAsteroid = Instantiate(prefab, position, Quaternion.identity, transform);
                ++counter;
            }
        }
        //Debug.Log(counter + " Asteroids created");
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        float size = mapRadius * 2 + clusterRadius;
        Gizmos.DrawWireCube(transform.position, new Vector3(size, 0, size));
    }
}
