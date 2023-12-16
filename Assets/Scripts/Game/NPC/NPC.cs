using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class NPC : Agent
{
    [SerializeField] private Transform enemyTransform;
    [SerializeField] private SpriteRenderer npcSpriteRenderer;
    private Animator animator;

    //private Color originalColor;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //originalColor = npcSpriteRenderer.color;
        //for (int i = 0; i < 3; i++)
        //{
        //    InstantiateEnemy();
        //}
    }

    //public override void OnEpisodeBegin()
    //{
    //    transform.localPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    //    enemyTransform.localPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
    //    ResetColor();
    //}

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(enemyTransform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float moveSpeed = 5f;

        if (moveX * moveY != 0)
        {
            // Toc do di theo duong cheo
            transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed / Mathf.Sqrt(2);
        }
        else
        {
            // Toc do di theo duong thang
            transform.localPosition += new Vector3(moveX, moveY, 0) * Time.deltaTime * moveSpeed;
        }

        animator.SetFloat("Speed", new Vector3(moveX, moveY, 0).sqrMagnitude);

        if (moveX != 0)
        {
            if (moveX > 0)
                npcSpriteRenderer.transform.localScale = new Vector3(1, 1, 0);
            else
                npcSpriteRenderer.transform.localScale = new Vector3(-1, 1, 0);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyAI>(out EnemyAI enemy) ||
            other.TryGetComponent<BulletEnemy>(out BulletEnemy bulletEnemy))
        {
            SetReward(-1f);
            //EndEpisode();
            //ChangeColor(Color.red);
        }
    }

    //private void ChangeColor(Color color)
    //{
    //    if (npcSpriteRenderer != null)
    //    {
    //        npcSpriteRenderer.color = color;
    //    }
    //}

    //private void ResetColor()
    //{
    //    if (npcSpriteRenderer != null)
    //    {
    //        npcSpriteRenderer.color = originalColor;
    //    }
    //}

    //public void Reward()
    //{
        //SetReward(1f);
        //EndEpisode();
        //ChangeColor(Color.green);

        //InstantiateEnemy();
    //}

    //private void InstantiateEnemy()
    //{
    //    Instantiate(enemyTransform, new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0), Quaternion.identity);
    //}
}
