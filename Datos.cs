// Datos fijos de la práctica. No hace falta modificar este fichero.
// Los viajes se generan con un generador congruencial propio y una semilla
// fija, así que TODO el alumnado trabaja exactamente con los mismos datos.

public static class Datos
{
    public static readonly List<Parada> Paradas = new()
    {
        new() { Id = 1,  Nombre = "Estación Central",    Zona = 1 },
        new() { Id = 2,  Nombre = "Plaza Mayor",         Zona = 1 },
        new() { Id = 3,  Nombre = "Ayuntamiento",        Zona = 1 },
        new() { Id = 4,  Nombre = "Mercado",             Zona = 1 },
        new() { Id = 5,  Nombre = "Catedral",            Zona = 1 },
        new() { Id = 6,  Nombre = "Universidad",         Zona = 2 },
        new() { Id = 7,  Nombre = "Hospital",            Zona = 2 },
        new() { Id = 8,  Nombre = "Biblioteca",          Zona = 1 },
        new() { Id = 9,  Nombre = "Museo",               Zona = 2 },
        new() { Id = 10, Nombre = "Parque Norte",        Zona = 2 },
        new() { Id = 11, Nombre = "Estadio",             Zona = 3 },
        new() { Id = 12, Nombre = "Aeropuerto",          Zona = 3 },
        new() { Id = 13, Nombre = "Polígono Industrial", Zona = 3 },
        new() { Id = 14, Nombre = "Centro Comercial",    Zona = 2 },
        new() { Id = 15, Nombre = "Puerto",              Zona = 2 },
        new() { Id = 16, Nombre = "Playa",               Zona = 3 },
        new() { Id = 17, Nombre = "Instituto",           Zona = 2 },
        new() { Id = 18, Nombre = "Cementerio",          Zona = 3 },
        new() { Id = 19, Nombre = "Terminal de Autobuses", Zona = 1 },
        new() { Id = 20, Nombre = "Teatro",              Zona = 1 },
        new() { Id = 21, Nombre = "Piscina",             Zona = 2 },
        new() { Id = 22, Nombre = "Mirador",             Zona = 3 },
        new() { Id = 23, Nombre = "Barrio Viejo",        Zona = 1 },
        new() { Id = 24, Nombre = "Parque Sur",          Zona = 2 },   // sin servicio
        new() { Id = 25, Nombre = "Juzgados",            Zona = 2 },   // sin servicio
    };

    // 7 vehículos: el nº 7 es de reserva y no tiene ninguna línea asignada.
    public static readonly List<Vehiculo> Vehiculos = new()
    {
        new() { Id = 1, Matricula = "1021-KLM", Capacidad = 40 },
        new() { Id = 2, Matricula = "2034-NPR", Capacidad = 60 },
        new() { Id = 3, Matricula = "3057-RST", Capacidad = 50 },
        new() { Id = 4, Matricula = "4082-VWX", Capacidad = 80 },
        new() { Id = 5, Matricula = "5016-BCD", Capacidad = 30 },
        new() { Id = 6, Matricula = "6049-FGH", Capacidad = 25 },
        new() { Id = 7, Matricula = "7063-JKL", Capacidad = 45 },
    };

    // La línea 6 es corta y no tiene ningún viaje registrado.
    public static readonly List<Linea> Lineas = new()
    {
        new() { Id = 1, Nombre = "Circular Centro",    VehiculoId = 1 },
        new() { Id = 2, Nombre = "Norte-Hospital",     VehiculoId = 2 },
        new() { Id = 3, Nombre = "Aeropuerto Express", VehiculoId = 3 },
        new() { Id = 4, Nombre = "Costera",            VehiculoId = 4 },
        new() { Id = 5, Nombre = "Cinturón",           VehiculoId = 5 },
        new() { Id = 6, Nombre = "Lanzadera Estadio",  VehiculoId = 6 },
    };

    public static readonly List<ParadaLinea> Recorridos = ConstruirRecorridos();

    public static readonly List<Viaje> Viajes = GenerarViajes();

    // ---------------------------------------------------------------

    private static List<ParadaLinea> ConstruirRecorridos()
    {
        var res = new List<ParadaLinea>();

        // (parada, minutos desde la parada anterior). La primera parada lleva 0.
        void Add(int linea, params (int parada, int min)[] tramos)
        {
            int orden = 1;
            foreach (var t in tramos)
                res.Add(new ParadaLinea
                {
                    LineaId = linea,
                    ParadaId = t.parada,
                    Orden = orden++,
                    MinutosDesdeAnterior = t.min
                });
        }

        Add(1, (1, 0), (2, 4), (3, 3), (4, 5), (5, 4), (8, 3), (23, 6));
        Add(2, (19, 0), (1, 6), (2, 4), (6, 7), (7, 5), (10, 6), (17, 8), (9, 9));
        Add(3, (12, 0), (19, 18), (1, 6), (2, 4), (14, 8), (13, 7), (11, 6));
        Add(4, (16, 0), (15, 5), (3, 8), (2, 4), (20, 3), (21, 6), (9, 7));
        Add(5, (22, 0), (18, 6), (10, 9), (14, 7), (4, 6));
        Add(6, (22, 0), (11, 10), (12, 6));

        return res;
    }

    private static List<Viaje> GenerarViajes()
    {
        uint estado = 20261015;
        double Azar()
        {
            estado = estado * 1103515245u + 12345u;
            return ((estado >> 8) & 0xFFFF) / 65536.0;
        }

        // Ocupación media aproximada de cada línea (la 6 no tiene viajes).
        var ocupacion = new Dictionary<int, double>
        {
            [1] = 0.82, [2] = 0.55, [3] = 0.78, [4] = 0.45, [5] = 0.30
        };
        var horas = new[] { (7, 30), (12, 0), (17, 30), (21, 0) };

        // (día, línea, franja) con viaje sin pasajeros
        var vacios = new HashSet<(int, int, int)> { (2, 5, 3), (6, 5, 0), (3, 4, 3), (6, 2, 3) };

        int Capacidad(int lineaId) =>
            Vehiculos.First(v => v.Id == Lineas.First(l => l.Id == lineaId).VehiculoId).Capacidad;

        var lunes = new DateTime(2026, 10, 5);
        var lista = new List<Viaje>();

        for (int dia = 0; dia < 7; dia++)
        {
            for (int linea = 1; linea <= 5; linea++)
            {
                int cap = Capacidad(linea);
                for (int h = 0; h < horas.Length; h++)
                {
                    double ruido = (Azar() - 0.5) * 0.4;
                    int pax = vacios.Contains((dia, linea, h))
                        ? 0
                        : Math.Clamp((int)Math.Round(cap * (ocupacion[linea] + ruido),
                                                     MidpointRounding.AwayFromZero), 0, cap);
                    lista.Add(new Viaje
                    {
                        LineaId = linea,
                        Fecha = lunes.AddDays(dia).AddHours(horas[h].Item1).AddMinutes(horas[h].Item2),
                        Pasajeros = pax
                    });
                }
            }

            // Servicios de refuerzo de la línea 1 de lunes a viernes
            if (dia < 5)
            {
                foreach (var (hh, mm) in new[] { (8, 15), (18, 15) })
                {
                    double ruido = (Azar() - 0.5) * 0.4;
                    int cap = Capacidad(1);
                    lista.Add(new Viaje
                    {
                        LineaId = 1,
                        Fecha = lunes.AddDays(dia).AddHours(hh).AddMinutes(mm),
                        Pasajeros = Math.Clamp((int)Math.Round(cap * (0.95 + ruido),
                                                               MidpointRounding.AwayFromZero), 0, cap)
                    });
                }
            }
        }

        var ordenada = lista.OrderBy(v => v.Fecha).ThenBy(v => v.LineaId).ToList();
        for (int i = 0; i < ordenada.Count; i++) ordenada[i].Id = i + 1;
        return ordenada;
    }
}
