Console.WriteLine("Ingrese Dos valores: ");
double a = double.Parse(Console.ReadLine());
double b = double.Parse(Console.ReadLine());

Console.WriteLine("La suma de esos valores es: " + (a + b));
Console.WriteLine("La resta de esos valores es: " + (a - b));
Console.WriteLine("La multiplicacion de esos valores es: " + (a * b));
if (b != 0)
{
    Console.WriteLine("La division de esos valores es: " + (a / b));
}
else
{
    Console.WriteLine("No se puede dividir por cero.");
}
Console.WriteLine("La division de esos valores es: " + (a /b));
Console.WriteLine("La raiz cuadrada del primer valor es: " + Math.Sqrt(a));
Console.WriteLine("La raiz cuadrada del segundo valor es: " + Math.Sqrt(b));