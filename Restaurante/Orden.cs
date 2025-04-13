using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

class Orden
{
    public Cliente Cliente { get; set; }
    public Mesa Mesa { get; set; }
    public List<Producto> Productos { get; set; }

    public Orden(Cliente cliente, Mesa mesa)
    {
        Cliente = cliente;
        Mesa = mesa;
        Productos = new List<Producto>();
    }

    public decimal CalcularTotal()
    {
        decimal total = 0;
        foreach (var producto in Productos)
        {
            total += producto.Precio;
        }
        return total;
    }
}
