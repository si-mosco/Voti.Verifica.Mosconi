using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosconi
{
    public class Verifica
    {
        //attributi
        private int _id; //PK
        private string _materia;
        private float _voto;
        private string _data;

        //costruttori
        public Verifica(string materia, float voto, string data)
        {
            
            Materia = materia;
            Voto = voto;
            Data = data;
        }
        public Verifica()
        {
        }

        //properties
        public int Id
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }

        public string Materia
        {
            set
            {
                _materia = value;
            }
            get
            {
                return _materia;
            }
        }

        public float Voto
        {
            set
            {
                _voto = value;
            }
            get
            {
                return _voto;
            }
        }

        public string Data
        {
            set
            {
                _data = value;
            }
            get
            {
                return _data;
            }
        }

        //metodi
        public string ToString()
        {
            return $"{Id};{Materia};{Voto};{Data}";
        }
    }
}
