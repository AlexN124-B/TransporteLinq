using System.Collections;

// Utilidades de salida por consola. No hace falta modificar este fichero.

public static class Consola
{
    // Muestra un resultado: una secuencia (un elemento por línea, con su número
    // de elementos), un texto, un valor suelto o null.
    public static void Mostrar(string titulo, object? resultado)
    {
        Console.WriteLine($"--- {titulo} ---");
        switch (resultado)
        {
            case null:
                Console.WriteLine("(null)");
                break;
            case string texto:
                Console.WriteLine(texto);
                break;
            case IEnumerable secuencia:
                var items = secuencia.Cast<object?>().ToList();
                Console.WriteLine($"({items.Count} elementos)");
                foreach (var it in items) Console.WriteLine("  " + it);
                break;
            default:
                Console.WriteLine(resultado);
                break;
        }
        Console.WriteLine();
    }

    public static void MostrarRuta(string titulo, List<Tramo>? ruta)
    {
        if (ruta is null || ruta.Count == 0)
        {
            Mostrar(titulo, "Sin ruta posible");
            return;
        }
        var lineas = new List<string> { $"{ruta.Count - 1} transbordos" };
        lineas.AddRange(ruta.Select(t => "  " + t));
        Mostrar(titulo, string.Join(Environment.NewLine, lineas));
    }
}
