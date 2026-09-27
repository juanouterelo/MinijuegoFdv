using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRaterPerMinute= 30f;
    public float spawnerRateIncrement= 1f;
    public float xlimit;
    public float maxTimeLife=4f;
    private float spawnNext = 0;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Time.time > spawnNext)
        {
            spawnNext= Time.time + 60 / spawnRaterPerMinute;
            spawnRaterPerMinute +=spawnerRateIncrement;
            Vector3 xleftLimit=Camera.main.ViewportToWorldPoint(new Vector3(0,1,0));
            Vector3 xrightLimit=Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
            float rand =Random.Range(xleftLimit.x,xrightLimit.x);
            Vector2 spawnPosition= new Vector2(rand,8f);
            GameObject meteor =Instantiate(asteroidPrefab,spawnPosition,Quaternion.identity);
            Destroy(meteor,maxTimeLife);
        }
    }
}
