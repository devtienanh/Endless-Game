using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;      //vị trí z hiện tại nơi prefab tiếp theo sẽ được tạo ra
    public float tileLength = 30; //Độ dài tile = 30
    public int numberOfTile;  //Số lần tạo tile trên màn 
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    void Start()
    {
        for (int i = 0; i < numberOfTile; i++)
        {   //Đảm bảo tile mặc định đầu tiên không có chướng ngại vật
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    
    void Update()
    {
        //Check xem vtrí z của player có lớn hơn tổng chiều dài mà các prefab đã tạo sẵn không
        //nếu có thì sinh ra một prefab mới từ mảng tilePrefabs

        if (playerTransform.position.z -35 > zSpawn -(numberOfTile * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
    //Spawn các tile trong mảng tilePrefabs theo thời gian (timeSpawnTile)
    public void SpawnTile(int timeSpawnTile)
    {
        GameObject go = Instantiate(tilePrefabs[timeSpawnTile], transform.forward * zSpawn,transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    public void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
