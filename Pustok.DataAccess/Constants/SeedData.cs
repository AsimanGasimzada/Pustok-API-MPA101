namespace Pustok.DataAccess.Constants;

public static class SeedData
{
    public static Status PendingStatus = new()
    {
        Id = Guid.Parse("85df16d7-6781-4b6a-bbe1-d0cf0fb643f0"),
        Name = "Pending"
    };

    public static Status DoneStatus = new()
    {
        Id = Guid.Parse("c775937b-22b8-4398-b0e1-051ff025a7c0"),
        Name = "Done"
    };

}
