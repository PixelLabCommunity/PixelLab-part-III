using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private float vfxDestroyDelay = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 _moveDir;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        var playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            _moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        }
        else
        {
            _moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDir * (moveSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;
        var vfxInstance = Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(vfxInstance, vfxDestroyDelay);
        Destroy(gameObject);
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        var randomX = transform.position.x + Random.Range(-2f, 2f);
        var randomY = transform.position.y + Random.Range(-1f, 1f);

        var endPoint = new Vector2(randomX, randomY);

        var timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            var linearT = timePassed / popDuration;
            var heightT = animCurve.Evaluate(linearT);
            var height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
}