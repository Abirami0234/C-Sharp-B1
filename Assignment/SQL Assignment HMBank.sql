--Task.1.

create database HMBank
use HMBank

create table customers (
customer_id int primary key,
first_name varchar(50) not null,
last_name varchar(50) not null,
dob date not null,
email varchar(100) unique,
phone_number varchar(15) unique,
address varchar(255) not null
)
 create table accounts (
 account_id int primary key,
customer_id int references customers(customer_id),
account_type varchar(20) check (account_type in ('savings', 'current', 'zero_balance')),
balance decimal(12,2)
 )

 create table transactions(
 transaction_id int primary key,
 account_id int references accounts(account_id),
 transaction_type varchar(20) check (transaction_type in ('deposit', 'withdrawal', 'transfer')),
 amount decimal(12, 2), 
 transaction_date date
 )


	

--Task.2.


--2.1

insert into customers values
(1101, 'John', 'Doe', '1990-05-15', 'john.doe@example.com', '9876543210', '123 main st, city'),
(1102, 'Alice', 'Smith', '1985-09-20', 'alice.smith@example.com', '8765432109', '456 elm st, town'),
(1103, 'Bob', 'Shnson', '1992-03-10', 'bob.johnson@example.com', '7654321098', '789 oak st, village'),
(1104, 'Emma', 'Brown', '1995-07-25', 'emma.brown@example.com', '6543210987', '101 pine st, county'),
(1105, 'Michael', 'White', '1988-11-30', 'michael.white@example.com', '5432109876', '202 cedar st, district'),
(1106, 'Sophia', 'Lee', '1993-06-18', 'sophia.lee@example.com', '4321098765', '303 birch st, suburb'),
(1107, 'David', 'Martin', '1991-02-22', 'david.martin@example.com', '3210987654', '404 willow st, town'),
(1108, 'Olivia', 'Clark', '1987-09-14', 'olivia.clark@example.com', '2109876543', '505 maple st, city'),
(1109, 'James', 'Walker', '1996-04-03', 'james.walker@example.com', '1098765432', '606 cherry st, district'),
(1110, 'Lily', 'Harris', '1994-12-07', 'lily.harris@example.com', '1987654321', '707 spruce st, suburb'),
(1111, 'William', 'Roberts', '1986-08-29', 'william.roberts@example.com', '2876543210', '808 fir st, county'),
(1112, 'Ava', 'Adams', '1997-01-15', 'ava.adams@example.com', '3765432109', '909 ash st, city'),
(1113, 'Benjamin', 'Nelson', '1990-10-20', 'benjamin.nelson@example.com', '4654321098', '111 poplar st, town'),
(1114, 'Rose', 'Baker', '1989-05-22', 'rose.baker@example.com', '5543210987', '222 sycamore st, village'),
(1115, 'Ethan', 'Hall', '1992-07-05', 'ethan.hall@example.com', '6432109876', '333 chestnut st, suburb')

select * from customers 

insert into accounts values 
(2101, 1101, 'savings', 5000.00),
(2102, 1102, 'current', 12000.50),
(2103, 1103, 'savings', 7500.75),
(2104, 1104, 'zero_balance', 0.00),
(2105, 1105, 'savings', 10200.25),
(2106, 1106, 'current', 8500.00),
(2107, 1107, 'savings', 5400.40),
(2108, 1108, 'zero_balance', 0.00),
(2109, 1109, 'current', 13300.90),
(2110, 1110, 'savings', 6200.30),
(2111, 1111, 'savings', 4700.50),
(2112, 1112, 'zero_balance', 0.00),
(2113, 1113, 'current', 9900.60),
(2114, 1114, 'savings', 2500.75),
(2115, 1115, 'current', 11000.00)

select * from accounts
delete from accounts
where account_id between 21001 and 21009

insert into transactions values
(3101, 2101, 'deposit', 2000.00, '2025-03-15'),
(3102, 2102, 'withdrawal', 1500.50, '2025-03-15'),
(3103, 2103, 'deposit', 5000.75, '2025-03-15'),
(3104, 2104, 'deposit', 3000.00, '2025-03-15'),
(3105, 2105, 'withdrawal', 2200.25, '2025-03-15'),
(3106, 2106, 'transfer', 5000.00, '2025-03-16'),
(3107, 2107, 'deposit', 1000.40, '2025-03-16'),
(3108, 2108, 'withdrawal', 500.00, '2025-03-16'),
(3109, 2109, 'deposit', 7000.90, '2025-03-16'),
(3110, 2110, 'transfer', 3200.30, '2025-03-16'),
(3111, 2111, 'withdrawal', 1800.50, '2025-03-17'),
(3112, 2112, 'deposit', 6500.00, '2025-03-17'),
(3113, 2113, 'transfer', 4000.60, '2025-03-17'),
(3114, 2114, 'withdrawal', 1500.75, '2025-03-17'),
(3115, 2115, 'deposit', 9000.00, '2025-03-17')

 select * from transactions

/* delete from transactions
where transaction_id between 3101 and 3115*/

--2.2.1

select first_name + ' ' + last_name as full_name, 
       account_type, 
       email
from customers c
join accounts a on c.customer_id = a.customer_id

--2.2.2

	select c.first_name + ' ' + c.last_name as full_name, 
       a.account_id, 
       a.account_type, 
       t.transaction_id, 
       t.transaction_type, 
       t.amount, 
       t.transaction_date
from customers c
join accounts a on c.customer_id = a.customer_id
join transactions t on a.account_id = t.account_id
order by c.customer_id, t.transaction_date

--2.2.3

select account_id, balance, balance + 5000 as new_balance
from accounts
where account_id = 2108

--2.2.4

select customer_id, first_name + ' ' + last_name as full_name 
from customers

--2.2.5

delete from accounts 
where balance = 0 and account_type = 'savings'

--2.2.6

select * from customers 
where address like '%main st%'

--2.2.7

select account_id, balance 
from accounts 
where account_id = 2109

--2.2.8

select * from accounts 
where account_type = 'current' and balance > 1000

--2.2.9

select * from transactions 
where account_id = 2110

--2.2.10

select account_id, balance, (balance * 0.05) as interest_accrued, account_type
from accounts 
where account_type = 'savings'

--2.2.11

select * from accounts 
where balance < 100

--2.2.12

select * from customers 
where address not like '%oak st%'


--Task.3.


--3.1

select avg(balance) as avg_balance 
from accounts

--3.2

select top 10 * from accounts 
order by balance desc 

--3.3

select sum(amount) as total_deposits 
from transactions 
where transaction_type = 'deposit' and transaction_date = '2025-03-15'

--3.4

select top 1 * from customers 
order by dob asc 
 -- Oldest customer top 1

select top 1 * from customers 
order by dob desc 
-- Newest customer


--3.5

select t.*, a.account_type 
from transactions t 
join accounts a on t.account_id = a.account_id

--3.6

select c.customer_id, 
       c.first_name + ' ' + c.last_name as full_name, 
       c.email, 
       c.phone_number, 
       a.account_id, 
       a.account_type, 
       a.balance
from customers c
join accounts a on c.customer_id = a.customer_id


--3.7

select c.customer_id, c.first_name + ' ' + c.last_name as full_name, c.email,
c.phone_number,t.transaction_id, t.account_id, t.transaction_type, t.amount, t.transaction_date 
from transactions t
join accounts a on t.account_id = a.account_id
join customers c on a.customer_id = c.customer_id
where t.account_id = 2105

--3.8

select customer_id, count(account_id) as account_count
from accounts
group by customer_id
having count(account_id) > 1

--3.9

select d.account_id, 
       d.total_deposits - w.total_withdrawals as net_transaction
from 
    (select account_id, sum(amount) as total_deposits 
     from transactions 
     where transaction_type = 'deposit' 
     group by account_id) d
left join 
    (select account_id, sum(amount) as total_withdrawals 
     from transactions 
     where transaction_type = 'withdrawal' 
     group by account_id) w
on d.account_id = w.account_id;

--3.10
--Task. 4

--4.1
select c.customer_id, first_name + ' ' + last_name as full_name, balance 
from customers c 
join accounts a on c.customer_id = a.customer_id 
where balance = (select max(balance) from accounts)

--4.2
select customer_id, avg(balance) as avg_balance
from accounts
where customer_id in (select customer_id from accounts group by customer_id having count(account_id) > 1)
group by customer_id

--4.3
select * from transactions
where amount > (select avg(amount) from transactions)

--4.4
select * from customers
where customer_id not in (select distinct customer_id from accounts a join transactions t on a.account_id = t.account_id)

--4.5
select sum(balance) as total_balance
from accounts
where account_id not in (select distinct account_id from transactions)

--4.6
select * from transactions
where account_id in (select account_id from accounts where balance = (select min(balance) from accounts))

--4.7
select customer_id, count(distinct account_type) as account_type_count
from accounts
group by customer_id
having count(distinct account_type) > 1

--4.8
select account_type, count(*) * 100.0 / (select count(*) from accounts) as percentage
from accounts
group by account_type

--4.9
select t.*
from transactions t
join accounts a on t.account_id = a.account_id
where a.customer_id = 1106

--4.10
select sum(balance) as total_balance from accounts









