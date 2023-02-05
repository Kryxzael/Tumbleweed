using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Transform Target;
    public float Y;

    private void LateUpdate()
    {
        transform.position = Target.position.SetY(Y);
    }
}
