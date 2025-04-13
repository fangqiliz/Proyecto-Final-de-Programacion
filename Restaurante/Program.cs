using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=tcp:programacion-server.database.windows.net,1433;Initial Catalog=Restaurante database;Persist Security Info=False;User ID=sqladmin;Password=Flz192006;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        BDHelper db = new BDHelper(connectionString);

        List<Producto> productosDisponibles = new List<Producto>()
    {
        new Producto(1, "Pizza", 350),
        new Producto(2, "Hamburguesa", 250),
        new Producto(3, "Refresco", 80),
        new Producto(4, "Pasta", 300),
        new Producto(5, "Postre", 150),
        new Producto(6, "Café", 100)
    };

        Console.WriteLine("--- Simulador de Restaurante ---");

        while (true)
        {
            Console.Write("\nNombre del cliente (escriba 'salir' para finalizar): ");
            string nombreCliente = Console.ReadLine();

            if (nombreCliente.ToLower() == "salir")
                break;

            Console.Write("Número de mesa: ");
            int numeroMesa = int.Parse(Console.ReadLine());

            Cliente cliente = new Cliente(nombreCliente);
            Mesa mesa = new Mesa(numeroMesa);
            Orden orden = new Orden(cliente, mesa);

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- Menú ---");
                Console.WriteLine("1. Ver productos disponibles");
                Console.WriteLine("2. Agregar producto a la orden");
                Console.WriteLine("3. Ver resumen de la orden");
                Console.WriteLine("4. Finalizar y guardar orden");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("\nProductos disponibles:");
                        foreach (var p in productosDisponibles)
                        {
                            Console.WriteLine($"{p.Id}. {p.Nombre} - RD${p.Precio}");
                        }
                        break;

                    case "2":
                        Console.Write("Ingrese el ID del producto a agregar: ");
                        int id = int.Parse(Console.ReadLine());
                        Producto prod = productosDisponibles.Find(p => p.Id == id);
                        if (prod != null)
                        {
                            orden.Productos.Add(prod);
                            Console.WriteLine($"Producto '{prod.Nombre}' agregado.");
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("\nResumen de la orden:");
                        foreach (var p in orden.Productos)
                        {
                            Console.WriteLine($"- {p.Nombre} - RD${p.Precio}");
                        }
                        Console.WriteLine($"Total parcial: RD${orden.CalcularTotal()}");
                        break;

                    case "4":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }

            // Guardar en BD
            int clienteId = db.InsertarCliente(nombreCliente);
            int mesaId = db.InsertarMesa(numeroMesa);
            int ordenId = db.InsertarOrden(clienteId, mesaId);

            foreach (var p in orden.Productos)
            {
                db.InsertarOrdenProducto(ordenId, p.Id);
            }

            Console.WriteLine("\nOrden guardada exitosamente en Azure SQL.");
            Console.WriteLine($"Total a pagar: RD${orden.CalcularTotal()}");
        }

        Console.WriteLine("\nPrograma finalizado.");
    }

}
