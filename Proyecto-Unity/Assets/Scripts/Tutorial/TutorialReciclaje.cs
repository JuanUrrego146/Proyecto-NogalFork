using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialReciclaje : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private string escenaPrincipal = "SampleScene";
    [SerializeField] private float duracionMensaje = 5f;
    [SerializeField] private ModuloSeparacion moduloSeparacion;
    [SerializeField] private ModuloCompra moduloCompra;
    [SerializeField] private ModuloCrafteo moduloCrafteo;
    [SerializeField] private Inventario inventario;
    private const string PREF_TUTORIAL = "TutorialCompletado";

    private static GameObject interaccionBlanca;
    private static GameObject interaccionVerde;
    private static GameObject interaccionNegra;

    private GameManager gameManager;
   

    private void Awake()
    {
        interaccionBlanca = GameObject.Find("InteraccionB");
        interaccionVerde = GameObject.Find("InteraccionV");
        interaccionNegra = GameObject.Find("InteraccionN");
        DesactivarInteracciones();
    }
    void Start()
    {
        //PlayerPrefs.SetInt(PREF_TUTORIAL, 0); /* < ------Para volver a ejecutar el tutorial incluso habiendoselo saltado quitar los comentariocs*/
        //PlayerPrefs.Save();
        if (PlayerPrefs.GetInt(PREF_TUTORIAL, 1) == 1)
        {
            
            SceneManager.LoadScene(escenaPrincipal);
            return;
            

        }
        gameManager = FindObjectOfType<GameManager>();
        moduloSeparacion = FindObjectOfType<ModuloSeparacion>();
        moduloCompra = FindObjectOfType<ModuloCompra>();
        moduloCrafteo = FindObjectOfType<ModuloCrafteo>();
        inventario = FindObjectOfType<Inventario>();
        if (gameManager != null)
        {
            gameManager.BloquearCamara();
        }

        StartCoroutine(MostrarTutorial());
    }

    public void SaltarTutorial()
    {
        if (gameManager != null)
        {
            gameManager.RestaurarCamara();
        }
        PlayerPrefs.SetInt(PREF_TUTORIAL, 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(escenaPrincipal);
    }

    private IEnumerator MostrarTutorial()
    {
        GestorCajaTexto.Instancia.MostrarMensaje(
            "El reciclaje es el proceso de recoger y transformar los residuos para darles una segunda vida, reduciendo la cantidad de basura que llega a los rellenos sanitarios y el uso de recursos naturales.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Separar correctamente los residuos desde casa permite que los materiales aprovechables lleguen limpios a los centros de reciclaje y disminuye la contaminaci�n en nuestro entorno.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "La caneca verde representa los residuos org�nicos biodegradables, como restos de comida y c�scaras, que pueden convertirse en abono natural mediante compostaje.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "La caneca blanca se usa para los materiales aprovechables no org�nicos como papeles limpios, pl�sticos, metales o vidrios que pueden reciclarse o reutilizarse.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "La caneca negra se reserva para los residuos no aprovechables, aquellos que est�n contaminados, sucios o deteriorados y que terminar�n en un relleno sanitario.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Ejemplos para la caneca verde: manzana, pan, br�coli, hamburguesa, s�ndwich de pollo, huevos, lechuga o arroz; todos son restos que se descomponen f�cilmente.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Ejemplos para la caneca blanca: libro, botella de pl�stico limpia, botella de vidrio en buen estado, martillo, palanca, caja de cart�n, pocillo o sart�n sin �xido; son materiales que pueden reciclarse o reutilizarse.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Ejemplos para la caneca negra: botella sucia, lata de comida sucia, lata de pintura, caneca de pintura, caja de pizza sucia o lata oxidada; al estar contaminados no pueden aprovecharse.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        if (moduloSeparacion != null)
        {
            moduloSeparacion.Interactuar();
        }
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Este es el m�dulo de separaci�n, aqu� obtienes bolsas y decides qu� hacer con su contenido.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Puedes abrir cada bolsa para revisar sus residuos o botarla directamente en la caneca correcta.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Las bolsas negras siempre deben abrirse para separar lo que contengan.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Si una bolsa ya trae todos los residuos separados, puedes usar el bot�n 'Botar bolsa'.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);

        if (moduloSeparacion != null)
        {
            yield return new WaitUntil(() => moduloSeparacion.HaClasificadoTodasLasBolsas);
        }
        yield return new WaitForSeconds(0.5f);

        if (moduloCompra != null)
        {
            moduloCompra.Interactuar();
        }
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Este es el m�dulo de compra. Aqu� puedes adquirir m�s mesas, estilos de ropa que llegar�n pr�ximamente y accesorios para tu bodega.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);

        inventario?.AsegurarDinero(10000);
        GestorCajaTexto.Instancia.MostrarMensaje(
            "Te hemos entregado 10.000 monedas para que compres la mesa de crafteo.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);

        GestorCajaTexto.Instancia.MostrarMensaje(
            "Compra la mesa de crafteo para continuar.",
            duracionMensaje);
        yield return new WaitUntil(() => moduloCrafteo != null && moduloCrafteo.gameObject.activeSelf);

        GestorCajaTexto.Instancia.MostrarMensaje(
            "Perfecto, ya tienes la mesa de crafteo. Ahora pasemos a aprender sobre ella.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);

        if (moduloCompra != null)
        {
            moduloCompra.SalirModuloCallback();
        }
        yield return new WaitForSeconds(0.5f);

        if (moduloCrafteo != null)
        {
            moduloCrafteo.Interactuar();
        }
        GestorCajaTexto.Instancia.MostrarMensaje(
            "En el m�dulo de crafteo, pr�ximamente podr�s crear objetos con los residuos reutilizables que clasificaste, aunque por ahora esta mec�nica est� en desarrollo.",
            duracionMensaje);
        yield return new WaitForSeconds(duracionMensaje);

        if (moduloCrafteo != null)
        {
            moduloCrafteo.SalirModuloCallback();
        }

        //PlayerPrefs.SetInt(PREF_TUTORIAL, 1);
        //PlayerPrefs.Save();
        yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene(escenaPrincipal);
    }
    private static void SetInteracciones(bool blanca, bool verde, bool negra)
    {
        if (interaccionBlanca != null) interaccionBlanca.SetActive(blanca);
        if (interaccionVerde != null) interaccionVerde.SetActive(verde);
        if (interaccionNegra != null) interaccionNegra.SetActive(negra);
    }

    public static void DesactivarInteracciones()
    {
        SetInteracciones(false, false, false);
    }

    private static void ActivarSoloInteraccion(GameObject interaccion)
    {
        SetInteracciones(interaccion == interaccionBlanca,
                         interaccion == interaccionVerde,
                         interaccion == interaccionNegra);
    }

    public static void MostrarMensajeResiduo(GameObject item)
    {
        if (PlayerPrefs.GetInt(PREF_TUTORIAL, 0) == 1) return;

        Item itemComp = item.GetComponent<Item>();
        if (itemComp == null)
        {
            DesactivarInteracciones();
            return;
        }

        DatosItem datos = itemComp.ObtenerDatosItem();
        string nombre = item.name.Replace("(Clone)", "").Trim();
        string mensaje;
        switch (datos.tipoReciclaje)
        {
            case ETipoReciclaje.OrganicoAprovechable:
                if (datos.tipoItem == ETipoItem.Bolsa)
                    mensaje = "Esta bolsa verde ya trae residuos org�nicos separados, b�tala completa en la caneca verde sin abrirla.";
                else
                    mensaje = nombre + " es un residuo org�nico que se descompone y puede convertirse en abono, por eso debe ir en la caneca verde.";
                ActivarSoloInteraccion(interaccionVerde);
                break;
            case ETipoReciclaje.Aprovechable:
                if (datos.tipoItem == ETipoItem.Bolsa)
                    mensaje = "Esta bolsa blanca contiene materiales reciclables limpios; depos�tala entera en la caneca blanca.";
                else
                    mensaje = nombre + " est� limpio y en buen estado, puede reciclarse o reutilizarse; depos�talo en la caneca blanca.";
                ActivarSoloInteraccion(interaccionBlanca);
                break;
            case ETipoReciclaje.NoAprovechable:
                if (datos.tipoItem == ETipoItem.Bolsa)
                {
                    mensaje = "Esta bolsa contiene materiales de Todo tipo, no ha sido clasificada, debi� abrirse y clasificarse; puede depositarse en cualquier caneca aunque algunos items no pertenezcan a ella.";
                    SetInteracciones(true, true, true);
                }

                else
                {
                    mensaje = nombre + " est� sucio, contaminado u oxidado y no se recicla; b�talo en la caneca negra.";
                    ActivarSoloInteraccion(interaccionNegra);
                }
                    
                break;
            default:
                DesactivarInteracciones();
                return;
        }
        GestorCajaTexto.Instancia.MostrarMensaje(mensaje, 5f);
    }
}
