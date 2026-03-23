<h2>🚀 Cách dùng (rất sạch)</h2>

<h3>📦 Prefab của bạn</h3>

<pre><code class="language-csharp">
public class Bullet : MonoBehaviour, IPoolable
{
    public void OnSpawn()
    {
        Debug.Log("Spawn Bullet");
        thực hiện các logic ban đầu : kiểu như HPEnemy = 100;
    }

    public void OnDespawn()
    {
        Debug.Log("Despawn Bullet");
        ví dụ : dừng animation ...
    }
}
</code></pre>

<hr/>

<h3>🎯 Spawn</h3>

<pre><code class="language-csharp">
Bullet bullet = PoolManager.Spawn(bulletPrefab);
</code></pre>

<p>Hoặc spawn với position + rotation:</p>

<pre><code class="language-csharp">
Bullet bullet = PoolManager.Spawn(
    bulletPrefab,
    firePoint.position,
    Quaternion.identity
);
</code></pre>

<hr/>

<h3>♻️ Despawn</h3>

<pre><code class="language-csharp">
PoolManager.Despawn(bullet);
</code></pre>
