List<string> filas = new List<string>();
int totalA = 0;
int totalB = 0;
int totalC = 0;
int totalReprobados = 0;
string seguir = "s";
while (seguir == "s")
{
    // 1. Pedir los datos
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Apellido: ");
    string apellido = Console.ReadLine();

    Console.Write("Nota 1: ");
    double nota1 = double.Parse(Console.ReadLine());

    Console.Write("Nota 2: ");
    double nota2 = double.Parse(Console.ReadLine());

    Console.Write("Nota 3: ");
    double nota3 = double.Parse(Console.ReadLine());

    Console.Write("Nota 4: ");
    double nota4 = double.Parse(Console.ReadLine());

    double promedio = (nota1 + nota2 + nota3 + nota4) / 4;

    string literal;
    if (promedio >= 90)
    {
        literal = "A";
        totalA++;
    }
    else if (promedio >= 80)
    {
        literal = "B";
        totalB++;
    }
    else if (promedio >= 70)
    {
        literal = "C";
        totalC++;
    }
    else
    {
        literal = "D";
        totalReprobados++;
    }
    string fila = nombre.PadRight(12) + apellido.PadRight(12)
                + nota1.ToString().PadRight(7) + nota2.ToString().PadRight(7)
                + nota3.ToString().PadRight(7) + nota4.ToString().PadRight(7)
                + promedio.ToString("F1").PadRight(10) + literal;
    filas.Add(apellido.ToUpper() + ";" + fila);
    Console.Write("¿Desea ingresar otro estudiante? (s/n): ");
    seguir = Console.ReadLine().ToLower();
    Console.WriteLine();
}
filas.Sort();
Console.WriteLine("Colegio Dios es bueno.");
Console.WriteLine("Calificaciones del cuatrimestre");
Console.WriteLine("==========================================================================");
Console.WriteLine("Nombre".PadRight(12) + "Apellido".PadRight(12) + "Nota1".PadRight(7)
                + "Nota2".PadRight(7) + "Nota3".PadRight(7) + "Nota4".PadRight(7)
                + "Promedio".PadRight(10) + "Literal");
Console.WriteLine("==========================================================================");

foreach (string linea in filas)
{
    int posicion = linea.IndexOf(';');
    Console.WriteLine(linea.Substring(posicion + 1));
}


Console.WriteLine("==========================================================================");
Console.WriteLine("Estudiantes en A: " + totalA);
Console.WriteLine("Estudiantes en B: " + totalB);
Console.WriteLine("Estudiantes en C: " + totalC);
Console.WriteLine("Estudiantes en Reprobados: " + totalReprobados);