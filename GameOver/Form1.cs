using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOver
{
    public partial class Form1 : Form
    {
        int _click_column = 0; //столбец ячейки, на которую нажал пользователь
        int _click_row = 0; //строка ячейки, на которую нажал пользователь
        int flag = -1; // флаг
     
        Color colorBack = Color.Black; // изнаальный цвет, которым будут выделяться ячейки
        Color colorText = Color.White; // изначальный цвет текста, когда ячейки будут выделяться

        
        public Form1()
        {
            InitializeComponent();
            
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;//устанавливаем цвет выделения ячеек белым
            Colums();//вызываем функцию, которая создаёт поле 
            ReadFile();//вызываем функцию, которая читает данные из файла

            
        }
        //Функция для CheckedListBox, которая вызывается, когда в ней выбирается другой элемент
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если отмечено больше 2 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox1.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }

            if (checkedListBox1.SelectedIndex == 1) //Проверяем условие, если отмечен второй элемент "Ластик"
            {
                colorBack = Color.White;//цвет выделения ячеек меняется на белый
                colorText = Color.Black;//цвет текста меняется на чёрный
            }
            //если условие не выполняется
            else {
                colorBack = Color.Black;//цвет выделения ячеек немяется на чёрный
                colorText = Color.White;//цвет текста меняется на белый
            }
        }
        //функция для чтения из файла
        public void ReadFile() {

            Random random = new Random();
            int x = random.Next(1, 4);
            string nameFile = "Game" + x + ".txt";
            FileStream file = new FileStream(nameFile, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            int k = 0; // переменная для определения строки в поле 
            string text; 
            while ((text = reader.ReadLine()) != null) { //чтение по строчно
                k = k+1;
                string[] splitLine = text.Split(' '); // разделитель 
                for (int i = 0; i < splitLine.Length; i++) {
                    if (splitLine[i] != "0") {
                        dataGridView1.Rows[k].Cells[i].Value = splitLine[i];


                    }
                    if (splitLine[i] == "1" ) {
                        dataGridView1.Rows[k].Cells[i].Style.BackColor = Color.Black;
                        dataGridView1.Rows[k].Cells[i].Style.ForeColor = Color.White;
                    }
                }
            }
            reader.Close();
            file.Close();

        }
        //функция для разбиения поля на ячейки 
        public void Colums() {

            int k = this.dataGridView1.Size.Width / 30;
            System.Windows.Forms.DataGridViewColumn[] f = new System.Windows.Forms.DataGridViewColumn[] { };
            for (int i = 0; i < k; i++) {
                System.Windows.Forms.DataGridViewTextBoxColumn co = new System.Windows.Forms.DataGridViewTextBoxColumn();
                co.Width = 30;
                this.dataGridView1.Columns.Add(co);
            }
            k = this.dataGridView1.Size.Height / 30;

            for (int i = 0; i < k - 1; i++) {

                this.dataGridView1.Rows.Add();
            }
        }
        
        // функция при нажатии левой кнопки мыши на ячейку 
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //сохраняются данные нажатой ячейки 
            _click_column = e.ColumnIndex;
            _click_row = e.RowIndex;
           // dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            flag = - flag;//поднятие и опускание флага
            dataGridView1.ClearSelection();//очищение выделенных ячеек
        }
        //функция выделения ячеек после нажатия 
        public void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flag == 1)//проверка флага
            {
                System.Drawing.Color color = System.Drawing.Color.Black;//цвет фона ячеек меняется на черный 
                string value_cell = "1";//определение значения ячейки 
                switch (dataGridView1.Rows[_click_row].Cells[_click_column].Value) {
                    case "2":
                        color = System.Drawing.Color.Yellow;
                        value_cell = "2";
                        break;
                    case "3":
                        color = System.Drawing.Color.Purple;
                        value_cell = "3";
                        break;
                    case "4":
                        color = System.Drawing.Color.Blue;
                        value_cell = "4";
                        break;
                    case "5":
                        color = System.Drawing.Color.LightCoral;
                        value_cell = "5";
                        break;
                    case "6":
                        color = System.Drawing.Color.Red;
                        value_cell = "6";
                        break;
                    case "7":
                        color = System.Drawing.Color.LightGreen;
                        value_cell = "7";
                        break;
                    case "8":
                        color = System.Drawing.Color.LightGreen;
                        value_cell = "8";
                        break;
                    case "9":
                        color = System.Drawing.Color.LightGreen;
                        value_cell = "9";
                        break;
                }
                
                   
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;//выделяем первую ячейку 
                dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Black;
                dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
                if (colorBack == Color.White) {
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                    dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                int flag2 = 1;
                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++) {
                    if (dataGridView1.SelectedCells[i].Style.BackColor == Color.Black) {
                        flag2 = -1;
                    }
                }
                    try
                {
                    if (flag2 != -1  || colorBack == Color.White ) 
                    if (dataGridView1.SelectedCells.Count == int.Parse(value_cell) && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == value_cell)
                    {
                        
                        for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                        {
                            dataGridView1.SelectedCells[i].Style.BackColor = colorBack;
                            dataGridView1.SelectedCells[i].Style.ForeColor = colorText;

                        }

                    }
                }
                catch {
                    
                }

                
            }
        }

      
    }

        
    }

