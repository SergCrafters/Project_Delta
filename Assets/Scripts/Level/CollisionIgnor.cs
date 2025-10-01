using UnityEngine;

public class CollisionIgnor : MonoBehaviour
{
    public float ignoreCollisionSpeed = 5f;
    public string playerTag = "Player";
    public LayerMask ignoreLayer;
    public LayerMask defaultLayer;

    private int ignoreLayerIndex;
    private int defaultLayerIndex;

    private void Start()
    {
        ignoreLayerIndex = LayerMaskToLayerIndex(ignoreLayer);
        defaultLayerIndex = LayerMaskToLayerIndex(defaultLayer);
    }

    private int LayerMaskToLayerIndex(LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // ������� ������� Time.fixedDeltaTime �� ��������
                float actualSpeed = playerRb.linearVelocity.magnitude;
                Debug.Log($"�������� ��������: {actualSpeed}, ���������: {ignoreCollisionSpeed}");

                if (actualSpeed >= ignoreCollisionSpeed)
                {
                    gameObject.layer = ignoreLayerIndex;
                    Debug.Log("���������� �������� - �����!");
                }
                else
                {
                    gameObject.layer = defaultLayerIndex;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // ���������� ������� ����, ����� ����� ������ �� ��������
            gameObject.layer = defaultLayerIndex;
        }
    }
}
