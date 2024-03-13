using System.Linq;

using UnityEngine;

public class Killzone : MonoBehaviour {


    // Update is called once per frame
    void Update() {

        FindObjectsOfType<GameObject>() // I Love LINQ queries :)
        .Where(obj => obj.transform.position.y < -10)
        .ToList()
        .ForEach(obj => obj.transform.position = obj.transform.position + (Vector3.up * 30));
    }
}
