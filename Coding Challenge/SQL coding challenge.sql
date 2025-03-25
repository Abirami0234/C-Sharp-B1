create database CarrerHub
use CarrerHub

create table companies(
companyid int identity(1001,1) primary key ,
companyname varchar(255) not null,
location varchar(255) not null
)

create table jobs(
jobid int identity(2001,1) primary key ,
companyid int references companies(companyid),
jobtitle varchar(255) not null,
jobdescription text,
joblocation varchar(255) not null,
salary decimal(10,2) check (salary >= 0),
jobtype varchar(50),
posteddate datetime default current_timestamp,
)

create table applicants(
applicantid int identity(3001,1) primary key,
firstname varchar(100) not null,
lastname varchar(100) not null,
email varchar(255) unique not null,
phone varchar(20) not null,
resume text
)

create table applications(
applicationid int identity(4001,1) primary key,
jobid int references jobs(jobid),
applicantid int references applicants(applicantid),
applicationdate datetime default current_timestamp,
coverletter text,
)

insert into companies(companyname, location) values
('Google', 'California'),
('Microsoft', 'Washington'),
('Amazon', 'Seattle'),
('Facebook', 'California'),
('Apple', 'LA'),
('Tesla', 'Texas'),
('Netflix', 'Los Angeles'),
('Adobe', 'San Jose'),
('Intel', 'Oregon'),
('IBM', 'New York')

select * from companies

insert into jobs (companyid, jobtitle, jobdescription, joblocation, salary, jobtype) values
(1001, 'Software Engineer', 'Develop and maintain software solutions.', 'California', 120000, 'Full-time'),
(1002, 'Data Scientist', 'Analyze data and build machine learning models.', 'Washington', 110000, 'Full-time'),
(1003, 'Cloud Engineer', 'Manage cloud-based infrastructure.', 'Seattle', 100000, 'Full-time'),
(1004, 'UI/UX Designer', 'Design user-friendly interfaces.', 'California', 95000, 'Full-time'),
(1005, 'Product Manager', 'Oversee product development.', 'LA', 130000, 'Full-time'),
(1006, 'AI Researcher', 'Work on cutting-edge AI projects.', 'Texas', 150000, 'Full-time'),
(1007, 'Cybersecurity Analyst', 'Protect company systems from threats.', 'Los Angeles', 105000, 'Full-time'),
(1008, 'DevOps Engineer', 'Automate deployment pipelines.', 'San Jose', 98000, 'Full-time'),
(1009, 'Embedded Systems Engineer', 'Develop hardware-software integrations.', 'Oregon', 102000, 'Full-time'),
(1010, 'Database Administrator', 'Manage and optimize databases.', 'New York', 95000, 'Full-time')

select * from jobs


insert into applicants (firstname, lastname, email, phone, resume) values
('John', 'Doe', 'john.doe@mail.com', '9191919191', 'Resume of John Doe'),
('Alice', 'Smith', 'alice.smith@mail.com', '9192939192', 'Resume of Alice Smith'),
('Bob', 'Johnson', 'bob.johnson@mail.com', '9293919392', 'Resume of Bob Johnson'),
('Charlie', 'Brown', 'charlie.brown@mail.com', '9292929292', 'Resume of Charlie Brown'),
('David', 'Wilson', 'david.wilson@mail.com', '9393939393', 'Resume of David Wilson'),
('Emma', 'Taylor', 'emma.taylor@mail.com', '9491959294', 'Resume of Emma Taylor'),
('Frank', 'White', 'frank.white@mail.com', '9494949494', 'Resume of Frank White'),
('Grace', 'Lee', 'grace.lee@mail.com', '9594929193', 'Resume of Grace Lee'),
('Mary', 'Fox', 'mary.fox@mail.com', '9192939495', 'Resume of Mary Fox'),
('Ian', 'day', 'ian.day@mail.com', '9798999799', 'Resume of Ian Day')

select * from applicants

insert into applications (jobid, applicantid, coverletter) values
(2001, 3001, 'I am excited about this role'),
(2002, 3002, 'I have experience in this field'),
(2003, 3003, 'Looking forward to joining your team'),
(2004, 3004, 'I am a perfect fit for this job'),
(2005, 3005, 'I have relevant experience in product management'),
(2006, 3006, 'Passionate about AI research'),
(2007, 3007, 'Cybersecurity is my expertise'),
(2008, 3008, 'Experienced in DevOps automation'),
(2009, 3009, 'Strong background in embedded systems'),
(2010, 3010, 'Database optimization is my specialty')

select * from applications

-- 5.
select j.jobtitle, count(a.applicationid) as applicationcount 
from jobs j
left join applications a on j.jobid = a.jobid
group by j.jobtitle

--6.
select j.jobtitle, c.companyname, j.joblocation, j.salary 
from jobs j
join companies c on j.companyid = c.companyid
where j.salary between 
(select min(salary) from jobs)and(select max(salary) from jobs)

--7.
select j.jobtitle, c.companyname, a.applicationdate 
from applications a
join jobs j on a.jobid = j.jobid
join companies c on j.companyid = c.companyid
where a.applicantid = 3003

--8.
select avg(salary) as AvgSalary from jobs where salary > 0

--9.
select top 1 c.companyname, count(j.jobid) as jobcount 
from companies c
join jobs j on c.companyid = j.companyid
group by c.companyid, c.companyname
order by jobcount desc;

--10.
alter table applicants add experience int not null default 0

update applicants set experience = 3 where applicantid = 3001
update applicants set experience = 5 where applicantid = 3002
update applicants set experience = 8 where applicantid = 3003
update applicants set experience = 2 where applicantid = 3004
update applicants set experience = 7 where applicantid = 3005
update applicants set experience = 4 where applicantid = 3006
update applicants set experience = 6 where applicantid = 3007
update applicants set experience = 1 where applicantid = 3008
update applicants set experience = 9 where applicantid = 3009
update applicants set experience = 0 where applicantid = 3010

select * from applicants


select a.*
from applicants a
join applications ap on a.applicantid = ap.applicantid
join jobs j on ap.jobid = j.jobid
join companies c on j.companyid = c.companyid
where c.location = 'Texas' and a.experience >= 3

--11.
select distinct jobtitle 
from jobs 
where salary between 60000 and 80000 --is null because the min salary is 95000

select distinct jobtitle 
from jobs 
where salary between 90000 and 110000

--12.
select jobtitle 
from jobs 
where jobid not in (select jobid from applications)
-- is null because each applicant has applied for each job

--13.
select a.firstname, a.lastname, c.companyname, j.jobtitle 
from applications ap
join applicants a on ap.applicantid = a.applicantid
join jobs j on ap.jobid = j.jobid
join companies c on j.companyid = c.companyid

--14.
select c.companyname, count(j.jobid) as jobcount 
from companies c
left join jobs j on c.companyid = j.companyid
group by c.companyid, c.companyname

--15.
select a.firstname, a.lastname, c.companyname, j.jobtitle 
from applicants a 
left join applications ap on a.applicantid = ap.applicantid 
left join jobs j on ap.jobid = j.jobid 
left join companies c on j.companyid = c.companyid

--16.
select distinct c.companyname 
from companies c
join jobs j on c.companyid = j.companyid
where j.salary > (select avg(salary) from jobs)

--17.
alter table applicants add city varchar(100)
alter table applicants add state varchar(100)

update applicants set city = 'Chennai', state = 'Tamil Nadu' where applicantid = 3001
update applicants set city = 'Bangalore', state = 'Karnataka' where applicantid = 3002
update applicants set city = 'Hyderabad', state = 'Telangana' where applicantid = 3003
update applicants set city = 'Mumbai', state = 'Maharashtra' where applicantid = 3004
update applicants set city = 'Pune', state = 'Maharashtra' where applicantid = 3005
update applicants set city = 'Delhi', state = 'Delhi' where applicantid = 3006
update applicants set city = 'Kolkata', state = 'West Bengal' where applicantid = 3007
update applicants set city = 'Chennai', state = 'Tamil Nadu' where applicantid = 3008
update applicants set city = 'Bangalore', state = 'Karnataka' where applicantid = 3009
update applicants set city = 'Hyderabad', state = 'Telangana' where applicantid = 3010

select * from applicants
--17.
select firstname, lastname, concat(city, ', ', state) as location 
from applicants

--18.
select jobtitle 
from jobs 
where jobtitle like '%Developer%' or jobtitle like '%Engineer%'

--19.
select a.firstname, a.lastname, j.jobtitle
from applicants a
left join applications ap on a.applicantid = ap.applicantid
left join jobs j on ap.jobid = j.jobid
union all
select null, null, jobtitle
from jobs
where jobid not in (select jobid from applications)

--20.
select a.firstname, a.lastname, c.companyname 
from applicants a
cross join companies c
where c.location = 'Seattle' and a.experience > 2

