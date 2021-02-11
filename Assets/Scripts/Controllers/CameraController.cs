#region

using UnityEngine;

#endregion

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    private void Update()
    {
        var transform1 = transform;
        var position = player.position;
        transform1.position = new Vector3(position.x, position.y, transform1.position.z);
    }
}