using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour {
    private Transform player;
    private EnvGenerate env;

    public GameObject[] obstacles;
    private WayPoints way;
    public float startPoint = 100f;
    public float minGenerateRange = 100f;
    public float maxGenerateRange = 200f;

    private int currentIndex;
    public static Vector3 position;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.player).transform;
        env = GameObject.FindWithTag(Tags.gameController).GetComponent<EnvGenerate>();
        way = GetComponentInChildren<WayPoints>();
        currentIndex = way.points.Length - 2;
    }

    private void Start()
    {
        GenerateObs();
    }

    // Update is called once per frame
    void Update () {

	}

    private void GenerateObs()
    {
        float startZ = transform.position.z - 3000f + startPoint;
        float endZ = transform.position.z;
        while (startZ <endZ )
        {
            GameObject ga= Instantiate(obstacles[Random.Range(0, obstacles.Length)], GetPointFromZ(startZ), Quaternion.identity);
            ga.transform.parent = this.transform;
            startZ += Random.Range(minGenerateRange, maxGenerateRange);
        }
    }

    private Vector3 GetPointFromZ(float z)
    {
        int index = 0;
        for(int i=0;i<way .points .Length -1;++i)
        {
            if(z<way .points [i].position .z&&z>way.points [i+1].position .z )
            {
                index = i;
                break;
            }
        }

        return Vector3.Lerp(way.points[index + 1].position,
                              way.points[index].position,
                              (z - way.points[index + 1].position.z) / (way.points[index].position.z - way.points[index + 1].position.z));
    }

    public Vector3 GetNextTargetPoint()
    {
        while (true)
        {
            if (way.points[currentIndex].position.z - player.position.z < 10f)
            {
                --currentIndex;
                if (currentIndex < 0)
                {
                    env.Generate();
                    Destroy(gameObject,3f);
                    return env.forest1.GetNextTargetPoint();
                }
            }
            else
                return way.points[currentIndex].position;
        }
    }
}
