using UnityEngine;

// 1. Enum State bawaan kamu
public enum StateZombie
{
    IDLE,
    PATROL,
    CHASE,
    ATTACK
}

// 2. Class Komponen untuk Menjalankan Logika 4 State Zombie
public class ZombieStateController : MonoBehaviour
{
    [Header("State Settings")]
    public StateZombie currentState = StateZombie.IDLE;

    [Header("Target & Detection")]
    public Transform playerTransform;      // Drag GameObject Player ke sini di Inspector
    public float detectionRange = 10f;     // Jarak zombie mulai ngejar player
    public float attackRange = 1.5f;       // Jarak zombie mulai nyerang player

    [Header("Movement Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public Transform[] patrolPoints;       // Titik-titik jalan patroli
    private int currentPatrolIndex = 0;

    private void Update()
    {
        // Otomatis cari player jika belum di-assign di Inspector
        if (playerTransform == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Jalankan logika sesuai State yang aktif
        switch (currentState)
        {
            case StateZombie.IDLE:
                ExecuteIdle();
                break;
            case StateZombie.PATROL:
                ExecutePatrol();
                break;
            case StateZombie.CHASE:
                ExecuteChase();
                break;
            case StateZombie.ATTACK:
                ExecuteAttack();
                break;
        }
    }

    // --- LOGIKA TIAP STATE ---

    // 1. IDLE: Diam dan memantau jarak Player
    private void ExecuteIdle()
    {
        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        // Kalau player masuk jarak deteksi, ganti ke CHASE
        if (distanceToPlayer <= detectionRange)
        {
            ChangeState(StateZombie.CHASE);
        }
    }

    // 2. PATROL: Jalan bolak-balik antar titik patroli
    private void ExecutePatrol()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= detectionRange)
            {
                ChangeState(StateZombie.CHASE);
                return;
            }
        }

        // Logika bergerak ke titik patroli
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Transform targetPoint = patrolPoints[currentPatrolIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }

    // 3. CHASE: Bergerak mendekati Player
    private void ExecuteChase()
    {
        if (playerTransform == null)
        {
            ChangeState(StateZombie.IDLE);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Jika player kabur keluar dari jarak deteksi, balik ke IDLE
        if (distanceToPlayer > detectionRange)
        {
            ChangeState(StateZombie.IDLE);
            return;
        }

        // Jika player sangat dekat, ganti ke ATTACK
        if (distanceToPlayer <= attackRange)
        {
            ChangeState(StateZombie.ATTACK);
            return;
        }

        // Logika mengejar posisi player
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
    }

    // 4. ATTACK: Menyerang Player saat dalam jarak serang
    private void ExecuteAttack()
    {
        if (playerTransform == null)
        {
            ChangeState(StateZombie.IDLE);
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Jika player menjauh dari jarak serang, balik mengejar (CHASE)
        if (distanceToPlayer > attackRange)
        {
            ChangeState(StateZombie.CHASE);
            return;
        }

        // Logika eksekusi serangan
        Debug.Log("[ZombieState] Zombie sedang menyerang Player!");
    }

    // Fungsi bantuan untuk mengganti state
    public void ChangeState(StateZombie newState)
    {
        if (currentState == newState) return;
        
        currentState = newState;
        Debug.Log($"[StateZombie] Berhasil ganti state ke: {currentState}");
    }
}