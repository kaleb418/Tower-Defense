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

    void Start() {
        LoadGridCubes();
        InitializePathfinding();
        ExploreNeighbors();
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
                print("Duplicate cube detected at (" + cubeCoords.x + ", " + cubeCoords.y + ")");
            }
        }
        print(grid.Count + " grid cubes loaded");
    }

    private void InitializePathfinding() {
        frontier.Add(startingWP);
        breadcrumbs.Add(startingWP, null);
    }

    private void ExploreNeighbors() {
        while(frontier.Count > 0) {

            currentWP = frontier[0];
            if(currentWP.GetCubeCoords() == endingWP.GetCubeCoords()) {
                print("Found ending at " + currentWP.GetCubeCoords().x + ", " + currentWP.GetCubeCoords().y);
                break;
            }

            foreach(Vector2Int direction in directions) {
                Vector2Int neighborCoords = new Vector2Int(currentWP.GetCubeCoords().x + direction.x, currentWP.GetCubeCoords().y + direction.y);
                print("Exploring cube at " + neighborCoords.x + ", " + neighborCoords.y);

                if(!grid.ContainsKey(neighborCoords)) {
                    print("Cube at " + neighborCoords.x + ", " + neighborCoords.y + " was not found, skipping...");
                    continue;
                }

                if(!breadcrumbs.ContainsKey(grid[neighborCoords])) {
                    frontier.Add(grid[neighborCoords]);
                    breadcrumbs.Add(grid[neighborCoords], currentWP);
                    print("Cube at " + neighborCoords.x + ", " + neighborCoords.y + " trails back to " + currentWP.GetCubeCoords().x + ", " + currentWP.GetCubeCoords().y);
                } else {
                    print("Cube at " + neighborCoords.x + ", " + neighborCoords.y + " already explored, skipping...");
                }
            }
            frontier.RemoveAt(0);
        }
    }

    private void DefinePath() {
        Waypoint current = endingWP;
        while(current != startingWP) {
            path.Add(current);
            current = breadcrumbs[current];
        }
        path.Add(startingWP);
        path.Reverse();
    }
}
