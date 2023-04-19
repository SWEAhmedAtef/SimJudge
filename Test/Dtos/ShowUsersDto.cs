using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Test.Models;

namespace Test.Dtos
{
    public class ShowUsersDto
    {
        [Key]
        public int Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string NikeName{ get; set; }
        
        public string ProblemStatus { get; set; }
    }
}