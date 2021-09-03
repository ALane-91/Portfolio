using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public enum FollowType
    {
        MoveTowards,
        Back
    }

    //Sets the default FollowType to be MoveTowards;
    public FollowType Type = FollowType.MoveTowards;
    //Refers back to the MovingPlatforms script
    public MovingPlatforms Path;
    public float Speed = 1;
    //If within a certain range of the next point in path, will start moving towards other the point after.
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> _currentPoint;

    public void Start()
    {
        if (Path == null)
        {
            //Sends an error to console if value of Path is null, e.g if no start point is set for a path.
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }

        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
            return;

        transform.position = _currentPoint.Current.position;
    }

    public void Update()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        if (Type == FollowType.MoveTowards)
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
        else if (Type == FollowType.Back)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            _currentPoint.MoveNext();
    }
}
