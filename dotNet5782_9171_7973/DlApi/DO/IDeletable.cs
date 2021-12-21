namespace DO
{
    /// <summary>
    /// Describes the ability to be deleted
    /// </summary>
    interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
