namespace AnkiNet;

public record AnkiNote
{
    public long Id { get; set; }
    public long NoteTypeId { get; set; }
    public string[] Fields { get; set; }
    public string Guid { get; set; } = System.Guid.NewGuid().ToString().Substring(0, 10);

    // TODO Pass a AnkiNoteType to check the number of fields match, or check in AnkiCollection

    //public AnkiNote(long noteTypeId, params string[] fields)
    //{
    //    Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    //    NoteTypeId = noteTypeId;
    //    Fields = fields;
    //}
}
