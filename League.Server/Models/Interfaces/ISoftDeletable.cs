namespace League.Server.Models.Interfaces
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedAt { get; set; }
        string? DeletedReason { get; set; }
        string? DeletedByUserId { get; set; }
    }
}
