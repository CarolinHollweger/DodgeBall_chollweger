
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{

    class PoolObject
    {
        public Transform transform;
        public bool inUse;
        public PoolObject(Transform t) { transform = t; }
        public void Use() { inUse = true; }
        public void Dispose() { inUse = false; }
    }

    [System.Serializable]
    public struct XSpawnRange
    {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public XSpawnRange xSpawnRange;
    public Vector3 defaultSpawnPos;
    public bool spawnImmediate; //particle prewarm
    public Vector3 immediateSpawnPos;
    public Vector2 targetAscpectRatio;

    float spawnTimer;
    float targetAspect;

    PoolObject[] poolObjects;

    private void OnGameOverConfirmed()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].Dispose();
            poolObjects[i].transform.position = Vector3.one * 1000;
        }

        if (spawnImmediate)
        {
            SpawnImmediate();
        }
    }


    private void Configure()
    {
        targetAspect = targetAscpectRatio.x / targetAscpectRatio.x;
        poolObjects = new PoolObject[poolSize];
        for (int i = 0; i < poolObjects.Length; i++)
        {
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjects[i] = new PoolObject(t);
        }

        if (spawnImmediate)
        {
            SpawnImmediate();
        }
    }

    private void Spawn()
    {
        Transform t = GetPoolObject();
        if (t == null) return; //if true, this indicates that poolSize is too small
        Vector3 pos = Vector3.zero;
        pos.y = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.x = Random.Range(xSpawnRange.min, xSpawnRange.max);
        t.position = pos;
    }

    private void SpawnImmediate()
    {
        Transform t = GetPoolObject();
        if (t == null) return; //if true, this indicates that poolSize is too small
        Vector3 pos = Vector3.zero;
        pos.y = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.x = Random.Range(xSpawnRange.min, xSpawnRange.max);
        t.position = pos;
    }

    private void Shift()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].transform.position += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]);
        }
    }

    private void CheckDisposeObject(PoolObject poolObject)
    {
        if (poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect) / targetAspect)
        {
            poolObject.Dispose();
            poolObject.transform.position = Vector3.one * 1000;
        }
    }

    private Transform GetPoolObject()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (!poolObjects[i].inUse)
            {
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }
        return null;
    }



}