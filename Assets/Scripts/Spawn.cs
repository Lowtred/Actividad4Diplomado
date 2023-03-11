using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemigo;
    public int posicionX;
    public int posicionZ;
    public int numEnemigos;
    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }

    IEnumerator SpawnEnemigos() 
    {
        while (numEnemigos < 10)
        {
            posicionX = Random.Range(1, 30);
            posicionZ = Random.Range(1, 30);
            Instantiate(enemigo, new Vector3(posicionX, 0, posicionZ), Quaternion.identity);
            yield return new WaitForSeconds(20f);
            numEnemigos += 1;
        }
    }
}
