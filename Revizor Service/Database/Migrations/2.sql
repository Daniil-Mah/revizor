alter table plan 
add PlanId uuid references Plan(Id) not null;