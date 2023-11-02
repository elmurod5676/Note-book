using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneNote
{
    public partial class Form1 : Form
    {
        DataTable notes = new DataTable();
        bool editing = false;
        int rowIndex = -1;

        public Form1()
        {
            InitializeComponent();

            notes.Columns.Add("Title");
            notes.Columns.Add("Note");

            previousNotes.DataSource = notes;

            CleanTextBoxes();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.No;

            if (!string.IsNullOrWhiteSpace(titleBox.Text) &&
                    !string.IsNullOrWhiteSpace(noteBox.Text) &&
                    editing)
            {
                dialogResult = MessageBox.Show("You have some unsaved info. do you want to save them ?", "Confirm", MessageBoxButtons.YesNo);
            }


            if (dialogResult == DialogResult.No)
            {

                if (rowIndex >= 0 &&
                        notes.Rows.Count > rowIndex)
                {
                    notes.Rows[rowIndex].Delete();
                    rowIndex = -1;
                    editing = false;
                }
                else
                    Console.WriteLine("Not a valid note");
            }
            else
            {
                notes.Rows[rowIndex]["Title"] = titleBox.Text;
                notes.Rows[rowIndex]["Note"] = noteBox.Text;

                CleanTextBoxes();

                rowIndex = -1;
                editing = false;
            }
        }
    

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (rowIndex >= 0 && notes.Rows.Count > rowIndex)
            {
                titleBox.Text = "" + notes.Rows[rowIndex][0];
                noteBox.Text = "" + notes.Rows[rowIndex][1];
            }
            else
                MessageBox.Show("Qator tanlanmagan !");
        }

        private void CleanTextBoxes()
        {
            titleBox.Text = string.Empty;
            noteBox.Text = string.Empty;
        }

        private void newnoteButton_Click(object sender, EventArgs e)
        {
            CleanTextBoxes();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (editing)
            {
                if (!string.IsNullOrWhiteSpace(titleBox.Text) &&
                       !string.IsNullOrWhiteSpace(noteBox.Text))
                {
                    notes.Rows[rowIndex]["Title"] = titleBox.Text;
                    notes.Rows[rowIndex]["Note"] = noteBox.Text;

                    CleanTextBoxes();

                    rowIndex = -1;
                    editing = false;
                }
                else
                {
                    MessageBox.Show("Ma'lumotlarni to'liq kiriting !");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(titleBox.Text) &&
                    !string.IsNullOrWhiteSpace(noteBox.Text))
                    notes.Rows.Add(titleBox.Text, noteBox.Text);

                CleanTextBoxes();

                editing = false;
            }

        }

        private void previousNotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            rowIndex = previousNotes.CurrentCell.RowIndex;

            if (rowIndex >= 0 && notes.Rows.Count > rowIndex)
                editing = true;
        }

        private void ExitLabel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
