using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateLevel : MonoBehaviour
{
    public GameObject wallPrefab;         
    public GameObject specialObstaclePrefab;
    public GameObject coinPrefab;         
    public GameObject boundaryPrefab;     
    public GameObject playerPrefab;       

    public Transform levelParent;         
    public int wallCount = 10;           
    public int coinCount = 5;            
    public float characterSize = 1.0f;    
    public float minWallDistance = 1.5f;  
    public float minCoinDistance = 1.0f;  
    public float minSpawnY = -4.0f;       
    public float maxSpawnY = 4.0f;        
    public int maxAttempts = 100;        

    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;
    private List<Vector2> obstaclePositions = new List<Vector2>(); 
    private List<Vector2> coinPositions = new List<Vector2>();
    void Start()
    {
        mainCamera = Camera.main;
        CalculateCameraBounds();
        LevelGenerator();
    }
   
    void CalculateCameraBounds()
    {
        cameraHeight = 2f * mainCamera.orthographicSize; 
        cameraWidth = cameraHeight * mainCamera.aspect;  
    }

    void LevelGenerator()
    {       
        GameObject boundaries = GenerateBoundary();
        
        for (int i = 0; i < wallCount; i++)
        {
            Vector2 randomPosition = GetRandomPositionWithinBounds();
            int attempts = 0; 
           
            while (!CanPlaceObstacle(randomPosition) && attempts < maxAttempts)
            {
                randomPosition = GetRandomPositionWithinBounds();
                attempts++;
            }

            if (attempts < maxAttempts) 
            {
                obstaclePositions.Add(randomPosition); 
                
                Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
               
                GameObject obstacleToSpawn = Random.Range(0f, 1f) < 0.2f ? specialObstaclePrefab : wallPrefab; // 20%
                Instantiate(obstacleToSpawn, randomPosition, randomRotation, levelParent);
            }
        }
        
        for (int i = 0; i < coinCount; i++)
        {
            Vector2 randomPosition = GetRandomPositionWithinBounds();
            int attempts = 0; 
           
            while ((IsPositionTooCloseToObstacle(randomPosition) || IsPositionTooCloseToCoin(randomPosition)) && attempts < maxAttempts)
            {
                randomPosition = GetRandomPositionWithinBounds();
                attempts++;
            }

            if (attempts < maxAttempts)  
            {
                coinPositions.Add(randomPosition); 
                Instantiate(coinPrefab, randomPosition, Quaternion.identity, levelParent);
            }
        }
    }

    bool IsPositionTooCloseToCoin(Vector2 position)
    {
        foreach (Vector2 coinPosition in coinPositions)
        {
            if (Vector2.Distance(position, coinPosition) < minCoinDistance)
            {
                return true; 
            }
        }

        return false;
    }
   
    GameObject GenerateBoundary()
    {        
        Vector2 boundaryPosition = Vector2.zero;
        return Instantiate(boundaryPrefab, boundaryPosition, Quaternion.identity, levelParent);
    }
    Vector2 GetRandomPositionWithinBounds()
    {
        float minX = -cameraWidth / 2;
        float maxX = cameraWidth / 2;
        float minY = Mathf.Clamp(minSpawnY, -cameraHeight / 2, cameraHeight / 2); 
        float maxY = Mathf.Clamp(maxSpawnY, -cameraHeight / 2, cameraHeight / 2);  

        return new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
    }    
    bool CanPlaceObstacle(Vector2 position)
    {
        foreach (Vector2 obstaclePosition in obstaclePositions)
        {
            if (Vector2.Distance(position, obstaclePosition) < minWallDistance)
            {
                return false; 
            }
        }
        return true;
    }
   
    bool IsPositionTooCloseToObstacle(Vector2 position)
    {
        foreach (Vector2 obstaclePosition in obstaclePositions)
        {
            if (Vector2.Distance(position, obstaclePosition) < minCoinDistance)
            {
                return true; 
            }
        }
        return false;
    }
}
