using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace JuegoSnake
{
    public class Snake
    {
        public enum Direccion { Derecha, Izquierda, Arriba, Abajo }

        public bool Vivo { get; set; }
        public ConsoleColor ColorCabeza { get; set; }
        public ConsoleColor ColorCuerpo { get; set; }
        public Ventana VentanaC { get; set; }
        public Comida ComidaC { get; set; }
        public List<Point> Cuerpo { get; set; }
        public Point Cabeza { get; set; }
        public Point PosicionInicial { get; set; }
        public Direccion DireccionActual { get; set; }
        public int Puntaje { get; set; }
        public int PuntajeMaximo { get; set; }

        private bool comiendo;

        public Snake(Point posicionInicial, ConsoleColor colorCabeza, ConsoleColor colorCuerpo, Ventana ventana, Comida comida)
        {
            PosicionInicial = posicionInicial;
            Cabeza = posicionInicial;
            ColorCabeza = colorCabeza;
            ColorCuerpo = colorCuerpo;
            VentanaC = ventana;
            ComidaC = comida;
            Cuerpo = new List<Point>();
            Puntaje = 0;
            PuntajeMaximo = 0;
            Vivo = true;
            DireccionActual = Direccion.Derecha;
        }

        public void Init()
        {
            Console.Clear();
            VentanaC.DibujarMarco();
            Cabeza = PosicionInicial;
            Cuerpo.Clear();
            IniciarCuerpo(2);
            Vivo = true;
            DireccionActual = Direccion.Derecha;
            ComidaC.GenerarComida(this);
        }

        public void IniciarCuerpo(int cantidad)
        {
            int x = Cabeza.X - 1;
            for (int i = 0; i < cantidad; i++)
            {
                Console.SetCursorPosition(x, Cabeza.Y);
                Console.Write("█");
                Cuerpo.Add(new Point(x, Cabeza.Y));
                x--;
            }
        }

        public void Mover()
        {
            Teclado();
            Point posAnteriorCabeza = Cabeza;
            MoverCabeza();
            MoverCuerpo(posAnteriorCabeza);
            ColisionesComida();

            if (ColisionesCuerpo())
            {
                Muerte();
                VentanaC.GameOver("G A M E   O V E R");
            }
        }

        private void MoverCabeza()
        {
            Console.ForegroundColor = ColorCabeza;
            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.Write(" ");

            switch (DireccionActual)
            {
                case Direccion.Derecha: Cabeza = new Point(Cabeza.X + 1, Cabeza.Y); break;
                case Direccion.Izquierda: Cabeza = new Point(Cabeza.X - 1, Cabeza.Y); break;
                case Direccion.Abajo: Cabeza = new Point(Cabeza.X, Cabeza.Y + 1); break;
                case Direccion.Arriba: Cabeza = new Point(Cabeza.X, Cabeza.Y - 1); break;
            }

            ColisionesMarco();

            Console.SetCursorPosition(Cabeza.X, Cabeza.Y);
            Console.WriteLine("█");
        }

        private void MoverCuerpo(Point posAnteriorCabeza)
        {
            Console.ForegroundColor = ColorCuerpo;
            Console.SetCursorPosition(posAnteriorCabeza.X, posAnteriorCabeza.Y);
            Console.WriteLine("█");
            Cuerpo.Insert(0, posAnteriorCabeza);

            if (comiendo)
            {
                comiendo = false;
                return;
            }

            Console.SetCursorPosition(Cuerpo[Cuerpo.Count - 1].X, Cuerpo[Cuerpo.Count - 1].Y);
            Console.Write(" ");
            Cuerpo.RemoveAt(Cuerpo.Count - 1);
        }

        private void Teclado()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.RightArrow && DireccionActual != Direccion.Izquierda)
                    DireccionActual = Direccion.Derecha;
                if (tecla.Key == ConsoleKey.LeftArrow && DireccionActual != Direccion.Derecha)
                    DireccionActual = Direccion.Izquierda;
                if (tecla.Key == ConsoleKey.UpArrow && DireccionActual != Direccion.Abajo)
                    DireccionActual = Direccion.Arriba;
                if (tecla.Key == ConsoleKey.DownArrow && DireccionActual != Direccion.Arriba)
                    DireccionActual = Direccion.Abajo;
            }
        }

        private void ColisionesMarco()
        {
            if (Cabeza.X <= VentanaC.LimiteSuperior.X) Cabeza = new Point(VentanaC.LimiteInferior.X - 1, Cabeza.Y);
            else if (Cabeza.X >= VentanaC.LimiteInferior.X) Cabeza = new Point(VentanaC.LimiteSuperior.X + 1, Cabeza.Y);

            if (Cabeza.Y <= VentanaC.LimiteSuperior.Y) Cabeza = new Point(Cabeza.X, VentanaC.LimiteInferior.Y - 1);
            else if (Cabeza.Y >= VentanaC.LimiteInferior.Y) Cabeza = new Point(Cabeza.X, VentanaC.LimiteSuperior.Y + 1);
        }

        private void ColisionesComida()
        {
            if (Cabeza == ComidaC.Posicion)
            {
                comiendo = true;
                Puntaje++;
                if (Puntaje > PuntajeMaximo) PuntajeMaximo = Puntaje;

                if (!ComidaC.GenerarComida(this))
                {
                    Vivo = false;
                    VentanaC.GameOver("J U E G O   C O M P L E T A D O");
                }
            }
        }

        private bool ColisionesCuerpo()
        {
            foreach (var parte in Cuerpo)
            {
                if (Cabeza == parte)
                {
                    Vivo = false;
                    return true;
                }
            }
            return false;
        }

        private void Muerte()
        {
            Console.ForegroundColor = ColorCuerpo;
            foreach (var parte in Cuerpo)
            {
                if (parte != Cabeza)
                {
                    Console.SetCursorPosition(parte.X, parte.Y);
                    Console.Write("░");
                    Thread.Sleep(120);
                }
            }
        }

        public void Informacion(int dist1, int dist2)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + dist1, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"Puntaje: {Puntaje}  ");

            Console.SetCursorPosition(VentanaC.LimiteSuperior.X + dist2, VentanaC.LimiteSuperior.Y - 1);
            Console.Write($"Puntaje Máximo: {PuntajeMaximo}  ");
        }
    }
}