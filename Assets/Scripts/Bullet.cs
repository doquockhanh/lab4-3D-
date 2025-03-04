using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Transform target;

    public GameObject explosionPrefab; // Thêm prefab của explosion

    public void SetTarget(GameObject enemy)
    {
        target = enemy.transform;
    }

    void Update()
    {
        if (target == null)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            target.GetComponent<EnemyHealth>().TakeDamage(damage);

            // Gọi hiệu ứng explosion tại vị trí va chạm
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            BulletPool.Instance.ReturnBullet(gameObject); // Hủy viên đạn
        }
    }
}
