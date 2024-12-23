using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeVision : MonoBehaviour
{
    [SerializeField] private EnemiesMovement enemiesMovement;
    public Color meshColor = Color.red;
    Mesh mesh;
    public float angle= 30;
    public float distance= 10;
    public float height= 1.0f;
    private float smoothSpeed = 1.0f;
    
    private float timeCounter = 0f;  // Contador de tiempo


    private Vector3 lastKnownPosition; // Última posición conocida del jugador


    void Update()
    {
        rotationMesh();
    }


    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        // Calcula el número de triángulos (6 para 2D: base y lados)
        int numTriangles = 3; 
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles * 3];

        // Define los vértices en 2D
        Vector3 origin = Vector3.zero; // Centro del cono
        Vector3 leftDirection = Quaternion.Euler(0, 0, -angle) * Vector3.up * distance; // Extremo izquierdo
        Vector3 rightDirection = Quaternion.Euler(0, 0, angle) * Vector3.up * distance; // Extremo derecho

        // Vértices del cono
        vertices[0] = origin;
        vertices[1] = leftDirection;
        vertices[2] = rightDirection;

        // Define los triángulos
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Asigna los vértices y triángulos al mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            enemiesMovement.isChasing = true;
            enemiesMovement.isReturningToPatrol = false;
            enemiesMovement.speed = 4f; 

            Debug.Log("Jugador detectado: comienza la persecución.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            lastKnownPosition = other.transform.position;
            enemiesMovement.isChasing = false;

           
            StartCoroutine(CheckLastKnownPosition());

            Debug.Log("Jugador fuera del rango: persigue última posición.");
        }
    }

    private IEnumerator CheckLastKnownPosition()
    {
        
        enemiesMovement.MoveToLastPosition(lastKnownPosition);
        

       
        yield return new WaitUntil(() => enemiesMovement.ReachedLastPosition());

      
        enemiesMovement.isReturningToPatrol = true;

        Debug.Log("Jugador no encontrado: regresando a patrullar.");
    }

    void rotationMesh()
    {
        float startAngleR = 85.0f;
        float endAngleR = 95.0f;
        float startAngleF = 265.0f;
        float endAngleF = 275.0f;
        float startAngleU = 175.0f;
        float endAngleU = 185.0f;
        float startAngleD = 355.0f;
        float endAngleD = 365.0f;
        

        // Interpolar el ángulo Z suavemente
        timeCounter += Time.deltaTime * smoothSpeed;

    // Generar un ángulo oscilante con Mathf.PingPong y el contador ajustado
        float angleZR = Mathf.Lerp(startAngleR, endAngleR, Mathf.PingPong(timeCounter, 1));
        float angleZL = Mathf.Lerp(startAngleF, endAngleF, Mathf.PingPong(timeCounter, 1));
        float angleZU = Mathf.Lerp(startAngleU, endAngleU, Mathf.PingPong(timeCounter, 1));
        float angleZD = Mathf.Lerp(startAngleD, endAngleD, Mathf.PingPong(timeCounter, 1));

        // Aplicar la rotación en el eje Z
        if(enemiesMovement.movingDirection == "right")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angleZR);

        }else if (enemiesMovement.movingDirection == "left")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angleZL);

        }else if(enemiesMovement.movingDirection == "up")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angleZU);
        }else if(enemiesMovement.movingDirection == "down")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angleZD);
        }
        

    }
}
