using CRM.Core.Domain.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.User
{
    public class CompleteRegistrationModel
    {


        [Required]
        public Gender GenderType { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Occupation { get; set; }
        [Required]
        public Education EducationType { get; set; }
        [Required]
        public Income IncomeAboveOrBelow80K { get; set; }
        [Required]
        public FamilyOrRelationship FamilyOrRelationshipType { get; set; }
        public string Reason { get; set; }

        public string HeaderImageUrl { get; set; }
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public int SrNo { get; set; }

    }
}
