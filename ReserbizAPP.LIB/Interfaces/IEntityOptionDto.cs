namespace ReserbizAPP.LIB.Interfaces
{
    interface IEntityOptionDto
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsDelete { get; set; }
        bool IsActive { get; set; }
        bool CanBeSelected { get; }
    }
}