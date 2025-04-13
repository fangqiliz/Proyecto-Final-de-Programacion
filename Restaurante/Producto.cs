using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;




class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }

    public Producto(int id, string nombre, decimal precio)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
    }
}
