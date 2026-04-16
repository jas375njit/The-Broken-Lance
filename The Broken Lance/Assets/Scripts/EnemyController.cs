using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4f;
    public float strikeRange = 3f;
    public float groundOffset = 1f;

    private Animator animator;
    private bool hasStruck = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (player == null)
            Debug.LogError("No player assigned on " + gameObject.name);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > strikeRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;

            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (animator != null) animator.SetBool("IsStriking", false);
        }
        else
        {
            if (!hasStruck)
            {
                if (animator != null) animator.SetBool("IsStriking", true);
                hasStruck = true;
            }
        }

        int groundLayer = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 20f, groundLayer))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y + groundOffset;
            transform.position = pos;
        }
    }
}