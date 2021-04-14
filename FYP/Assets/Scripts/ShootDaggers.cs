using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootDaggers : MonoBehaviour
{
    [SerializeField] int numberOfDaggers = 10;
    [SerializeField] float startAngle = 90f, endAngle = 270f;

    Vector2 dagDirection;


    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("Shoot", 0f, 2f);
    }

    void Shoot()
    {
        float angleInterval = (endAngle - startAngle) / numberOfDaggers;
        float angle = startAngle;


        for (int dagger = 0; dagger < numberOfDaggers; dagger++)
        {
            float dagX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float dagY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 dagVector = new Vector3(dagX, dagY, 0f);
            dagDirection = (dagVector - transform.position).normalized;

            GameObject dag = DaggerPool.daggerPoolInstance.GetDagger();
            dag.transform.position = transform.position;
            dag.transform.rotation = transform.rotation;
            dag.SetActive(true);
            dag.GetComponent<Dagger>().SetMoveDirection(dagDirection);

            angle += angleInterval;

        }
    }


}
