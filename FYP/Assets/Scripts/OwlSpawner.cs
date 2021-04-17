using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlSpawner : MonoBehaviour
{
    public float spawnRate = 5f;
    public GameObject owlPrefab;

    Transform player;

    bool inSpawnArea = false;
    // Spawn Positions
    int minIndex = 0;
    int maxIndex = 3;
    Vector2 sp1 = new Vector2(19.15f, 5f);
    Vector2 sp2 = new Vector2(34.61f, 5f);
    Vector2 sp3 = new Vector2(23.04f, 5f);

    Vector2 sp4 = new Vector2(32.15f, -10f);
    Vector2 sp5 = new Vector2(41.21f, -10f);
    Vector2 sp6 = new Vector2(40.01f, -2.04f);
    
    Vector2 sp7 = new Vector2(54.1f, 0.88f);
    Vector2 sp8 = new Vector2(40.8f, 4.05f);
    Vector2 sp9 = new Vector2(53.24f, -9.6f);

    List<Vector2> spList;
    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        spList = new List<Vector2>();
        LoadSpawnList();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {

        while (inSpawnArea) { 
        
            yield return StartCoroutine(SpawnOwls());

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPos();
    }
    
    void LoadSpawnList()
    {
        spList.Add(sp1);
        spList.Add(sp2);
        spList.Add(sp3);
        spList.Add(sp4);
        spList.Add(sp5);
        spList.Add(sp6);
        spList.Add(sp7);
        spList.Add(sp8);
        spList.Add(sp9);
    }

    void CheckPlayerPos()
    {
        if (player.position.x < 18.14)  return;
        else inSpawnArea = true;
        
        if (player.position.x >= 18.14f && player.position.x < 29.57f)
        {
            minIndex = 0;
            maxIndex = 3;
        }
        else if(player.position.x >= 29.57f && player.position.x < 39.79f)
        {
            minIndex = 3;
            maxIndex = 6;
        }
        else if (player.position.x >= 39.79f && player.position.x < 54.7f)
        {
            minIndex = 6;
            maxIndex = 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && inSpawnArea == false)
        {
            inSpawnArea = true;
            StartCoroutine(Start());
        }
    }

    IEnumerator SpawnOwls()
    {
        yield return new WaitForSeconds(spawnRate);
        
        GameObject owl = Instantiate(owlPrefab, spList[Random.Range(minIndex, maxIndex)], Quaternion.identity);
        owl.transform.parent = transform;
    }
}
