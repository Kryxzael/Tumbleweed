using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private const int CHUNK_SIZE = 10;
    private const int INITIAL_SPAWN_COUNT = 10;
    private const float NEXT_SPAWN_THRESHOLD = 50f;

    private int _chunksSinceLastSpawn = 0;
    public float MaxChunksWithoutSpawn = 3;
    public float[] SpawnChances = Array.Empty<float>(); //Item n is the chance of n enemies spawning in a chunk


    public LevelChunk[] Chunks = Array.Empty<LevelChunk>();
    public Crashable[] Crashables = Array.Empty<Crashable>();

    public int NextSpawnPosition = 0;
    private Queue<LevelChunk> _loadedChunks = new Queue<LevelChunk>();
    GameObject _player;


    private void Start()
    {
        if ((Chunks?.Length ?? 0) == 0)
            throw new NullReferenceException("No chunks registered");

        _player = this.GetPlayer();

        for (int i = 0; i < INITIAL_SPAWN_COUNT; i++)
            SpawnNext(false);
    }

    private void Update()
    {
        if (NextSpawnPosition * CHUNK_SIZE - _player.transform.position.z < NEXT_SPAWN_THRESHOLD)
            SpawnNext(true);
    }


    public void SpawnNext(bool destroyOldestChunk)
    {
        //Select and spawn the chunk
        LevelChunk chunk;

        do
        {
            chunk = Chunks[UnityEngine.Random.Range(0, Chunks.Length)];
        } while (chunk.PickChance < UnityEngine.Random.value);

        LevelChunk spawned = Instantiate(chunk, new Vector3(0, 0, NextSpawnPosition * CHUNK_SIZE), Quaternion.identity);
        _loadedChunks.Enqueue(spawned);

        //Spawn crashables
        int enemySpawnCount;

        do
        {
            enemySpawnCount = UnityEngine.Random.Range(0, SpawnChances.Length);
        } while (SpawnChances[enemySpawnCount] < UnityEngine.Random.value || (enemySpawnCount == 0 && _chunksSinceLastSpawn >= MaxChunksWithoutSpawn));

        if (enemySpawnCount == 0)
            _chunksSinceLastSpawn++;

        else
            _chunksSinceLastSpawn = 0;

        foreach (CrashableSpawner i in spawned.GetComponentsInChildren<CrashableSpawner>().OrderBy(i => UnityEngine.Random.value).Take(enemySpawnCount))
        {
            Crashable crashable = Crashables[UnityEngine.Random.Range(0, Crashables.Length)];
            Instantiate(crashable, i.transform.position, Quaternion.identity, spawned.transform);
        }

        //Cleanup
        NextSpawnPosition += chunk.SizeMultiplier;

        if (destroyOldestChunk)
            Destroy(_loadedChunks.Dequeue().gameObject);
    }
}