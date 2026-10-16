using System;

namespace Revizor_Service.Database.Models;

public class Record
{
    public Guid Id {get;set;}
    public Guid User {get;set;}
    public Guid Facility {get;set;}
    public DateTime StartAt {get;set;}
    public DateTime EndAt {get;set;}
    public float PassageWay {get;set;}
}
