using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;




class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Cliente(string nombre)
    {
        Nombre = nombre;
    }
}