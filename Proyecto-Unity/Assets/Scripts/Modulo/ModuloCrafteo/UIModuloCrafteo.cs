﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIModuloCrafteo : MonoBehaviour
{
    // ───────────────────────────────────────
    // 1. REFERENCIAS SERIALIZADAS
    // ───────────────────────────────────────

    [Header("Referencias UI Modulo Compra")]
    [SerializeField]
    private GameObject ContenedorCrafteables;                                                       // Zona donde se van a generar los botones de los objetos que se van a craftear

    [Header("Prefabs UI Modulo Compra")]
    [SerializeField]
    private GameObject prefabBoton;

    // ───────────────────────────────────────
    // 2. CAMPOS PRIVADOS INTERNOS
    // ───────────────────────────────────────
    private List<ObjetoCrafteable> crafteablesDisponibles = new List<ObjetoCrafteable>();
    private ModuloCrafteo moduloCrafteo;

    // ───────────────────────────────────────
    // 3. MÉTODOS UNITY
    // ───────────────────────────────────────
    void Start()
    {
        moduloCrafteo = GetComponent<ModuloCrafteo>();
        ObtenerListas();
        GenerarUI(ref crafteablesDisponibles, ContenedorCrafteables);
        EstablecerListas();
    }

    // ───────────────────────────────────────
    // 5. MÉTODOS PRIVADOS
    // ───────────────────────────────────────
    private void ObtenerListas()
    {
        if (moduloCrafteo == null) return;
        crafteablesDisponibles = moduloCrafteo.ObtenerCrafteablesDisponibles();
    }

    private void GenerarUI(ref List<ObjetoCrafteable> lista, GameObject contenedor)
    {
        GenerarBotones(ref lista, contenedor);
        InicializarBotones(ref lista);
    }

    private void EstablecerListas()
    {
        moduloCrafteo.EstablecerCrafteablesDisponibles(crafteablesDisponibles);

    }

    private void GenerarBotones(ref List<ObjetoCrafteable> lista, GameObject contenedor)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            GameObject instanciaBoton = Instantiate(prefabBoton, contenedor.transform.position, Quaternion.identity);
            instanciaBoton.transform.SetParent(contenedor.transform, false);

            lista[i].botonObjeto = instanciaBoton.GetComponent<Button>();

            EstablecerImagen(lista[i]);
        }
    }

    private void EstablecerImagen(ObjetoCrafteable objeto)
    {
        if(objeto.imagenCrafteable != null)
        {
            Image imagenHija = objeto.botonObjeto.transform.Find("Image").GetComponent<Image>();
            imagenHija.sprite = objeto.imagenCrafteable;
        }
    }

    private void InicializarBotones(ref List<ObjetoCrafteable> lista)
    {
        for (int i = 0; i < lista.Count; i++)
        {
            int indexCapturado = i; // Captura el índice en una variable local (clave para evitar bugs de cierre)
            AsignarCallbackCompra(lista, i, indexCapturado);
        }


    }

    private void AsignarCallbackCompra(List<ObjetoCrafteable> lista, int i, int indexCapturado)
    {
        lista[i].botonObjeto.onClick.AddListener(() =>
        {
            moduloCrafteo.MostrarCrafteable(lista[indexCapturado].botonObjeto, lista);
        });
    }
}


