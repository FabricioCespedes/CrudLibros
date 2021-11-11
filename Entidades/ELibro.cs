using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ELibro
    {
        string claveLibro;

        string titulo;

        string claveAutor;

        ECategoria claveCategoria;

        bool existe ;

        public ELibro()
        {
            claveLibro = string.Empty;
            titulo = string.Empty;
            claveAutor = string.Empty;
            claveCategoria = new ECategoria();
            existe = false;
        }

        public ELibro(string claveLibro, string titulo, string claveAutor, ECategoria claveCategoria, bool existe)
        {
            this.claveLibro = claveLibro;
            this.titulo = titulo;
            this.claveAutor = claveAutor;
            this.claveCategoria = claveCategoria;
            this.existe = existe;
        }

        public string ClaveLibro { get => claveLibro; set => claveLibro = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string ClaveAutor { get => claveAutor; set => claveAutor = value; }
        public ECategoria ClaveCategoria { get => claveCategoria; set => claveCategoria = value; }
        public bool Existe { get => existe; set => existe = value; }
    }
}
