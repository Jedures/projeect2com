using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTouching : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            JengaManager.Instance.GameOver();
        }
    }
}
