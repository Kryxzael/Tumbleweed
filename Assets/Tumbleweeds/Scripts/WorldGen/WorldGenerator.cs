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

    public LevelChunk[] Chunks = Array.Empty<LevelChunk>();

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
        LevelChunk chunk;

        do
        {
            chunk = Chunks[UnityEngine.Random.Range(0, Chunks.Length)];
        } while (chunk.PickChance < UnityEngine.Random.value);

        _loadedChunks.Enqueue(Instantiate(chunk, new Vector3(0, 0, NextSpawnPosition * CHUNK_SIZE), Quaternion.identity));
        NextSpawnPosition += chunk.SizeMultiplier;

        if (destroyOldestChunk)
            Destroy(_loadedChunks.Dequeue().gameObject);
    }
}