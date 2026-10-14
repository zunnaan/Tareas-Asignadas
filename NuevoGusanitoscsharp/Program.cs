using System;
using System.Threading;

namespace JuegoSnake
{
    internal class Program
    {
        private static Ventana ventana;
        private static Snake snake;
        private static Comida comida;
        private static bool ejecucion = true;
        private static bool jugar = false;

        static void Main(string[] args)
        {
            Iniciar();
            Game();
        }

        private static void Iniciar()
        {
            ventana = new Ventana("Snake Game", 65, 20, ConsoleColor.Black, ConsoleColor.White, new System.Drawing.Point(5, 3), new System.Drawing.Point(59, 18));
            comida = new Comida(ConsoleColor.Green, ventana);
            snake = new Snake(new System.Drawing.Point(8, 4), ConsoleColor.Red, ConsoleColor.White, ventana, comida);
        }

        private static void Game()
        {
            while (ejecucion)
            {
                ventana.Menu();
                ventana.Teclado(ref ejecucion, ref jugar, snake);

                while (jugar)
                {
                    snake.Informacion(0, 34);
                    snake.Mover();

                    if (!snake.Vivo)
                    {
                        jugar = false;
                        snake.Puntaje = 0;
                    }

                    Thread.Sleep(100);
                }

                Thread.Sleep(100);
            }
        }
    }
}