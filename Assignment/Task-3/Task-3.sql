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
select account_id, avg(balance) as avg_daily_balance 
from accounts 
where account_id in 
    (select distinct account_id from transactions 
     where transaction_date >= dateadd(day, -30, getdate())) 
group by account_id

--3.11

select account_type, sum(balance) as 'Total balance'
from accounts
group by account_type

--3.12

select account_id, count(transaction_id) as transaction_count
from transactions
group by account_id
order by transaction_count desc

--3.13

select c.customer_id, c.first_name + ' ' + c.last_name as full_name, a.account_type, sum(a.balance) as total_balance
from accounts a
join customers c on a.customer_id = c.customer_id
group by c.customer_id, c.first_name, c.last_name, a.account_type
having sum(a.balance) > 10000

--3.14

select account_id, amount, transaction_date, count(*) as duplicate_count
from transactions
group by account_id, amount, transaction_date
having count(*) > 1

