//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AzuredbConnection
{
    using System;
    using System.Collections.Generic;
    
    public partial class EstudiantesGrupos
    {
        public int EstGrupoID { get; set; }
        public string EstudianteID { get; set; }
        public Nullable<int> GrupoID { get; set; }
    
        public virtual Estudiante Estudiante { get; set; }
        public virtual Grupos Grupos { get; set; }
    }
}
