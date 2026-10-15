// PRÁCTICA LINQ: SIMULADOR DE UNA RED DE TRANSPORTE PÚBLICO
//
// Cómo trabajar:
//  1. Sustituye cada línea "object? rXX = null;" por tu consulta: var rXX = ...;
//  2. Ejecuta (dotnet run) y compara tu salida con salida_esperada.txt.
//  3. Respeta el orden y el formato que pide cada ejercicio: si no, la salida no coincidirá.
//  4. Usa solo LINQ (nada de bucles for/foreach para resolver las consultas).
using System.Globalization;
using static Consola;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var paradas = Datos.Paradas;
var lineas = Datos.Lineas;
var recorridos = Datos.Recorridos;
var vehiculos = Datos.Vehiculos;
var viajes = Datos.Viajes;

Console.WriteLine("=========== BLOQUE 1: FILTRADO Y ORDENACIÓN ===========");
Console.WriteLine();
// 1.1 Paradas de la zona 2, ordenadas por nombre.
object? r11 = paradas.Where(p => p.Zona == 2).OrderBy(p => p.Nombre);
Mostrar("1.1 Paradas de la zona 2", r11);

// 1.2 Vehículos ordenados por capacidad ascendente (empate: por matrícula).
object? r12 = vehiculos.OrderBy(v => v.Capacidad).ThenBy(v => v.Matricula);
Mostrar("1.2 Vehículos por capacidad", r12);

// 1.3 Las cinco paradas con Id más alto, de mayor a menor.
object? r13 = paradas.OrderByDescending(p => p.Id).Take(5);
Mostrar("1.3 Cinco paradas con Id más alto", r13);

// 1.4 Nombres de las líneas (solo el texto) en orden alfabético.
object? r14 = lineas.OrderBy(l => l.Nombre).Select(l => l.Nombre);
Mostrar("1.4 Nombres de línea alfabéticos", r14);

// 1.5 Viajes del 2026-10-07, de más a menos pasajeros (empate: por Id).
var dia = new DateTime(2026, 10, 7);
object? r15 = viajes.Where(v => v.Fecha.Date == dia).OrderByDescending(v => v.Pasajeros).ThenBy(v => v.Id);
Mostrar("1.5 Viajes del 2026-10-07", r15);

Console.WriteLine("=========== BLOQUE 2: PROYECCIONES Y APLANAMIENTO ===========");
Console.WriteLine();

// 2.1 Nombres (solo texto) de las paradas de la línea 3, en orden de recorrido.
object? r21 = recorridos.Join(lineas.Where(l => l.Id == 3), r => r.LineaId, l => l.Id, (r, l) => r)
                     .OrderBy(r => r.Orden)
                     .Join(paradas, r => r.ParadaId, p => p.Id, (r, p) => p.Nombre);
Mostrar("2.1 Paradas de la línea 3", r21);

// 2.2 Nombres de todas las paradas con servicio, sin repetir, en orden alfabético.
//     Pista: resuélvelo con Distinct y, después, con DistinctBy.
object? r22 = null;
Mostrar("2.2 Paradas con servicio (sin repetir)", r22);

// 2.3 Para cada línea (orden por Id de línea): objeto anónimo { Linea, Zonas }
//     donde Linea es el nombre y Zonas es un texto como "1,2,3" (zonas distintas, ordenadas).
object? r23 = null;
Mostrar("2.3 Zonas por línea", r23);

// 2.4 Objetos anónimos { Linea, Orden, Parada } con todos los puntos de todos los
//     recorridos: por línea (Id) y, dentro de cada línea, por Orden. Parada = nombre.
//     Pista: SelectMany.
object? r24 = null;
Mostrar("2.4 Recorrido completo de todas las líneas", r24);

// 2.5 Paradas que aparecen en más de una línea: objetos { Parada, NumLineas },
//     de más a menos líneas (empate: por nombre de parada).
object? r25 = null;
Mostrar("2.5 Paradas compartidas por varias líneas", r25);

Console.WriteLine("=========== BLOQUE 3: COMBINACIÓN DE COLECCIONES ===========");
Console.WriteLine();

// 3.1 Cada línea con su vehículo: { Linea, Matricula, Capacidad } (orden por Id de línea).
//     Pista: Join.
object? r31 = null;
Mostrar("3.1 Línea y vehículo", r31);

// 3.2 TODAS las paradas (también las que no tienen servicio), por Id de parada:
//     { Parada, Lineas } con Lineas como texto "L1, L2" (ordenadas por Id de línea).
//     Pista: GroupJoin. Con Join, las paradas sin servicio desaparecerían.
object? r32 = null;
Mostrar("3.2 Líneas que pasan por cada parada", r32);

// 3.3 Recorrido de cada línea (orden por Id de línea): { Linea, Recorrido }
//     con Recorrido = "Parada A -> Parada B -> Parada C" (orden de recorrido).
object? r33 = null;
Mostrar("3.3 Recorrido de cada línea", r33);

// 3.4 Vehículos que no tienen ninguna línea asignada (los objetos Vehiculo completos).
object? r34 = null;
Mostrar("3.4 Vehículos sin línea", r34);

// 3.5 Informe de TODAS las líneas (orden por Id): { Linea, Matricula, Capacidad, NumViajes }.
//     Las líneas sin viajes deben salir con NumViajes = 0.
object? r35 = null;
Mostrar("3.5 Informe por línea", r35);

Console.WriteLine("=========== BLOQUE 4: AGRUPACIÓN Y ESTADÍSTICAS ===========");
Console.WriteLine();

// 4.1 Total de pasajeros por línea: { Linea, Total }, de mayor a menor total.
//     Solo aparecen las líneas que tienen viajes.
object? r41 = null;
Mostrar("4.1 Pasajeros totales por línea", r41);

// 4.2 Media de pasajeros por viaje de cada línea (orden por Id de línea): { Linea, Media }
//     con la media redondeada a 1 decimal y escrita como texto con ToString("F1").
object? r42 = null;
Mostrar("4.2 Media de pasajeros por viaje", r42);

// 4.3 La parada que pasa por más líneas: un solo objeto { Parada, NumLineas }.
object? r43 = null;
Mostrar("4.3 Parada con más líneas", r43);

// 4.4 Pasajeros por día, en orden cronológico: { Fecha, Pasajeros }
//     con Fecha como texto "yyyy-MM-dd" (ToString("yyyy-MM-dd")).
object? r44 = null;
Mostrar("4.4 Pasajeros por día", r44);

// 4.5 Líneas cuya ocupación media supera el 70 % de la capacidad de su vehículo:
//     { Linea, OcupacionMedia } de mayor a menor, con la ocupación en % y 1 decimal
//     (ToString("F1")). Ocupación de un viaje = 100 * Pasajeros / Capacidad.
object? r45 = null;
Mostrar("4.5 Líneas con ocupación media > 70 %", r45);

// 4.6 (AMPLIACIÓN) Vehículos infrautilizados: ocupación media < 40 % o sin viajes.
//     { Matricula, OcupacionMedia } (por Id de vehículo); si no tiene viajes, el texto "sin viajes".
//     Ojo: Average() sobre una secuencia vacía lanza una excepción.
object? r46 = null;
Mostrar("4.6 (ampliación) Vehículos infrautilizados", r46);

Console.WriteLine("=========== BLOQUE 5: CONSULTAS AVANZADAS ===========");
Console.WriteLine();

// 5.1a ¿Existe alguna línea sin viajes registrados? (bool)
object? r51a = null;
Mostrar("5.1a ¿Hay alguna línea sin viajes?", r51a);

// 5.1b ¿Todos los viajes respetan la capacidad del vehículo de su línea? (bool)
object? r51b = null;
Mostrar("5.1b ¿Todos los viajes respetan la capacidad del vehículo?", r51b);

// 5.2a Nombres de las paradas comunes a las líneas 1 y 3, en orden alfabético. Pista: Intersect.
object? r52a = null;
Mostrar("5.2a Paradas comunes a las líneas 1 y 3", r52a);

// 5.2b Nombres de las paradas de la línea 5 o de la 6, sin repetir, en orden alfabético. Pista: Union.
object? r52b = null;
Mostrar("5.2b Paradas de la línea 5 o de la 6 (sin repetir)", r52b);

// 5.3 Nombres de las paradas exclusivas de la línea 5 (ninguna otra línea pasa por ellas). Pista: Except.
object? r53 = null;
Mostrar("5.3 Paradas exclusivas de la línea 5", r53);

// 5.4 Crea un Dictionary<int, Parada> con todas las paradas (clave = Id).
//     Muestra su nº de entradas y la parada de clave 7.
Dictionary<int, Parada>? dic = null;
Mostrar("5.4 Diccionario de paradas",
        dic is null ? "(sin hacer)" : $"Entradas: {dic.Count}{Environment.NewLine}dic[7] = {dic[7]}");

// 5.5 Crea un HashSet<int> con los Id de las paradas que tienen servicio.
//     Muestra cuántas son y si contiene la parada 2 y la parada 24.
HashSet<int>? conServicio = null;
Mostrar("5.5 Paradas con servicio",
        conServicio is null
            ? "(sin hacer)"
            : $"Total: {conServicio.Count}{Environment.NewLine}¿Parada 2? {conServicio.Contains(2)}{Environment.NewLine}¿Parada 24? {conServicio.Contains(24)}");

// 5.6 (AMPLIACIÓN) Nombres de las paradas sin conexión directa con la parada 17 (Instituto),
//     sin contar la propia 17, en orden alfabético.
object? r56 = null;
Mostrar("5.6 (ampliación) Paradas sin conexión directa con la 17", r56);

// ---------- Reto final ----------
// Implementa los niveles en Planificador.cs. Aquí solo se ejecutan las pruebas.
PruebasPlanificador.Ejecutar();
