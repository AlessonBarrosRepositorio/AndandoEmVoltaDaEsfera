using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform player; // Refer�ncia ao personagem
    public Vector3 offset; // Deslocamento da c�mera em rela��o ao personagem
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o do movimento
    public float minDistanceFromGround = 2f; // Dist�ncia m�nima permitida entre a c�mera e o ch�o

    void LateUpdate()
    {
        // Calcula a posi��o desejada da c�mera
        Vector3 desiredPosition = player.position + offset;

        // Suaviza a transi��o para a nova posi��o
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Verifica a dist�ncia entre a c�mera e o ch�o usando um Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            float distanceToGround = hit.distance;

            // Se a dist�ncia ao ch�o for menor que a dist�ncia m�nima, ajuste a posi��o da c�mera
            if (distanceToGround < minDistanceFromGround)
            {
                smoothedPosition += transform.up * (minDistanceFromGround - distanceToGround);
            }
        }

        // Aplica a posi��o suavizada
        transform.position = smoothedPosition;

        // Faz a c�mera olhar para o personagem
        transform.LookAt(player);
    }
}
