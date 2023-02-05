using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameOverText : MonoBehaviour
{
    private PlayerSpeedController _player;
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Awake()
    {
        _player = this.GetPlayer().GetComponent<PlayerSpeedController>();
        _text = GetComponent<TextMeshProUGUI>();
        _text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GameOver)
        {
            _text.enabled = true;

            if (Input.anyKeyDown)
                SceneManager.LoadScene(0);
        }
    }
}
