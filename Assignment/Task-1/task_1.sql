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


