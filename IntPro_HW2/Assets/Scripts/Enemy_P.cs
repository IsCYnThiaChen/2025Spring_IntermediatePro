using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_P : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Patrolling,
        Chasing
    }

    public EnemyState currentState = EnemyState.Patrolling;
    private Transform player;

    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int numPatrolPoints = 3;
    [SerializeField] private Vector2 patrolRangeX = new Vector2(-10, 10);
    [SerializeField] private Vector2 patrolRangeZ = new Vector2(-10, 10);

    private Vector3[] patrolPoints;
    private int currentPoint = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateRandomPatrolPoints();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (distanceToPlayer < chaseRange)
                    currentState = EnemyState.Chasing;
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                break;
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPoint]) < 1.0f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
        Vector3 direction = (patrolPoints[currentPoint] - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * (speed * 1.5f) * Time.deltaTime;
    }

    void GenerateRandomPatrolPoints()
    {
        patrolPoints = new Vector3[numPatrolPoints];
        for (int i = 0; i < numPatrolPoints; i++)
        {
            float randomX = Random.Range(patrolRangeX.x, patrolRangeX.y);
            float randomZ = Random.Range(patrolRangeZ.x, patrolRangeZ.y);
            patrolPoints[i] = new Vector3(randomX, 0, randomZ);
        }
    }
}

