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

        public string ClaveLibro {get; set ;}
        public string Titulo {get; set;}
        public string ClaveAutor {get; set;}
        public ECategoria ClaveCategoria {get; set;}
        public bool Existe { get; set; }
    }
}
