using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding;

public class zombieController : MonoBehaviour
{
    //public float speed = 5f; // Speed at which the zombie moves
    public float detectionRange = 10f; // Distance within which the zombie detects the player
    public float snarlRange = 5f;
    //[SerializeField] AudioSource snarlingSound;
    private AudioSource snarlingSound;

    [SerializeField] GameObject player; // To store the player GameObject
    private Rigidbody2D rb; // Rigidbody component of the zombie

    //Pathfinding stuff
    [Header("Navigator options")]
    [SerializeField] float gridSize = 0.5f; //increase patience or gridSize for larger maps
    [SerializeField] float speed = 0.022f; //increase for faster movement
    [Tooltip("The layers that the navigator can not pass through.")]
    [SerializeField] LayerMask obstacles;
    [Tooltip("Deactivate to make the navigator move along the grid only, except at the end when it reaches to the target point. This shortens the path but costs extra Physics2D.LineCast")] 
    [SerializeField] bool searchShortcut =false; 
    [Tooltip("Deactivate to make the navigator to stop at the nearest point on the grid.")]
    [SerializeField] bool snapToGrid =false; 
    Vector2 targetNode; //target in 2D space
    List <Vector2> path;
    List<Vector2> pathLeftToGo= new List<Vector2>();
    [SerializeField] bool drawDebugLines;

    //timer
    private float actionTimer = 0f;
    private float actionInterval = 0.25f;
    
    Pathfinder<Vector2> pathfinder;

    void Start()
    {   
        pathfinder = new Pathfinder<Vector2>(GetDistance,GetNeighbourNodes,1000);

        snarlingSound = GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag("Player");
            snarlingSound.Stop();

        //player = GameObject.FindGameObjectWithTag("Character"); // Find the player GameObject by tag
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component
    }

    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //  if (distanceToPlayer < snarlRange){
        //      snarlingSound.Play();
        //  }

        // Move towards the player if within range
        if (distanceToPlayer < detectionRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized; // Direction vector towards the player
            rb.velocity = direction * speed; // Move the zombie
            
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving if the player is out of range
        }


        //pathfinding stuff------------------------------------------------------------
        actionTimer += Time.deltaTime;

        // Check if it's time to update the path/action
        if (actionTimer >= actionInterval)
        {
            // Reset the timer
            actionTimer = 0f;

            // Assume you want to get a move command to the player's current position
            // You might need to adjust this part depending on your game logic
            if (player != null) // Make sure there's a reference to the player
            {
                GetMoveCommand(player.transform.position);
            }
        }

        if (pathLeftToGo.Count > 0)
        {
            Vector3 dir = (Vector3)pathLeftToGo[0] - transform.position;
            transform.position += dir.normalized * speed;
            if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude < speed * speed)
            {
                transform.position = pathLeftToGo[0];
                pathLeftToGo.RemoveAt(0);
            }
        }

        if (drawDebugLines)
        {
            for (int i = 0; i < pathLeftToGo.Count - 1; i++)
            {
                Debug.DrawLine(pathLeftToGo[i], pathLeftToGo[i + 1], Color.red);
            }
        }
    
    }
     void GetMoveCommand(Vector2 target)
    {
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) //Generate path between two points on grid that are close to the transform position and the assigned target.
        {
            if (searchShortcut && path.Count>0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }

        }
        
    }
    Vector2 GetClosestNode(Vector2 target) 
    {
        return new Vector2(Mathf.Round(target.x/gridSize)*gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }
    float GetDistance(Vector2 A, Vector2 B) 
    {
        return (A - B).sqrMagnitude; //Uses square magnitude to lessen the CPU time.
    }
    Dictionary<Vector2,float> GetNeighbourNodes(Vector2 pos) 
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();
        for (int i=-1;i<2;i++)
        {
            for (int j=-1;j<2;j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j)*gridSize;
                if (!Physics2D.Linecast(pos,pos+dir, obstacles))
                {
                    neighbours.Add(GetClosestNode( pos + dir), dir.magnitude);
                }
            }

        }
        return neighbours;
    }
     List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();
        
        for (int i=0;i<path.Count;i++)
        {
            newPath.Add(path[i]);
            for (int j=path.Count-1;j>i;j-- )
            {
                if (!Physics2D.Linecast(path[i],path[j], obstacles))
                {
                    
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }

}