using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Walnut.Entities
{
    //The purpouse of this class is to provide a way to associate some metadata
    //to a given entity (candiates, files, etc) at run-time without using regular table relationsships.
    //This approach will simplify the database design and will make the system more flexible for the user.
    //
    //A set of flags is called "ENTITY CIRCUNSTANCE", i.e. "Candidate Circunstance", "File Circunstance", etc.
    //The sistem will generate a list of all posible cirucntances (or flag combiantions) for  a given entity,
    //which is called "ENTITIES CIRCUNSTANCE LIST". Then a concrete process will be assigned to each entry on that list.
    //
    //the number of flags determines the PROCESS COMPLEXITY LEVEL, which can be calculated and shown to the user throught the UI.
    //Use a permutation C# library (like outstanding C# library) which provdes efficient algorithms for this class of problem
    public class EntityFlag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotMapped]
        public string Description { get { return FlagName + " (" + EntityName + ")"; } }

        [Required]
        [DisplayName("Flag Name")]
        public string FlagName { get; set; }

        // Brief explanation, dictionary-like, about this flag
        [Required]
        public string Definition { get; set; }

        [Required]
        [DisplayName("Destination Entity")]
        public string EntityName { get; set; }

        [DisplayName("Usage Remarks")]
        public string UsageRemarks { get; set; }

        // Es un indicador positivo, o indica algo positivo por si mismo sin necesidad
        // de estar asociado con tros indicadores? 
        [DisplayName("Positive Indicator?")]
        public bool IsPositive { get; set; }

        // Combinable: que puede ser considerado en permutaciones con otros indicadores.
        // Por ejemplo, un flag "Received" que indica que alg (fichero, factura, etc.) ha 
        // sido recibido, puede ser irrelevante en una combinacion. Valor = false exluira
        // el flag de cualquier combinacion, al objeto de simplificar los procesos.
        [DisplayName("Is combinable?")]
        public bool IsCombinable { get; set; }

        public byte[] Icon { get; set; }



    }
}