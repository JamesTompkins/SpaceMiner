using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantStarCreator : MonoBehaviour {
    public int minStars, maxStars, distance;
    public GameObject starPrefab;


    void Start() {
        int number = Random.Range(minStars, maxStars);

        for (int i = 0; i < number; i++) {
            Vector3 pos = new Vector3 {
                x = Random.Range(-1f, 1f),
                y = Random.Range(-1f, 1f),
                z = Random.Range(-1f, 1f)
            };
            float normalization = 1 / Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y + pos.z * pos.z);
            pos *= normalization * distance;

            GameObject instance = Instantiate(starPrefab, pos, Quaternion.identity, transform);
            instance.GetComponent<LensFlare>().brightness = Random.Range(0f, 0.5f);
        }
    }
}
