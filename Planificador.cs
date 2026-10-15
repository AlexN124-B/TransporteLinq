// RETO FINAL: PLANIFICADOR DE RUTAS
//
// Reglas de la red:
//  - Todas las líneas son bidireccionales: se puede viajar en ambos sentidos.
//  - El tiempo entre dos paradas consecutivas de una línea es el mismo en los dos sentidos
//    (es el MinutosDesdeAnterior de la parada que va más adelante en el Orden).
//
// Implementa los niveles en orden. Cada método sustituye al "throw" por tu código.
// Para comprobarlo, ejecuta el programa y compara con salida_esperada.txt.

public record Tramo(Linea Linea, Parada Desde, Parada Hasta)
{
    public override string ToString() => $"L{Linea.Id} {Linea.Nombre}: {Desde.Nombre} -> {Hasta.Nombre}";
}

public static class Planificador
{
    // NIVEL 1 - Conexión directa
    // Líneas que pasan por las dos paradas, ordenadas por Id. Lista vacía si no hay ninguna.
    public static List<Linea> LineasDirectas(int origen, int destino)
        => throw new NotImplementedException();

    // NIVEL 2 - Recorrido de una línea
    // Paradas de la línea entre origen y destino (ambas incluidas), en el orden en que se
    // recorren (si origen va después que destino, el orden se invierte), y los minutos totales:
    // la suma de MinutosDesdeAnterior de las paradas del tramo, SIN contar la de partida.
    public static (int Minutos, List<Parada> Paradas) TramoDeLinea(int lineaId, int origen, int destino)
        => throw new NotImplementedException();

    // NIVEL 3 - Un transbordo
    // Combinaciones (línea de subida, línea de bajada, parada de transbordo) tales que:
    //  - la primera línea pasa por el origen y la segunda por el destino,
    //  - las dos líneas son distintas y comparten la parada de transbordo,
    //  - la parada de transbordo no es ni el origen ni el destino.
    // Ordenadas por Id de la primera línea, Id de la segunda y Id de la parada.
    public static List<(Linea Primera, Linea Segunda, Parada Transbordo)> UnTransbordo(int origen, int destino)
        => throw new NotImplementedException();

    // NIVEL 4 - Menos transbordos (búsqueda en anchura, BFS)
    // Ruta con el menor número de transbordos, como lista de tramos (una línea cada uno).
    // Pista: el grafo no es de paradas sino de LÍNEAS; dos líneas son vecinas si comparten parada.
    // Desempates (para que tu salida coincida): explora las líneas por Id ascendente y haz
    // el transbordo en la parada de menor Id entre las compartidas.
    // Devuelve null si no hay ruta posible. Si hay una línea directa, un solo tramo.
    public static List<Tramo>? MenosTransbordos(int origen, int destino)
        => throw new NotImplementedException();

    // NIVEL 5 (OPCIONAL) - Mejor ruta por tiempo (Dijkstra)
    // Minimiza el tiempo total: los minutos en línea más 'penalizacionTransbordo' minutos
    // por cada cambio de línea. Pista: el estado del grafo es (parada, línea en la que vas).
    // Devuelve null si no hay ruta.
    public static (int Minutos, List<Tramo> Tramos)? MejorPorTiempo(int origen, int destino, int penalizacionTransbordo = 5)
        => throw new NotImplementedException();
}
