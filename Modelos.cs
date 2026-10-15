// Modelos de la red de transporte. No hace falta modificar este fichero.
// Se han añadido sobrescrituras de ToString() para que los resultados
// se vean legibles al imprimirlos por consola.

public class Parada
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int Zona { get; set; }

    public override string ToString() => $"[{Id}] {Nombre} (zona {Zona})";
}

public class Linea
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public int VehiculoId { get; set; }

    public override string ToString() => $"L{Id} {Nombre} (vehículo {VehiculoId})";
}

public class ParadaLinea
{
    public int LineaId { get; set; }
    public int ParadaId { get; set; }
    public int Orden { get; set; }
    public int MinutosDesdeAnterior { get; set; }

    public override string ToString() =>
        $"L{LineaId} #{Orden}: parada {ParadaId} (+{MinutosDesdeAnterior} min)";
}

public class Vehiculo
{
    public int Id { get; set; }
    public string Matricula { get; set; } = "";
    public int Capacidad { get; set; }

    public override string ToString() => $"[{Id}] {Matricula} (capacidad {Capacidad})";
}

public class Viaje
{
    public int Id { get; set; }
    public int LineaId { get; set; }
    public DateTime Fecha { get; set; }
    public int Pasajeros { get; set; }

    public override string ToString() =>
        $"Viaje {Id:000}: L{LineaId} {Fecha:yyyy-MM-dd HH:mm} - {Pasajeros} pasajeros";
}
