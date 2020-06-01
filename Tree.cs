using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOver
{
    public class Tree : Form
    {
        DataGridView dataGridView1;
        public DataGridViewCell thisNodeCell;
        List<Tree> movesCell = new List<Tree>();// все возможные ходы
        public List<DataGridViewCell> listSelectedPath = new List<DataGridViewCell>();
        public List<List<DataGridViewCell>> shelders = new List<List<DataGridViewCell>>();
        int count;//переменная отвечающая за то, сколько ещё ячеек нужно до составления пары
        string value;//значение выбранной ячейки
        public int countMatchingCells = 0;

        int IndexRowMain;
        int IndexColMain;

        public Tree(DataGridView dv, DataGridViewCell cell, int _count, string _value, int Row, int Col)
        {
            dataGridView1 = dv;
            thisNodeCell = cell;
            count = _count;
            value = _value;
            IndexColMain = Col;
            IndexRowMain = Row;
            listSelectedPath = new List<DataGridViewCell>();
            
        }

        public Tree(DataGridView dv, DataGridViewCell cell, int _count, string _value)
        {

            dataGridView1 = dv;
            thisNodeCell = cell;
            count = _count;
            value = _value;
            IndexColMain = cell.ColumnIndex;
            IndexRowMain = cell.RowIndex;
            listSelectedPath = new List<DataGridViewCell>();

        }

        //функция для отыскания единственного возможного пути для соединения ячеек
        public void AllChild(List<DataGridViewCell> moves, List<DataGridViewCell> _Moves, ref List<List<DataGridViewCell>> list)
        {

            if (movesCell.Count() != 0 )
            {
                moves.Add(thisNodeCell);
                if (count == 2 && _Moves.Count() == 0)
                {
                    _Moves.Clear();
                    Console.WriteLine("Приравнинвание ");
                    for (int i = 0; i < moves.Count; i++) {
                        _Moves.Add(moves[i]);
                    }
                    _Moves.Add(movesCell[0].thisNodeCell);
                    list.Add(_Moves);

                }
                if (count == 2 && _Moves.Count() > 0) {
                    List<DataGridViewCell> Moves_ = new List<DataGridViewCell>();
                    for (int i = 0; i < moves.Count; i++)
                    {
                        Moves_.Add(moves[i]);
                    }
                    Moves_.Add(movesCell[0].thisNodeCell);
                    list.Add(Moves_);
                }
                /*if (count == 2 && _Moves.Count() > 0)
                {
                    _Moves.RemoveRange(0, _Moves.Count() - 1);
                    for (int i = 0; i < moves.Count; i++) _Moves.Add(moves[i]);
                    _Moves.Add(movesCell[0].thisNodeCell);

                }*/
                for (int i = 0; i < movesCell.Count(); i++)
                {
                    movesCell[i].AllChild(moves, _Moves, ref list);
                }
                moves.Remove(thisNodeCell);


            }

        }
        //функция для проверки на то, что путь соединения ячеек один
        public void LastNode(ref int countLast)
        {
            if (movesCell.Count() != 0)
            {
                if (count == 2 && movesCell.Count() == 1)
                {

                    ++countLast;
                    Console.WriteLine("Добавление" + countLast);
                }
                Console.WriteLine("Раскрытие детей");
                for (int i = 0; i < movesCell.Count(); i++)
                {
                    movesCell[i].LastNode(ref countLast);
                }
            }
        }
        //функция для вычисления всех путей, которые можно построить от выбранной ячейки
        public void CalculatingMoves(DataGridViewCell parent)
        {

            if (count > 2)
            {

                for (int i = -1; i < 2; i++)
                {

                    if (i != 0)
                    {

                        if (thisNodeCell.RowIndex + i >= 0 && thisNodeCell.RowIndex + i < this.dataGridView1.RowCount)
                        {

                            //если ячейка пустая и у нее белый цвет фона
                            if (this.dataGridView1.Rows[thisNodeCell.RowIndex + i].Cells[thisNodeCell.ColumnIndex].Value == null && this.dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + i].Style.BackColor != Color.Black)
                            {


                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + (thisNodeCell.RowIndex + i) + ", " + thisNodeCell.ColumnIndex);
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + parent.RowIndex + ", " + parent.ColumnIndex);
                                if (thisNodeCell.RowIndex + i != parent.RowIndex)
                                {

                                    int c = count - 1;
                                    Tree child = new Tree(dataGridView1, this.dataGridView1.Rows[thisNodeCell.RowIndex + i].Cells[thisNodeCell.ColumnIndex], c, value, IndexRowMain, IndexColMain);
                                    child.CalculatingMoves(thisNodeCell);
                                    movesCell.Add(child);
                                }
                            }
                        }
                        if (thisNodeCell.ColumnIndex + i >= 0 && thisNodeCell.ColumnIndex + i < this.dataGridView1.ColumnCount)
                        {
                            Console.WriteLine("Count moves " + movesCell.Count());
                            //если ячейкая пустая и белого цвета
                            if (this.dataGridView1[thisNodeCell.ColumnIndex + i, thisNodeCell.RowIndex].Value == null && dataGridView1[thisNodeCell.ColumnIndex + i, thisNodeCell.RowIndex].Style.BackColor != Color.Black)
                            {
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + (thisNodeCell.RowIndex) + ", " + (thisNodeCell.ColumnIndex + i));
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + parent.RowIndex + ", " + parent.ColumnIndex);
                                if (thisNodeCell.ColumnIndex + i != parent.ColumnIndex)
                                {
                                    int c = count - 1;
                                    Tree child = new Tree(dataGridView1, this.dataGridView1.Rows[thisNodeCell.RowIndex].Cells[thisNodeCell.ColumnIndex + i], c, value, IndexRowMain, IndexColMain);
                                    child.CalculatingMoves(thisNodeCell);
                                    movesCell.Add(child);
                                }
                            }
                        }

                    }

                }

            }
            //если это предпоследний ход 
            //то проверяется, есть ли вокруг свободная ячейка с нужным значение 
            if (count == 2)
            {

                for (int j = -1; j < 2; j++)
                {
                    if (j != 0 && thisNodeCell.RowIndex + j >= 0 && thisNodeCell.RowIndex + j < this.dataGridView1.RowCount)
                    {
                        Console.WriteLine("----------" + count + ", " + value);
                        Console.WriteLine("То что проверяется" + (thisNodeCell.RowIndex + j) + ", " + thisNodeCell.ColumnIndex);
                        if (dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Value != null)
                        {

                            if ((thisNodeCell.ColumnIndex != IndexColMain || thisNodeCell.RowIndex + j != IndexRowMain) && dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Style.BackColor != Color.Black && dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Value.ToString() == value)
                            {

                                Console.WriteLine("Последний ход добавление");
                                Console.WriteLine("Индексы ячейки от которой проверяется " + thisNodeCell.RowIndex + ", " + thisNodeCell.ColumnIndex);

                                Tree child = new Tree(dataGridView1, dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j], 1, value);
                                movesCell.Add(child);

                            }
                        }

                    }
                    if (j != 0 && thisNodeCell.ColumnIndex + j >= 0 && thisNodeCell.ColumnIndex + j < this.dataGridView1.RowCount)
                    {
                        Console.WriteLine("----------" + count + ", " + value);
                        Console.WriteLine("То что проверяется " + (thisNodeCell.RowIndex) + ", " + (thisNodeCell.ColumnIndex + j));
                        if (dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Value != null)
                            if ((thisNodeCell.ColumnIndex + j != IndexColMain || thisNodeCell.RowIndex != IndexRowMain) && dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Style.BackColor != Color.Black && dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Value.ToString() == value)
                            {

                                Console.WriteLine("Последний ход добавление");
                                Console.WriteLine("Индексы ячейки от которой проверяется " + thisNodeCell.RowIndex + ", " + thisNodeCell.ColumnIndex);
                                Tree child = new Tree(dataGridView1, dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex], 1, value);
                                movesCell.Add(child);


                            }
                    }
                }

            }


        }





        public void CalculatingMoves2(DataGridViewCell parent, int flag)
        {

            if (count > 2)
            {

                for (int i = -1; i < 2; i++)
                {

                    if (i != 0)
                    {

                        if (thisNodeCell.RowIndex + i >= 0 && thisNodeCell.RowIndex + i < this.dataGridView1.RowCount)
                        {

                            //если ячейка пустая и у нее белый цвет фона
                            if (this.dataGridView1.Rows[thisNodeCell.RowIndex + i].Cells[thisNodeCell.ColumnIndex].Value == null )
                            {


                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + (thisNodeCell.RowIndex + i) + ", " + thisNodeCell.ColumnIndex);
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + parent.RowIndex + ", " + parent.ColumnIndex);
                                if (thisNodeCell.RowIndex + i != parent.RowIndex)
                                {

                                    int c = count - 1;
                                    Tree child = new Tree(dataGridView1, this.dataGridView1.Rows[thisNodeCell.RowIndex + i].Cells[thisNodeCell.ColumnIndex], c, value, IndexRowMain, IndexColMain);
                                    child.CalculatingMoves2(thisNodeCell, flag);
                                    movesCell.Add(child);
                                }
                            }
                        }
                        if (thisNodeCell.ColumnIndex + i >= 0 && thisNodeCell.ColumnIndex + i < this.dataGridView1.ColumnCount)
                        {
                            Console.WriteLine("Count moves " + movesCell.Count());
                            //если ячейкая пустая и белого цвета
                            if (this.dataGridView1[thisNodeCell.ColumnIndex + i, thisNodeCell.RowIndex].Value == null )
                            {
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + (thisNodeCell.RowIndex) + ", " + (thisNodeCell.ColumnIndex + i));
                                Console.WriteLine("gfgfgfgfgfgfgfgfg " + parent.RowIndex + ", " + parent.ColumnIndex);
                                if (thisNodeCell.ColumnIndex + i != parent.ColumnIndex)
                                {
                                    int c = count - 1;
                                    Tree child = new Tree(dataGridView1, this.dataGridView1.Rows[thisNodeCell.RowIndex].Cells[thisNodeCell.ColumnIndex + i], c, value, IndexRowMain, IndexColMain);
                                    child.CalculatingMoves2(thisNodeCell, flag);
                                    movesCell.Add(child);
                                }
                            }
                        }

                    }

                }

            }
            //если это предпоследний ход 
            //то проверяется, есть ли вокруг свободная ячейка с нужным значение 
            if (count == 2)
            {

                for (int j = -1; j < 2; j++)
                {
                    if (j != 0 && thisNodeCell.RowIndex + j >= 0 && thisNodeCell.RowIndex + j < this.dataGridView1.RowCount)
                    {
                        Console.WriteLine("----------" + count + ", " + value);
                        Console.WriteLine("То что проверяется" + (thisNodeCell.RowIndex + j) + ", " + thisNodeCell.ColumnIndex);
                        if (dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Value != null)
                        {
                            Color color;
                            if (flag == 1) color = Color.White;
                            else color = Color.Black;
                            if ((thisNodeCell.ColumnIndex != IndexColMain || thisNodeCell.RowIndex + j != IndexRowMain) && dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Value.ToString() == value && dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j].Style.BackColor == color)
                            {

                                Console.WriteLine("Последний ход добавление");
                                Console.WriteLine("Индексы ячейки от которой проверяется " + thisNodeCell.RowIndex + ", " + thisNodeCell.ColumnIndex);

                                Tree child = new Tree(dataGridView1, dataGridView1[thisNodeCell.ColumnIndex, thisNodeCell.RowIndex + j], 1, value);
                                movesCell.Add(child);

                            }
                        }

                    }
                    if (j != 0 && thisNodeCell.ColumnIndex + j >= 0 && thisNodeCell.ColumnIndex + j < this.dataGridView1.RowCount)
                    {
                        Console.WriteLine("----------" + count + ", " + value);
                        Console.WriteLine("То что проверяется " + (thisNodeCell.RowIndex) + ", " + (thisNodeCell.ColumnIndex + j));
                        if (dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Value != null)
                        {
                            Color color;
                            if (flag == 1) color = Color.White;
                            else color = Color.Black;
                            if ((thisNodeCell.ColumnIndex + j != IndexColMain || thisNodeCell.RowIndex != IndexRowMain) && dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Value.ToString() == value && dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex].Style.BackColor == color)
                            {

                                Console.WriteLine("Последний ход добавление");
                                Console.WriteLine("Индексы ячейки от которой проверяется " + thisNodeCell.RowIndex + ", " + thisNodeCell.ColumnIndex);
                                Tree child = new Tree(dataGridView1, dataGridView1[thisNodeCell.ColumnIndex + j, thisNodeCell.RowIndex], 1, value);
                                movesCell.Add(child);


                            }
                        }
                    }
                }

            }


        }

    }
}
