using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSphere : MonoBehaviour
{

    public Transform planet; // Refer�ncia ao planeta (esfera)
    public float moveSpeed = 5f; // Velocidade de movimento
    public float rotationSpeed = 100f; // Velocidade de rota��o
    public float gravity = 10f; // Gravidade em dire��o ao planeta
    public float groundDistance = 2.3f; // Dist�ncia para considerar o personagem no ch�o

    private Vector3 movement;
    private bool isGrounded;

    void Update()
    {
        // Captura as entradas do teclado
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calcula a dire��o em que o personagem deve se mover na superf�cie da esfera
        Vector3 directionToPlanet = (transform.position - planet.position).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, directionToPlanet).normalized;
        Vector3 forward = Vector3.Cross(directionToPlanet, right).normalized;

        // Movimenta o personagem na superf�cie da esfera
        movement = (right * moveX + forward * moveZ) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        // Aplica uma "gravidade" para manter o personagem na superf�cie
        Vector3 gravityDirection = (planet.position - transform.position).normalized;

        // Alinha o personagem para que ele sempre esteja "em p�" na superf�cie
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Verifica se o personagem est� no ch�o
        isGrounded = Physics.Raycast(transform.position, gravityDirection, groundDistance, LayerMask.GetMask("Ground"));

        // Implementa o pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(gravityDirection);
        }
    }

    private void Jump(Vector3 gravityDirection)
    {
        // Faz o personagem "saltar" da superf�cie
        transform.position -= gravityDirection * gravity * Time.deltaTime;
        isGrounded = false; // O personagem est� no ar
    }
}

