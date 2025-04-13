using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


class Mesa
{
    public int Id { get; set; }
    public int Numero { get; set; }

    public Mesa(int numero)
    {
        Numero = numero;
    }
}