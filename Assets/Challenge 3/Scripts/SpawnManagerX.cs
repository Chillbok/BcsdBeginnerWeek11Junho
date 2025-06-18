using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    private float spawnDelay = 2f; //float 대입을 위해 숫자 뒤에 f 입력
    private float spawnInterval = 1.5f;

    private PlayerControllerX playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PrawnsObject", spawnDelay, spawnInterval);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerControllerX>();
        StartCoroutine(SpawnObjectsCoroutine());
    }

    // Spawn obstacles
    void SpawnObjects()
    {
        // Set random spawn location and random object index
        Vector3 spawnLocation = new Vector3(30, Random.Range(5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);

        // If game is still active, spawn new object
        if (!playerControllerScript.gameOver)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }

    IEnumerator SpawnObjectsCoroutine()
    {
        //최초 지연시간만큼 기다리기
        yield return new WaitForSeconds(spawnDelay);

        //무한 루프 돌면서 오브젝트 계속 생성
        while (true)
        {
            //오브젝트 생성
            SpawnObjects();

            //다음 오브젝트 생성까지 spawnDelay만큼 기다리기
            yield return new WaitForSeconds(spawnInterval);

            //게임오버 되면 코루틴 중지
            if (playerControllerScript.gameOver)
            {
                yield break; //코루틴 종료
            }
        }
    }
}
