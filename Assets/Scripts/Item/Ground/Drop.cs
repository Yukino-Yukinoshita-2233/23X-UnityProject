using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    private List<GameObject> _GroundLists;
    private float _passedtime;
    private float _timespacing = 3f; // 时间间隔
    private void Start()
    {
        _GroundLists = GetComponent<Spawn>().GetGroundLists();
    }

    // Drop a ground
    private void Update()
    {
        if (_GroundLists != null)
        {
            float dt = Time.deltaTime;
            _passedtime += dt;
            if (_passedtime >= _timespacing)
            {
                int num = Random.Range(0, _GroundLists.Count);
                DestroyImmediate(_GroundLists[num]);
                _GroundLists.RemoveAt(num);
                Debug.Log("Drop a ground");
                _passedtime -= _timespacing;
            }
        }
    }
}
