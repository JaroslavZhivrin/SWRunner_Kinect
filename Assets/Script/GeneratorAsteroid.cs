using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorAsteroid : MonoBehaviour {
    public bool easy = false;
    [SerializeField]
    private GameObject[] asteroidPrefab;
    private GameObject asteroids;
    [SerializeField]
    private float width = 4;
    [SerializeField]
    private float density = 0.4f;


    private void Awake()
    {
        asteroids = new GameObject("Asteroids");
        easy = RunnerInfo.easy;
        density = RunnerInfo.density;
    }

    private void Start()
    {
        
    }

    public void generate(float length, float paddingStart, float paddingEnd, Vector3 pos, Quaternion rotate) {
        GameObject astField = new GameObject("AstField");
        astField.transform.position = Vector3.zero;
        int count = (int)(density * length);
        float step = (float)width / 3;
        for (int i = 0; i < count; i++){
            GameObject ast = asteroidPrefab[Random.Range(0, asteroidPrefab.Length - 1)];
            Quaternion q = Quaternion.Euler(Random.Range(-15, 15), Random.Range(0, 360), Random.Range(-15, 15));
            if (!easy){
                Instantiate(ast, new Vector3(Random.Range(-width / 2, width / 2), Random.Range(-width / 2, width / 2), Random.Range(paddingStart, length - paddingEnd)), q, astField.transform);
            } else
            {
                Instantiate(ast, new Vector3(Random.Range(-1, 2) * step, 0, Random.Range(paddingStart, length - paddingEnd)), q, astField.transform);

            }
        } 
        astField.transform.position = pos;
        astField.transform.rotation = rotate;
        astField.transform.parent = asteroids.transform;
    }
}
