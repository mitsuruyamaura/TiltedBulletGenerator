using UnityEngine;

/// <summary>
/// 弾の制御用クラス
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 発射
    /// </summary>
    /// <param name="direction">弾を発射する方向と力</param>
    /// <param name="duration">弾の生存期間</param>
    public void Shoot(Vector2 direction, float duration) {

        // Rigidbody2D コンポーネントの取得をおこない、取得できたら
        if (TryGetComponent(out Rigidbody2D rb)) {
            
            // 弾を発射し、生存期間後に破壊
            rb.AddForce(direction, ForceMode2D.Impulse);
            Destroy(gameObject, duration);
        }
    }
}