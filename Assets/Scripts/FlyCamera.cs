using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _gameObjectToRotate;

    [SerializeField]
    public float _sensitivity;

    private GameObject _pivot;

    private void Start()
    {
        _pivot = new GameObject("pivot");
        _pivot.transform.position = _gameObjectToRotate.position;

        transform.SetParent(_pivot.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
#if UNITY_EDITOR
            _pivot.transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime, 0);
#endif
        }
    }
}
