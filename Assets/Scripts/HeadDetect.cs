using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JengaPiece")
            JengaManager.Instance.GameOver();

    }
}
