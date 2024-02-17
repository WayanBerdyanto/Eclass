namespace Eclass.Models;

public class EclassDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string StudentsCollectionName { get; set; } = null!;

    public string LecturerCollectionName { get; set; } = null!;

    public string SubjectCollectionName { get; set; } = null!;

    public string ElectiveCoursesCollectionName { get; set; } = null!;


}