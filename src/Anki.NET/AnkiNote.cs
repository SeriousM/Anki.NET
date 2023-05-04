using AnkiNet.mapper;

namespace AnkiNet;

public record AnkiNote
{
    public long Id { get; set; }
    public long NoteTypeId { get; set; }
    public string[] Fields { get; set; }
    public string Guid { get; set; } = ChecksumUtils.Checksum(System.Guid.NewGuid().ToString()) + "XX" /* 8 checksum + 2 fixed */;

    // TODO Pass a AnkiNoteType to check the number of fields match, or check in AnkiCollection

    //public AnkiNote(long noteTypeId, params string[] fields)
    //{
    //    Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    //    NoteTypeId = noteTypeId;
    //    Fields = fields;
    //}
}
