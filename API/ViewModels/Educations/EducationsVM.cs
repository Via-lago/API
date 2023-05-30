using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.Educations;

public class EducationsVM
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    [Range(0,4)]
    public float GPA { get; set; }
    public Guid UniversityGuid { get; set; }

    public static EducationsVM ToVM(Education education)
    {
        return new EducationsVM
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }
}
