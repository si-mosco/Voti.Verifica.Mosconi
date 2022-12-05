using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Mosconi
{
    public class Elenco
    {
        //attributi
        private Verifica[] _lista = new Verifica[999];
        private int _lungh=0;

        //costruttori
        public Elenco()
        {

        }

        //properties
        public Verifica[] Lista
        {
            set
            {
                _lista = value;
            }
            get
            {
                return _lista;
            }
        }
        public int Lungh
        {
            set
            {
                _lungh = value;
            }
            get
            {
                return _lungh;
            }
        }

        //metodi
        public void Aggiunta(Verifica verifica)
        {
            bool fatto = false;

            if (Lista[Lungh] == null)
            {
                Lista[Lungh] = verifica;
                fatto = true;
                Lungh++;
            }
            if (!fatto)
                throw new Exception("Aggiunta non eseguita per: elenco pieno");
        }

        public string[] Visualizza()
        {
            string[] listaStringa = new string[Lungh];

            try
            {
                for (int i = 0; i < Lungh; i++)
                {
                    //listaStringa[i] = Lista[i].ToString();
                    listaStringa[i] = $"{Lista[i].Id};{Lista[i].Voto};{Lista[i].Materia};{Lista[i].Data}";
                }
            }
            catch
            {
                throw new Exception($"Riferimento ad un oggetto, che si riferisce a null");
            }

            return listaStringa;
        }

        public float Media(string Materia)
        {
            float votoTot = 0;
            int nVoti = 0;

            for (int i = 0; i < Lungh; i++)
            {
                if (Lista[i].Materia == Materia)
                {
                    votoTot += Lista[i].Voto;
                    nVoti++;
                }
            }

            if (nVoti != 0)
            {
                return votoTot / nVoti;
            }
            else
            {
                throw new Exception($"Voti non presenti con questa materia: {Materia}");
            }
        }
    }
}
