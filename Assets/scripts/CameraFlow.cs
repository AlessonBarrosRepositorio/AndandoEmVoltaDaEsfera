using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform player; // Referência ao personagem
    public Vector3 offset; // Deslocamento da câmera em relação ao personagem
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento
    public float minDistanceFromGround = 2f; // Distância mínima permitida entre a câmera e o chão

    void LateUpdate()
    {
        // Calcula a posição desejada da câmera
        Vector3 desiredPosition = player.position + offset;

        // Suaviza a transição para a nova posição
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Verifica a distância entre a câmera e o chão usando um Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit))
        {
            float distanceToGround = hit.distance;

            // Se a distância ao chão for menor que a distância mínima, ajuste a posição da câmera
            if (distanceToGround < minDistanceFromGround)
            {
                smoothedPosition += transform.up * (minDistanceFromGround - distanceToGround);
            }
        }

        // Aplica a posição suavizada
        transform.position = smoothedPosition;

        // Faz a câmera olhar para o personagem
        transform.LookAt(player);
    }
}
