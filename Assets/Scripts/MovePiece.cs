using UnityEngine;

public class MovePiece : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _renderer;

    [SerializeField]
    private Rigidbody _rigidbody;

    private Color _materialColor; 

    private bool pieceGrabbed;        
    
    private void Start()
    {
        JengaManager.Instance.canMove = true;

        _materialColor = _renderer.material.color;
    }

    private void OnMouseEnter()
    {
        if (!JengaManager.Instance.canMove)
            return;

        if (JengaManager.Instance.canMove && !pieceGrabbed)
        {
            _renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _materialColor;
    }

    private void OnMouseDown()
    {
        if (!JengaManager.Instance.pieceSelected && JengaManager.Instance.canMove)
        {
            pieceGrabbed = true;
            JengaManager.Instance.pieceSelected = true;
        }
    }

    private void OnMouseUp()
    {
        if (pieceGrabbed)
        {

            transform.position = new Vector3(10000, 100000, 100000);

            _rigidbody.Sleep();

            Invoke("DestroyObject", 0.1f);

            JengaManager.Instance.pieceSelected = false;
        }
    }
    
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
