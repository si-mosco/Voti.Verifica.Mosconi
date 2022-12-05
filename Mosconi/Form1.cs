using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mosconi
{
    public partial class Form1 : Form
    {
        public Elenco elenco = new Elenco();
        public int identificatore;
        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("ID");
            listView1.Columns.Add("VOTO");
            listView1.Columns.Add("MATERIA");
            listView1.Columns.Add("DATA ");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;


            //CaricamentoInListview(elenco);
        }

        private void button1_Click(object sender, EventArgs e) //aggiunta
        {
            Verifica temp = new Verifica();
            bool controllino = false;
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                if (TrovaId() != -1)
                {
                    temp.Id = TrovaId();
                }
                try
                {
                    temp.Voto = float.Parse(textBox1.Text);
                }
                catch
                {
                    throw new Exception($"Voto non valido");
                }
                temp.Materia = textBox2.Text;
                try
                {
                    string controllo = textBox3.Text.Split('/')[0];
                    controllino = false;
                }
                catch
                {
                    throw new Exception($"Data non valida (es. 19/09/2021)");
                }
                if (!controllino)
                    temp.Data = textBox3.Text;

                elenco.Aggiunta(temp);
            }
            else
                throw new Exception($"Inserire tutti i campi");

            MessageBox.Show("Aggiunta eseguita");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e) //media
        {
            if (textBox4.Text != "")
            {
                MessageBox.Show($"La media è: {elenco.Media(textBox4.Text)}");
            }

            textBox4.Text = "";
        }

        public void CaricamentoInListview(Elenco elenco)
        {
            listView1.Items.Clear();

            string[] temp = elenco.Visualizza();

            for (int i = 0; i < temp.Length; i++)
            {
                try
                {
                    string[] cose = temp[i].Split(';');
                    string[] items2 = new string[cose.Length - 1];
                    for (int o = 0; o < items2.Length; o++)
                        items2[o] = cose[o];
                    ListViewItem riga = new ListViewItem(temp[i].Split(';'));
                    listView1.Items.Add(riga);
                }
                catch
                {
                    throw new Exception($"Stringa vuota");
                }
            }

        }

        private void button3_Click(object sender, EventArgs e) //ricarica
        {
            listView1.Items.Clear();
            CaricamentoInListview(elenco);
        }

        public int TrovaId()
        {
            for (int i = 0; i < elenco.Lista.Length; i++) //trova l'identificatore
            {
                if (elenco.Lista[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //modifica
        {
            int ind;
            try
            {
                ind = ricerca(elenco, int.Parse(textBox8.Text));

                MessageBox.Show($"{ind}");
            }
            catch
            {
                throw new Exception($"Id non valido");
            }

            bool controllino = false;
            if (ind != -1)
                elenco.Lista[ind].Id = ind;
            else
                throw new Exception($"Id non trovato");
            try
            {
                elenco.Lista[ind].Voto = float.Parse(textBox7.Text);
            }
            catch
            {
                throw new Exception($"Voto non valido");
            }
            elenco.Lista[ind].Materia = textBox6.Text;
            try
            {
                string controllo = textBox5.Text.Split('/')[0];
                controllino = false;
            }
            catch
            {
                throw new Exception($"Data non valida (es. 19/09/2021)");
            }
            if (!controllino)
                elenco.Lista[ind].Data = textBox5.Text;

            //elimina vecchio coso
            elenco.Aggiunta(elenco.Lista[ind]);

            MessageBox.Show("Modifica eseguita");
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }

        public int ricerca(Elenco elenco, int Id)
        {
            for (int i = 0; i < elenco.Lungh + 1; i++)
            {
                if (elenco.Lista[i].Id == Id)
                {
                    return i;
                }
            }
            return -1;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem == null) return;
            int listIndex = listView1.FocusedItem.Index;
            MessageBox.Show(Convert.ToString(listIndex));
        }

        private void button5_Click(object sender, EventArgs e)//elimina
        {
            int ind;
            try
            {
                ind = ricerca(elenco, int.Parse(textBox9.Text));
            }
            catch
            {
                throw new Exception($"Id non valido");
            }

            if (ind != -1)
            {
                elenco.Lista[ind] = null;

                MessageBox.Show("Eliminazione eseguita");
                elenco.Lungh -= 1;
                textBox9.Text = "";

                for (int i = ind; i < elenco.Lungh; i++)
                {
                    elenco.Lista[i] = elenco.Lista[i + 1];
                }
            }
            else
                throw new Exception($"Id non trovato");
        }

        private void button6_Click(object sender, EventArgs e)//ordina
        {
            for (int i = 0; i < elenco.Lungh - 1; i++) //ordina per anno
            {
                for (int j = i; j < elenco.Lungh; j++)
                {
                    if (int.Parse(elenco.Lista[j].Data.Split('/')[2]) < int.Parse(elenco.Lista[i].Data.Split('/')[2]))
                    {
                        Verifica temp = elenco.Lista[i];
                        elenco.Lista[i] = elenco.Lista[j];
                        elenco.Lista[j] = temp;
                    }
                    else if (int.Parse(elenco.Lista[j].Data.Split('/')[2]) == int.Parse(elenco.Lista[i].Data.Split('/')[2]))
                    {
                        //per mese

                        if (int.Parse(elenco.Lista[j].Data.Split('/')[1]) < int.Parse(elenco.Lista[i].Data.Split('/')[1]))
                        {
                            Verifica temp = elenco.Lista[i];
                            elenco.Lista[i] = elenco.Lista[j];
                            elenco.Lista[j] = temp;
                        }
                        else if (int.Parse(elenco.Lista[j].Data.Split('/')[1]) == int.Parse(elenco.Lista[i].Data.Split('/')[1]))
                        {
                            //per giorno
                            if (int.Parse(elenco.Lista[i].Data.Split('/')[0]) < int.Parse(elenco.Lista[j].Data.Split('/')[0]))
                            {
                                Verifica temp = elenco.Lista[j];
                                elenco.Lista[j] = elenco.Lista[i];
                                elenco.Lista[i] = temp;
                            }
                        }
                    }

                }
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
