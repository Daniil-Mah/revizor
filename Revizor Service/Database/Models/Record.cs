using System;

namespace Revizor_Service.Database.Models;

public class Record
{
    public Guid Id {get;set;}
    public Revizor Revizor {get;set;}
    public Facility Facility {get;set;}
    public DateTime StartAt {get;set;}
    public DateTime EndAt {get;set;}
    public DateTime Nigth {get;set;}
    public float PassageWay {get;set;}
}
