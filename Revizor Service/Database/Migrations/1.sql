create table Role(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(50) not null unique
);

create table User(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) not null unique,
    RoleId uuid references Role(Id) not null,
    Password Text not null,
    CreatedAt timestamp default now()
);

create table Facility (
    Id uuid primary key default gen_random_uuid(),
    Title varchar (150) not null,
    Location varchar(150) not null
);

 create table Plan(
    Id uuid primary key default gen_random_uuid(),
    src text not null
);

create table Record(
    Id uuid primary key default gen_random_uuid(),
    FacilityId uuid references Facility(Id) not null ,
    StartAt timestamp not null,
    EndAt timestamp not null,
    PassageWay float null,
    RevizorID uuid references User(Id) not null
);

create table ArchiveReports(
    Id uuid primary key default gen_random_uuid,
    Date timestamp not null,
    src text not null
);