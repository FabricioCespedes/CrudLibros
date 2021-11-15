using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EEjemplar
    {
        string claveEjemplar;

        ELibro eLibro;

        ECondicion eCondicion;

        EEstado eEstado;

        EEditorial eEditorial;

        string edicion;

        int numeroPaginas;

        public EEjemplar(string claveEjemplar, ELibro eLibro, ECondicion eCondicion, EEstado eEstado, EEditorial eEditorial, string edicion, int numeroPaginas)
        {
            this.claveEjemplar = claveEjemplar;
            this.eLibro = eLibro;
            this.eCondicion = eCondicion;
            this.eEstado = eEstado;
            this.eEditorial = eEditorial;
            this.edicion = edicion;
            this.numeroPaginas = numeroPaginas;
        }

        public EEjemplar()
        {
        }

        public string ClaveEjemplar { get => claveEjemplar; set => claveEjemplar = value; }
        public ELibro ELibro { get => eLibro; set => eLibro = value; }
        public string Edicion { get => edicion; set => edicion = value; }
        public int NumeroPaginas { get => numeroPaginas; set => numeroPaginas = value; }
        internal ECondicion ECondicion { get => eCondicion; set => eCondicion = value; }
        internal EEstado EEstado { get => eEstado; set => eEstado = value; }
        internal EEditorial EEditorial { get => eEditorial; set => eEditorial = value; }
    }
}
