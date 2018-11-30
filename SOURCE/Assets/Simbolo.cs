using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simbolo{

    private string nombre;
    //propiedades
    //java tenian getters y setters
    //propiedades

    public string Nombre
    {
        private set
        {
            nombre = value;
        }
        get
        {
            return nombre;
        }
    }

    public Simbolo(string nombre)
    {
        Nombre = nombre;
        Debug.Log(Nombre);
    }
}
