﻿using API.Models;

namespace API.ViewModels.Universities;
    public class UniversityVM
    {
        public Guid? Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        /*public static UniversityVM ToVM(University university)
        {
            return new UniversityVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }*/
    }

