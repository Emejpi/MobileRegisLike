using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicActions : MonoBehaviour {

    protected bool CloseEnought(float value1, float value2, float mistake)
    {
        return value1 + mistake > value2 && value1 - mistake < value2;
    }

    protected Vector3 LerpVec3(Vector3 vec1, Vector3 vec2, float speed)
    {
        return new Vector3(Mathf.Lerp(vec1.x, vec2.x, speed), Mathf.Lerp(vec1.y, vec2.y, speed), Mathf.Lerp(vec1.z, vec2.z, speed));
    }

}
