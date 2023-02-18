using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    private PlayerController _playerController;
    [SerializeField] private float _moveSpeed;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Running)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerController.transform.position, _moveSpeed * Time.deltaTime);
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<PlayerController>().Kill("Ghost");
        }
    }
}
