create extension if not exists "uuid-ossp";

insert into client (id, name, age)
VALUES
(uuid_generate_v4(),'Client 2',20),
(uuid_generate_v4(),'Client 3',22),
(uuid_generate_v4(),'Client 4',23),
(uuid_generate_v4(),'Client 5',24),
(uuid_generate_v4(),'Client 6',25),
(uuid_generate_v4(),'Client 7',26),
(uuid_generate_v4(),'Client 8',27),
(uuid_generate_v4(),'Client 9',28),
(uuid_generate_v4(),'Client 10',29);

insert into employee (id, name, salary)
VALUES
(uuid_generate_v4(),'Employee 2',2324),
(uuid_generate_v4(),'Employee 3',2232),
(uuid_generate_v4(),'Employee 4',2324),
(uuid_generate_v4(),'Employee 5',2234),
(uuid_generate_v4(),'Employee 6',2436),
(uuid_generate_v4(),'Employee 7',2632),
(uuid_generate_v4(),'Employee 8',2743),
(uuid_generate_v4(),'Employee 9',2835),
(uuid_generate_v4(),'Employee 10',2932);

insert into account(id, name, amount, client_id)
values
(uuid_generate_v4(),'USD',2,'5572ee8c-4acc-47fa-b735-eba7990ad34d'),
(uuid_generate_v4(),'USD',1,'77a0eed2-c32b-4ea3-b303-5eb8006741a3'),
(uuid_generate_v4(),'USD',1,'a1e8cb6f-3e9b-41b4-a1fb-8cf988703435'),
(uuid_generate_v4(),'USD',1,'548860c3-81b9-4c66-b66e-ebd8a9c9bb20'),
(uuid_generate_v4(),'MDL',3,'77a0eed2-c32b-4ea3-b303-5eb8006741a3'),
(uuid_generate_v4(),'MDL',1,'a1e8cb6f-3e9b-41b4-a1fb-8cf988703435'),
(uuid_generate_v4(),'RUB',1,'77a0eed2-c32b-4ea3-b303-5eb8006741a3'),
(uuid_generate_v4(),'RUB',2,'548860c3-81b9-4c66-b66e-ebd8a9c9bb20'),
(uuid_generate_v4(),'RUB',1,'a1e8cb6f-3e9b-41b4-a1fb-8cf988703435');


select client_id, amount
from account
where amount>100
order by amount asc;


select min(amount)
from account;


select sum(amount)
from account;



select c.name, a.id
from account a
join client c on c.id = a.client_id;


select name, age
from client
order by age DESC;


select count (*) as count
from client
where age=20;

select age
from client
group by age;

select *
from client
limit 5;

