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
