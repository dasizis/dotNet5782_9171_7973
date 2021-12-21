namespace DO
{
    /// <summary>
    /// An interface which describe the ability to be deleted
    /// </summary>
    interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
