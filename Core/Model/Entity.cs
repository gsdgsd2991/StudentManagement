using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Core.Model
{
    public class Entity:IDel
    {
        public bool isDeleted { get; set; }
        [Key]
        public int ID{get;set;}
    }
}
