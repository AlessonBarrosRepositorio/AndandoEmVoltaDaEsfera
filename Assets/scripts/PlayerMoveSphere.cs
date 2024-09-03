using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSphere : MonoBehaviour
{

    public Transform planet; // Referência ao planeta (esfera)
    public float moveSpeed = 5f; // Velocidade de movimento
    public float rotationSpeed = 100f; // Velocidade de rotação
    public float gravity = 10f; // Gravidade em direção ao planeta
    public float groundDistance = 2.3f; // Distância para considerar o personagem no chão

    private Vector3 movement;
    private bool isGrounded;

    void Update()
    {
        // Captura as entradas do teclado
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula a direção em que o personagem deve se mover na superfície da esfera
        Vector3 directionToPlanet = (transform.position - planet.position).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, directionToPlanet).normalized;
        Vector3 forward = Vector3.Cross(directionToPlanet, right).normalized;

        // Movimenta o personagem na superfície da esfera
        movement = (right * moveX + forward * moveZ) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Aplica uma "gravidade" para manter o personagem na superfície
        Vector3 gravityDirection = (planet.position - transform.position).normalized;

        // Alinha o personagem para que ele sempre esteja "em pé" na superfície
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verifica se o personagem está no chão
        isGrounded = Physics.Raycast(transform.position, gravityDirection, groundDistance, LayerMask.GetMask("Ground"));

        // Implementa o pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(gravityDirection);
        }
    }

    private void Jump(Vector3 gravityDirection)
    {
        // Faz o personagem "saltar" da superfície
        transform.position -= gravityDirection * gravity * Time.deltaTime;
        isGrounded = false; // O personagem está no ar
    }
}

