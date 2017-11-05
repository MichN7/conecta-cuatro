using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conectaCuatro
{
    public partial class Tablero : Form
    {
        private Rectangle[] columnas;
        private int[,] tablero;
        private int turno;

        public Tablero()
        {
            InitializeComponent();
            this.columnas = new Rectangle[7];
            this.tablero = new int[6, 7];
            this.turno = 1;
        }

        /*METODOS Y EVENTOS PARA LOS GRAFICOS(TABLERO)*/

        //Evento que pinta el tablero usando un rectangulo y ellipses
        private void Tablero_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.BlueViolet, 24, 24, 340, 300);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0) {
                        this.columnas[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }
            }
        }

        //Evento que pinta un elipse dependiendo del click en el tablero
        private void Tablero_MouseClick(object sender, MouseEventArgs e)
        {
            int columnaIndex = this.numeroColumna(e.Location);
            if(columnaIndex != -1)
            {
                int filaIndex = this.filaVacia(columnaIndex);
                if(filaIndex != -1)
                {
                    this.tablero[filaIndex, columnaIndex] = this.turno;
                    if(this.turno == 1)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Black, 32 + 48 * columnaIndex, 32 + 48 * filaIndex, 32, 32);

                        if(jugadorGanador(this.turno)!= -1 )
                        {
                            MessageBox.Show("Jugador " + this.turno + " ha ganado!");
                            Application.Restart();
                        }
                        else
                        {
                            this.turno = 2;
                        }
                       
 
                    }else if(this.turno == 2)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Blue, 32 + 48 * columnaIndex, 32 + 48 * filaIndex, 32, 32);
                        if (jugadorGanador(this.turno) != -1)
                        {
                            MessageBox.Show("Jugador " + this.turno + " ha ganado!");
                            Application.Restart();
                        }
                        else
                        {
                            this.turno = 1;
                        }
                    }
                    
                }
            }
        }

        //Regres el numero de columna clickeada
        private int numeroColumna(Point mouse) {
            for (int i = 0; i < this.columnas.Length; i++)
            {
                if((mouse.X >= this.columnas[i].X) && (mouse.Y >= this.columnas[i].Y))
                {
                    if((mouse.X <= this.columnas[i].X + this.columnas[i].Width) && (mouse.Y <= this.columnas[i].Y + this.columnas[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        //Checa si la fila esta vacía, si esta vacía regresa la posición de la fila
        private int filaVacia(int columna) {
            for (int i = 5; i >= 0; i--)
            {
                if(this.tablero[i,columna] == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        //metodos del juego
            //jugador1 = 1
            //jugador2 = 2
        
        private int jugadorGanador(int jugador)
        {
            //checa si jugador ha ganado en forma vertical
            for (int fila = 0; fila < this.tablero.GetLength(0) - 3; fila++)
            {
                for (int columna = 0; columna < this.tablero.GetLength(1); columna++)
                {
                    if (NumerosIgualA(jugador, this.tablero[fila, columna], this.tablero[fila + 1, columna], this.tablero[fila + 2, columna], this.tablero[fila + 3, columna]))
                    {
                        return jugador;
                    }
                }
            }

            //checa si ganador ha ganado en forma horizontal
            for (int fila = 0; fila < this.tablero.GetLength(0); fila++)
            {
                for (int columna = 0; columna < this.tablero.GetLength(1) -3; columna++)
                {
                    if (NumerosIgualA(jugador, this.tablero[fila, columna], this.tablero[fila, columna + 1], this.tablero[fila, columna + 2], this.tablero[fila, columna + 3]))
                    {
                        return jugador;
                    }
                }
            }

            //checa si jugador ha ganado diagonalmente
            for (int fila = 0; fila < this.tablero.GetLength(0) - 3; fila++)
            {
                for (int columna = 0; columna < this.tablero.GetLength(1) - 3; columna++)
                {
                    if (NumerosIgualA(jugador, this.tablero[fila, columna], this.tablero[fila +1, columna + 1], this.tablero[fila + 2, columna + 2], this.tablero[fila + 3, columna + 3]))
                    {
                        return jugador;
                    }
                }
            }

            for (int fila = 0; fila < this.tablero.GetLength(0) - 3; fila++)
            {
                for (int columna = 3; columna < this.tablero.GetLength(1); columna++)
                {
                    if (NumerosIgualA(jugador, this.tablero[fila, columna], this.tablero[fila + 1, columna - 1], this.tablero[fila + 2, columna - 2], this.tablero[fila + 3, columna - 3]))
                    {
                        return jugador;
                    }
                }
            }

            return -1;
        }

        private bool NumerosIgualA(int numCheckar, params int[] numeros)
        {
            foreach (int numero in numeros)
            {
                if (numero != numCheckar)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
