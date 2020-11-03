using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder:MonoBehaviour {

    [SerializeField] Waypoint startingWP;
    [SerializeField] Waypoint endingWP;

    private Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    private Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private List<Waypoint> frontier = new List<Waypoint>();
    private Dictionary<Waypoint, Waypoint> breadcrumbs = new Dictionary<Waypoint, Waypoint>();
    private List<Waypoint> path = new List<Waypoint>();
    private Waypoint currentWP;

    void Awake() {
        LoadGridCubes();
        InitializePathfinding();
        Pathfind();
        DefinePath();
    }

    public List<Waypoint> GetPath() {
        return path;
    }

    private void LoadGridCubes() {
        Waypoint[] waypointArr = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypointArr) {

            Vector2Int cubeCoords = waypoint.GetCubeCoords();

            if(!grid.ContainsKey(cubeCoords)) {   // prevent duplicate coords
                grid.Add(cubeCoords, waypoint);
            } else {
                print("Found duplicate waypoint at " + waypoint + ", not adding to gridspace");
            }
        }
    }

    private void InitializePathfinding() {
        frontier.Add(startingWP);
        breadcrumbs.Add(startingWP, null);
    }

    private void Pathfind() {
        while(frontier.Count > 0) {
            currentWP = frontier[0];

            if(currentWP.GetCubeCoords() == endingWP.GetCubeCoords()) {
                // current coord is ending coord, break loop
                break;
            }

            ExploreNeighbors();
            frontier.RemoveAt(0);
        }
    }

    private void ExploreNeighbors() {
        foreach(Vector2Int direction in directions) {

            Vector2Int neighborCoords = new Vector2Int(currentWP.GetCubeCoords().x + direction.x, currentWP.GetCubeCoords().y + direction.y);
            if(!grid.ContainsKey(neighborCoords)) {
                // cube doesn't exist, skip
                continue;
            }

            if(!grid[neighborCoords].IsWalkable()) {
                // can't walk on, skip
                continue;
            }

            if(!breadcrumbs.ContainsKey(grid[neighborCoords])) {    // skip if already explored
                // add neighbor to frontier and breadcrumbs
                frontier.Add(grid[neighborCoords]);
                breadcrumbs.Add(grid[neighborCoords], currentWP);
            }
        }
    }

    private void DefinePath() {
        Waypoint current = endingWP;
        while(current != startingWP) {
            // add each waypoint to path (backwards)
            path.Add(current);

            if(breadcrumbs.ContainsKey(current)) {
                current = breadcrumbs[current];
            } else {
                // no viable path found
                path.Clear();
                print("No viable path found from " + startingWP + " to " + endingWP);
                break;
            }
        }
        path.Add(startingWP);
        path.Reverse();
    }
}
