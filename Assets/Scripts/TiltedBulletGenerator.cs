using UnityEngine;

/// <summary>
/// プレイヤーの向きに応じて傾く直線上に弾を配置して発射する機能
/// </summary>
public class TiltedBulletGenerator : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;    // 弾のプレハブ

    [SerializeField] private float bulletLine;       // 直線

    [SerializeField] private bool isDebugDrawRayOn;  // デバッグ用。傾いている直線の可視化。true なら描画

    [SerializeField] private float bulletSpeed;      // 弾の速度

    [SerializeField] private float duration;         // 弾の生存期間

    private Vector3 lineDirection;                   // プレイヤーの向いている方向を加味した方向ベクトル


    /// <summary>
    /// 弾の情報の設定
    /// </summary>
    /// <param name="bulletSpeed"></param>
    /// <param name="duration"></param>
    public void SetUpBulletData(float bulletSpeed, float duration) {
        this.bulletLine = bulletSpeed;
        this.duration = duration;
    }

    private void Update() {

        // デバッグ用の処理がオフなら処理しない
        if (!isDebugDrawRayOn) {
            return;
        }

        // 傾けた直線を描画
        Debug.DrawRay(transform.position, lineDirection, Color.red);
    }

    /// <summary>
    /// バレット生成準備
    /// </summary>
    /// <param name="direction">プレイヤーの向いている方向</param>
    /// <param name="level">生成する弾の数</param>
    public void PrepareGenerateBullet(Vector2 direction, int level = 1) {

        // レベルに応じて、直線の長さを調整する
        float currentBulletLine = bulletLine / 2 * level;

        // direction の情報を元に、プレイヤーの向きに対する角度（ラジアン）を求め(Mathf.Atan2(direction.y, direction.x))        // その情報を度数に変換し(* Mathf.Rad2Deg)、プレイヤーの向きに合わせて傾ける角度を求める
        // 角度の初期値は右90度(時計の針の3時)を 0 として始まるので、水平にするには 90 を足す
        float tilt = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        // Quaternion.Eulerを使い、プレイヤーの向きに合わせて傾けた回転角度を作成
        Quaternion rotation = Quaternion.Euler(0, 0, tilt);

        // 中心点(プレイヤーの位置)を基準に、プレイヤーの向きに応じて傾いた直線ベクトル(方向)を算出
        // rotation(傾けた回転角度) * Vector3.right(ワールド座標での水平方向。時計の３時の方向)
        lineDirection = rotation * Vector3.right;

        for (int i = 0; i < level; i++) {

            // 直線上の１つ辺りの角度の初期値
            float angle = 0;

            // レベル１以上なら角度間隔を調整する
            if (level > 1) {

                // 直線上の等間隔数の計算
                float t = (float)i / (level - 1);

                // 直線上に用意する弾の数で、各弾の角度間隔を算出
                angle = Mathf.Lerp(-currentBulletLine / 2, currentBulletLine / 2, t);
            }

            // angle * lineDirection をすることで、傾いた状態での直線上の等間隔の値が算出できる
            GenerateBullet(direction, angle * lineDirection, rotation);
        }
    }

    /// <summary>
    /// 弾の生成
    /// </summary>
    /// <param name="direction">弾を打ち出す方向/param>
    /// <param name="generatePos">弾の生成位置</param>
    /// <param name="rotation">弾の回転</param>
    private void GenerateBullet(Vector2 direction, Vector2 generatePos, Quaternion rotation) {

        // 弾の生成
        Bullet bullet = Instantiate(bulletPrefab, (Vector2)transform.position + generatePos, rotation);

        // 弾の発射命令
        bullet.Shoot(direction * bulletSpeed, duration);
    }
}
