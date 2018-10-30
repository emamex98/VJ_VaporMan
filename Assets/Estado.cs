using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Estado {

    private string nombre;
    private Dictionary<Simbolo, Estado> transicion;
    private Type tipo;

    public string Nombre
    {
        get
        {
            return nombre;
        }
    }

    public Type Tipo
    {
        get
        {
            return tipo;
        }
    }

    public Estado(string nombre, Type tipo)
    {
        this.nombre = nombre;
        this.tipo = tipo;
        transicion = new Dictionary<Simbolo, Estado>();
    }

    public void definirTransicion(Simbolo simbolo, Estado estado)
    {
        transicion.Add(simbolo, estado);
    }

    public Estado aplicarSimbolo(Simbolo simbolo)
    {
        if (transicion.ContainsKey(simbolo))
        {
            return transicion[simbolo];
        }

        return this;
    }
}
