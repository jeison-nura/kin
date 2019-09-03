using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

using Joint = Windows.Kinect.Joint;
using System;

public class BodySourceView : MonoBehaviour 
{
    public GameObject mJointObject;
    public BodySourceManager _BodyManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private List<JointType> _joint = new List<JointType> {
        JointType.HandLeft,
        JointType.HandRight,
    };
    
    
    
    void Update () 
    {
        #region Get kinect data
        Body[] data = _BodyManager.GetData();
        if (data == null)
            return;
        List<ulong> trackedIds = new List<ulong>();
        foreach ( var body in data ) {
            if (body == null)
                continue;
            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
        }
        #endregion

        #region Delete kinect bodies
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        foreach (ulong trackingId in knownIds) {
            if (!trackedIds.Contains(trackingId)) {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }
        #endregion
        #region create kinect bodies
        foreach ( var body in data ) {
            if (body == null)
                continue;

            if (body.IsTracked) {
                if (!_Bodies.ContainsKey(body.TrackingId)) {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                UpdateBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
        #endregion
    }    

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        foreach ( JointType joint in _joint ) {
            GameObject newJoint = Instantiate(mJointObject);
            newJoint.name = joint.ToString();

            newJoint.transform.parent = body.transform;
        }
        
        return body;
    }


    private void UpdateBodyObject(Body body, GameObject bodyObject)
    {

        foreach ( JointType mjoint in _joint ) {

            Joint sourceJoint = body.Joints[mjoint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
            targetPosition.z = 0;

            Transform jointObject = bodyObject.transform.Find(mjoint.ToString());
            jointObject.position = targetPosition;
        }

    }


    private static Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
