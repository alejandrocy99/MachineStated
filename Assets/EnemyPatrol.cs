using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;



public class EnemyPatrol : MonoBehaviour
{
    public Vector3 min,max;
    Vector3 destination;
    private bool PlayerDestecter;

    private void Start(){
        RandomDestination();
        StartCoroutine("Patrol");
    }

    IEnumerator Patrol(){
        while (true)
        {
            if(Vector3.Distance(transform.position,destination)  < 1.5f){
                GetComponent<Animator>().SetFloat("velocity",2);
                yield return new WaitForSeconds(Random.Range(1f,3f));
                RandomDestination();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void RandomDestination()
    {
      destination = new Vector3(Random.Range(min.x,max.y),0,Random.Range(min.z,max.z));
      GetComponent<NavMeshAgent>().SetDestination(destination);
      GetComponent<Animator>().SetFloat("velocity",2);
    }

    private void OnTriggerEnter(Collider other){
        PlayerDestecter = true;
        transform.LookAt(other.transform);
        StopCoroutine("Patrol");
        GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
    }
    private void OnTriggerExit(Collider other){
        PlayerDestecter = false;
    }
}
