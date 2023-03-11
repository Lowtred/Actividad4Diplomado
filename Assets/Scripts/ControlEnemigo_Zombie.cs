using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigo_Zombie : MonoBehaviour
{
    //InfoEnemigo
    public float danoPorGolpe;
    public float vidaDelEnemigo;
    float vidaActualEnemigo;
    //EstadosEnemigo
    public float rangoDeAlerta;
    public LayerMask capaDelJugador;
    private bool estarAlerta;
    public Transform jugador;
    public float velocidad;
    //Animaciones
    public Animator animator;
    public string variableGolpear;
    public string variableMovimiento;
    public string variableDano;
    public string variableMorir;
    

    //LogicaAtaque
    bool enMovimiento = false;
    bool canMove = true;
    public GameObject colliderAtaque;

    // Start is called before the first frame update
    void Start()
    {
        vidaActualEnemigo = vidaDelEnemigo;
        jugador = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canMove) 
        {
            estarAlerta = Physics.CheckSphere(transform.position, rangoDeAlerta, capaDelJugador);
            if(estarAlerta== true) 
            {
                if (enMovimiento == false) 
                {
                    animator.SetBool(variableMovimiento, true);
                    enMovimiento = true;
                }    
                Vector3 posJugador = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
                transform.LookAt(posJugador);
                transform.position = Vector3.MoveTowards(transform.position, posJugador, velocidad * Time.deltaTime);
            }
            else 
            {
                if (enMovimiento == true)
                {
                    animator.SetBool(variableMovimiento, false);
                    enMovimiento = false;
                }
            }
        }
        
    }

    //Detector de golpes recibidos
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AtaqueJugador"))
        {
            if (vidaActualEnemigo > 0)
            { 
                animator.SetTrigger(variableDano);
                vidaActualEnemigo -= danoPorGolpe;
                Debug.Log(vidaActualEnemigo);
            }

            if (vidaActualEnemigo <= 0)
            {
                animator.SetTrigger(variableMorir);
                Destroy(gameObject,4);
            }
        }
    }

    //Golpenado

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Contacto con jugador");
            animator.SetTrigger(variableGolpear);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeAlerta);
    }

    public void Ataca()
    {
        colliderAtaque.SetActive(true);
    }

    public void DejaDeAtacar()
    {
        
        colliderAtaque.SetActive(false);
    }

    public void Muevete()
    {
        canMove = true;
    }

    public void NoTeMuevas()
    {
        canMove = false;
        animator.SetBool(variableMovimiento, false);
        enMovimiento = false;
    }
}
