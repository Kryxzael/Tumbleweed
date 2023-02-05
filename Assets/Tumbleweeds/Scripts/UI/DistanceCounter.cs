using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DistanceCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Transform _player;

    public string Format = "{0:0}m";
    public float DistanceMultiplier = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _player = this.GetPlayer().transform;
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = string.Format(System.Globalization.CultureInfo.InvariantCulture, Format, _player.position.z * DistanceMultiplier);
    }
}
