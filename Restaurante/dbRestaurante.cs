using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


class BDHelper
{
    private string connectionString;

    public BDHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public int InsertarCliente(string nombre)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            var cmd = new SqlCommand("INSERT INTO Cliente (Nombre) OUTPUT INSERTED.Id VALUES (@nombre)", con);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            return (int)cmd.ExecuteScalar();
        }
    }

    public int InsertarMesa(int numeroMesa)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            var cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM Mesa WHERE Numero = @numero) INSERT INTO Mesa (Numero) VALUES (@numero); SELECT Id FROM Mesa WHERE Numero = @numero;", con);
            cmd.Parameters.AddWithValue("@numero", numeroMesa);
            return (int)cmd.ExecuteScalar();
        }
    }

    public int InsertarOrden(int clienteId, int mesaId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            var cmd = new SqlCommand("INSERT INTO Orden (ClienteId, MesaId) OUTPUT INSERTED.Id VALUES (@clienteId, @mesaId)", con);
            cmd.Parameters.AddWithValue("@clienteId", clienteId);
            cmd.Parameters.AddWithValue("@mesaId", mesaId);
            return (int)cmd.ExecuteScalar();
        }
    }

    public void InsertarOrdenProducto(int ordenId, int productoId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            var cmd = new SqlCommand("INSERT INTO OrdenProducto (OrdenId, ProductoId) VALUES (@ordenId, @productoId)", con);
            cmd.Parameters.AddWithValue("@ordenId", ordenId);
            cmd.Parameters.AddWithValue("@productoId", productoId);
            cmd.ExecuteNonQuery();
        }
    }
}
