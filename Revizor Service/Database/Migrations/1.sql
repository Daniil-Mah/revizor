create table Role(
    Id serial primary key,
    Name varchar(50) not null unique
);

create table Users(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) unique not null ,
    Password Text not null,
    RoleId int references Role(Id) not null,
    CreatedAt timestamp default now(),
    RefreshToken varchar(50) null,
    RefreshExpireTime timestamp null
);

create table Facility (
    Id uuid primary key default gen_random_uuid(),
    Title varchar (150) not null,
    Location varchar(150) not null,

    CONSTRAINT uq_title_location UNIQUE (title, location)
);


create table Plan(
    Id uuid primary key default gen_random_uuid(),
    Name varchar(100) not null unique,
    CreatedAt timestamp not null default now(),
    CreatedBy uuid references Users(Id) not null,
    PeriodStart timestamp not null,
    PeriodEnd timestamp not null default (now()::date + Interval '1 months'), 
    IsArchived boolean not null default false
);
create unique index idx_only_true on Plan(IsArchived) where (IsArchived = true);

create table Record(
    Id uuid primary key default gen_random_uuid(),
    UserId uuid references Users(Id) not null,
    FacilityId uuid references Facility(Id) not null,
    StartAt timestamp not null,
    EndAt timestamp not null,
    PassageWay float null,
    CreatedAt timestamp not null default now()
);

create table PlanAssigment(
    Id uuid primary key default gen_random_uuid(),
    PlanId uuid references Plan(id) on delete cascade,
    FacilityId uuid references Facility(Id), 
    PassAt timestamp not null, 
    PassBy uuid references Users(Id),
    ResponsiblePerson uuid references Users(Id)
);
