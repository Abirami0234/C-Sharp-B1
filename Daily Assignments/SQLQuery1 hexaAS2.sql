create database HexaAs2
use HexaAs2

create table dept (
    deptno int primary key,
    dname varchar(20),
    loc varchar(20)
)
create table emp (
    empno int primary key,
    ename varchar(20),
    job varchar(20),
    mgr_id int null,
    hiredate date,
    sal int,
    comm int null,
	deptno int references dept(deptno)
)

insert into dept values (10, 'ACCOUNTING', 'NEW YORK')
insert into dept values (20, 'RESEARCH', 'DALLAS')
insert into dept values (30, 'SALES', 'CHICAGO') 
insert into dept values (40, 'OPERATIONS', 'BOSTON')

sp_help dept
select * from dept

sp_help emp
select * from emp

insert into emp values(7369, 'SMITH', 'CLERK', 7902, '1980-12-17', 800, NULL, 20)
insert into emp values(7499, 'ALLEN', 'SALESMAN', 7698, '1981-02-20', 1600, 300, 30)
insert into emp values(7521, 'WARD', 'SALESMAN', 7698, '1981-02-22', 1250, 500, 30)
insert into emp values (7566, 'JONES', 'MANAGER', 7839, '1981-04-02', 2975, null, 20)
insert into emp values (7654, 'MARTIN', 'SALESMAN', 7698, '1981-09-28', 1250, 1400, 30)
insert into emp values (7698, 'BLAKE', 'MANAGER', 7839, '1981-05-01', 2850, null, 30)
insert into emp values (7782, 'CLARK', 'MANAGER', 7839, '1981-06-09', 2450, null, 10)
insert into emp values (7788, 'SCOTT', 'ANALYST', 7566, '1987-04-19', 3000, null, 20)
insert into emp values (7839, 'KING', 'PRESIDENT', null, '1981-11-17', 5000, null, 10)
insert into emp values (7844, 'TURNER', 'SALESMAN', 7698, '1981-09-08', 1500, 0, 30)
insert into emp values (7876, 'ADAMS', 'CLERK', 7788, '1987-05-23', 1100, null, 20)
insert into emp values (7900, 'JAMES', 'CLERK', 7698, '1981-12-03', 950, null, 30)
insert into emp values (7902, 'FORD', 'ANALYST', 7566, '1981-12-03', 3000, null, 20)
insert into emp values (7934, 'MILLER', 'CLERK', 7782, '1982-01-23', 1300, null, 10)

--1.List all employees whose name begins with 'A'. 
select * from emp where ename like 'A%'

--2.Select all those employees who don't have a manager.
select * from emp where mgr_id is null

--3.List employee name, number and salary for those employees who earn in the range 1200 to 1400. 
select empno,ename,sal from emp where sal between 1200 and 1400

--4.Give all the employees in the RESEARCH department a 10% pay rise. 
--Verify that this has been done by listing all their details before and after the rise. 
--select empno,ename,sal,(sal*1.10) 'Annual Salary' from emp where deptno = 20

--5.Find the number of CLERKS employed. Give it a descriptive heading. 
select count(*)  'Number of Clerks' from emp where job = 'CLERK'

--6.Find the average salary for each job type and the number of people employed in each job. 
select job Job, avg(sal)'Average Salary', count(*)'Number of Employees' from emp group by job

--7.List the employees with the lowest and highest salary. 
select empno, ename, sal from emp where sal = (select min(sal) from emp)or sal = (select max(sal) from emp)

--8.List full details of departments that don't have any employees.
select * from dept where deptno not in (select distinct deptno from emp)

--9.Get the names and salaries of all the analysts earning more than 1200 who are based in department 20.
--Sort the answer by ascending order of name. 
select ename, sal from emp where job = 'ANALYST' and sal > 1200 and deptno = 20 order by ename asc;

--10.For each department, list its name and number together with the total salary paid to employees in that department. 
--select dname, deptno, (select sum(sal) from emp where emp.deptno = dept.deptno)'Total Salary' from dept

--11.Find out salary of both MILLER and SMITH.
select ename, sal from emp where ename ='MILLER'or ename = 'SMITH'

--12.Find out the names of the employees whose name begin with ‘A’ or ‘M’
select ename from emp where ename like 'A%' or ename like 'M%'

--13.Compute yearly salary of SMITH. 
select ename, (sal * 12)'Yearly Salary' from emp where ename = 'SMITH'

--14.List the name and salary for all employees whose salary is not in the range of 1500 and 2850
select ename, sal from emp where sal not between 1500 and 2850

--15.Find all managers who have more than 2 employees reporting to them
--select mgr_id as 'Manager ID', (select ename from emp where empno = e.mgr_id) as manager_name, count(*) 'Number of Employees'
--from emp e where mgr_id is not null group by mgr_id having count(*) > 2