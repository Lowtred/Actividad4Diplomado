using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlDelPersonaje : MonoBehaviour
{
    //InfoJugador
    public float danoPorGolpe;
    public float vidaJugador;
    float vidaActualJugador;

    //Movimiento
    
    public CharacterController controlador;
    public Transform camara;
    public float velocidadDeMovimiento;
    private float rotacionSuave = 0.1f;
    float velocidadRotacionSuave;


    public Transform chequeadorDeSuelo;
    public float distanciaAlSuelo;
    public LayerMask mascaraDeSuelo;

    Vector3 velocidad;
    bool estaEnElSuelo;
    public float gravedad = -9.81f;
    public float alturaDelSalto;

    //Animacion
    public Animator animator;


    public string variableMovimiento;
    public string variableSuelo;
    public string variableDano;
    public string variableMorir;

    public bool canMove;

    public GameObject colliderAtaque;
    void Start()
    {
        controlador = GetComponent<CharacterController>();
        vidaActualJugador = vidaJugador;
        DejaDeAtacar();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && estaEnElSuelo)
        {
            animator.SetTrigger("Ataque");
        }

        if (canMove)
        {
            estaEnElSuelo = Physics.CheckSphere(chequeadorDeSuelo.position, distanciaAlSuelo, mascaraDeSuelo);
            if (estaEnElSuelo && velocidad.y < 0)
            {
                velocidad.y = -2f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direccion = new Vector3(horizontal, 0f, vertical).normalized; //Normalized es para que al moverse en diagonal no vaya más rápido
            animator.SetFloat(variableMovimiento, (Mathf.Abs(vertical) + Mathf.Abs(horizontal)));

            if (direccion.magnitude >= 0.1f)
            {
                float anguloARotar = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.eulerAngles.y;
                float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloARotar, ref velocidadRotacionSuave, rotacionSuave);
                transform.rotation = Quaternion.Euler(0f, angulo, 0f);

                Vector3 direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.forward;
                controlador.Move(direccionDelMovimiento.normalized * velocidadDeMovimiento * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && estaEnElSuelo)
            {
                velocidad.y = Mathf.Sqrt(alturaDelSalto * -2f * gravedad);
            }

            velocidad.y += gravedad * Time.deltaTime;
            controlador.Move(velocidad * Time.deltaTime);

            animator.SetBool(variableSuelo, controlador.isGrounded);
        }
        
    }

    //Detector de golpes recibidos
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AtaqueEnemigo"))
        {
            if (vidaActualJugador > 0)
            {
                animator.SetTrigger(variableDano);
                vidaActualJugador -= danoPorGolpe;
                Debug.Log(vidaActualJugador);
            }

            if (vidaActualJugador <= 0)
            {
                animator.SetTrigger(variableMorir);
                //Destroy(gameObject, 4);
            }
        }
    }

    
    public void Ataca()
    {
        canMove = false;
        colliderAtaque.SetActive(true);
    }

    public void DejaDeAtacar()
    {
        canMove = true;
        colliderAtaque.SetActive(false);
    }

}