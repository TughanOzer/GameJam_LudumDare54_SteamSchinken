using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoiceLines : MonoBehaviour
{
    [SerializeField] private EventReference _idleVoiceLines;

    private float _timeTillVoiceLine;

    // Start is called before the first frame update
    void Start()
    {
        _timeTillVoiceLine = UnityEngine.Random.Range(20, 40);
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeTillVoiceLine > 0)
            _timeTillVoiceLine -= Time.deltaTime;
        else
            _timeTillVoiceLine = UnityEngine.Random.Range(20, 40);

        if (_timeTillVoiceLine <= 0)
            AudioManager.Instance.PlayOneShot(_idleVoiceLines, Player.Instance.transform.position);
    }
}
