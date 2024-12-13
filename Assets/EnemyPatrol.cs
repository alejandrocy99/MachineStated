using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;



public class EnemyPatrol : MonoBehaviour
{
    public Vector3 min, max;
    Vector3 destination;
    private bool PlayerDestecter;
    public float playerDistanceDetection,playerAtaqueDetection;
    Transform player;
    public float visionAngle;

    private void Start()
    {
        RandomDestination();
        StartCoroutine("Patrol");
        StartCoroutine("Alert");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    IEnumerator Alert()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) < playerDistanceDetection)
            {
                Vector3 directionPlayer = player.position - transform.position;
                if (Vector3.Angle(directionPlayer.normalized, transform.forward) < visionAngle)
                {


                    StopCoroutine("Patrol");
                    StartCoroutine("Atacar");
                    break;
                }
        }
        yield return new WaitForEndOfFrame();
    }
    IEnumerator Patrol()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, destination) < 1.5f)
            {
                GetComponent<Animator>().SetFloat("velocity", 2);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
                RandomDestination();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Atacar()
    {
        StopCoroutine("Alert");
        while (true)
        {
            
            if(Vector3.Distance(transform.position,player.position) < playerDistanceDetection)
            {
                StartCoroutine("Patrol");
                StartCoroutine("Alert");
                break;
            }
            if (Vector3.Distance(transform.position,player.position)< playerAtaqueDetection)
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
                 GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                GetComponent<Animator>().SetBool("ataque" , true);
            }else
            {
                GetComponent<Animator>().SetBool("ataque" , false);
            }
            yield return new WaitForEndOfFrame();
        }
    }}
    private void RandomDestination()
    {
        destination = new Vector3(Random.Range(min.x, max.y), 0, Random.Range(min.z, max.z));
        GetComponent<NavMeshAgent>().SetDestination(destination);
        GetComponent<Animator>().SetFloat("velocity", 2);
    }
    #region  Deteccion por trigger
    // private void OnTriggerEnter(Collider other){
    //     PlayerDestecter = true;
    //     transform.LookAt(other.transform);
    //     StopCoroutine("Patrol");
    //     GetComponent<NavMeshAgent>().SetDestination(other.transform.position);
    // }
    // private void OnTriggerExit(Collider other){
    //     PlayerDestecter = false;
    // }
    #endregion

}

