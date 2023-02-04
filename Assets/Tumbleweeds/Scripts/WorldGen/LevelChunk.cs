using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class LevelChunk : MonoBehaviour
{
    [Range(0f, 1f)]
    public float PickChance = 1f;

    public int SizeMultiplier = 1;
}