namespace DO
{
    /// <summary>
    /// Describes the ability to be deleted
    /// </summary>
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
