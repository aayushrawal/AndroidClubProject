using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TILE_MANAGER : MonoBehaviour
{
    public GameObject[] tileprefabs;

    private Transform playerTransform;

    public float spwanZ = 20.0f;
    public float length = 20.0f;
    public int no_tiles = 7;
    private float safezone = 30.0f;
    private List<GameObject> active;
    private int lastprefb = 0;
    // Use this for initialization
    private void Start()
    {
        active = new List<GameObject>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < no_tiles; i++)
        {
            spawn();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform.position.z - safezone > (spwanZ - (no_tiles * length)))
        {
            spawn();
            delete();
        }
    }
    private void spawn(int prefabindex = -1)
    {
        GameObject go;
        go = Instantiate(tileprefabs[random()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spwanZ;
        spwanZ += length;
        active.Add(go);
    }
    private void delete()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
    public int random()
    {
        if (tileprefabs.Length <= 1)
            return 0;
        int randomindex = lastprefb;

        while (randomindex == lastprefb)
        {
            randomindex = Random.Range(0, tileprefabs.Length);

        }
        lastprefb = randomindex;
        return randomindex;

    }
}
