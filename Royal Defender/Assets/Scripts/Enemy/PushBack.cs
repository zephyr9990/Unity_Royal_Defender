using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    private float pushBackTime;
    private bool _pushBack;
    private float _timer;
    private Rigidbody _rBody;
 
    // Start is called before the first frame update
    void Start()
    {
        pushBackTime = .25f;
        _pushBack = false;
        _timer = 0f;
        _rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_pushBack && _timer >= pushBackTime)
        {
            _pushBack = false;
            _rBody.isKinematic = true;
        }
    }

    public void AddPushBack(Vector3 direction, float force)
    {
        _timer = 0;
        _pushBack = true;
        _rBody.isKinematic = false;
        _rBody.AddForce(direction * force, ForceMode.Impulse);
    }
}
