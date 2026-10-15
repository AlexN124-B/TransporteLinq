// Casos de prueba del planificador. No hace falta modificar este fichero.
// Si un nivel todavía no está implementado, se avisa y se continúa con el siguiente.

public static class PruebasPlanificador
{
    private static string Nombre(int id) => Datos.Paradas.First(p => p.Id == id).Nombre;

    private static void Probar(string titulo, Func<object?> prueba)
    {
        try
        {
            Consola.Mostrar(titulo, prueba());
        }
        catch (NotImplementedException)
        {
            Consola.Mostrar(titulo, "(sin implementar)");
        }
    }

    private static void ProbarRuta(string titulo, Func<List<Tramo>?> prueba)
    {
        try { Consola.MostrarRuta(titulo, prueba()); }
        catch (NotImplementedException) { Consola.Mostrar(titulo, "(sin implementar)"); }
    }

    public static void Ejecutar()
    {
        Console.WriteLine("=========== RETO FINAL: PLANIFICADOR ===========");
        Console.WriteLine();

        // Nivel 1
        Probar($"Nivel 1: líneas directas {Nombre(5)} -> {Nombre(23)}",
               () => Planificador.LineasDirectas(5, 23));
        Probar($"Nivel 1: líneas directas {Nombre(1)} -> {Nombre(2)}",
               () => Planificador.LineasDirectas(1, 2));
        Probar($"Nivel 1: líneas directas {Nombre(17)} -> {Nombre(21)}",
               () => Planificador.LineasDirectas(17, 21));

        // Nivel 2
        Probar("Nivel 2: tramo L2 de 19 a 17", () =>
        {
            var t = Planificador.TramoDeLinea(2, 19, 17);
            return $"{t.Minutos} min: " + string.Join(" -> ", t.Paradas.Select(p => p.Nombre));
        });
        Probar("Nivel 2: tramo L2 de 17 a 19 (sentido contrario)", () =>
        {
            var t = Planificador.TramoDeLinea(2, 17, 19);
            return $"{t.Minutos} min: " + string.Join(" -> ", t.Paradas.Select(p => p.Nombre));
        });
        Probar("Nivel 2: tramo L3 de 12 a 11", () =>
        {
            var t = Planificador.TramoDeLinea(3, 12, 11);
            return $"{t.Minutos} min: " + string.Join(" -> ", t.Paradas.Select(p => p.Nombre));
        });

        // Nivel 3
        Probar($"Nivel 3: un transbordo {Nombre(17)} -> {Nombre(21)}",
               () => Planificador.UnTransbordo(17, 21)
                         .Select(x => $"L{x.Primera.Id} -> {x.Transbordo.Nombre} -> L{x.Segunda.Id}"));
        Probar($"Nivel 3: un transbordo {Nombre(16)} -> {Nombre(22)}",
               () => Planificador.UnTransbordo(16, 22)
                         .Select(x => $"L{x.Primera.Id} -> {x.Transbordo.Nombre} -> L{x.Segunda.Id}"));

        // Nivel 4
        ProbarRuta($"Nivel 4: menos transbordos {Nombre(5)} -> {Nombre(23)}",
                   () => Planificador.MenosTransbordos(5, 23));
        ProbarRuta($"Nivel 4: menos transbordos {Nombre(17)} -> {Nombre(21)}",
                   () => Planificador.MenosTransbordos(17, 21));
        ProbarRuta($"Nivel 4: menos transbordos {Nombre(16)} -> {Nombre(22)}",
                   () => Planificador.MenosTransbordos(16, 22));
        ProbarRuta($"Nivel 4: menos transbordos {Nombre(24)} -> {Nombre(1)} (parada sin servicio)",
                   () => Planificador.MenosTransbordos(24, 1));

        // Nivel 5 (opcional)
        Probar($"Nivel 5 (opcional): mejor ruta por tiempo {Nombre(16)} -> {Nombre(22)}", () =>
        {
            var r = Planificador.MejorPorTiempo(16, 22);
            return r is null
                ? "Sin ruta posible"
                : $"{r.Value.Minutos} minutos (con 5 min por transbordo){Environment.NewLine}" +
                  string.Join(Environment.NewLine, r.Value.Tramos.Select(t => "  " + t));
        });
        Probar($"Nivel 5 (opcional): mejor ruta por tiempo {Nombre(17)} -> {Nombre(21)}", () =>
        {
            var r = Planificador.MejorPorTiempo(17, 21);
            return r is null
                ? "Sin ruta posible"
                : $"{r.Value.Minutos} minutos (con 5 min por transbordo){Environment.NewLine}" +
                  string.Join(Environment.NewLine, r.Value.Tramos.Select(t => "  " + t));
        });
    }
}
