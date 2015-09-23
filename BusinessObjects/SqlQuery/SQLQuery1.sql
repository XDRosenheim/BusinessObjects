use master
go

Create database KudvenkatDB11
go

use KudvenkatDB11
go

Create table tblEmployee
(
 Id int Primary Key identity,
 Name varchar(30),
 Gender varchar(10),
 City varchar(25),
 DateOfBirth date
)
go

Insert into tblEmployee values
 ('Mark', 'Male', 'Brønshøj', '1979-01-05'),
 ('Jesper', 'Male', 'Helsingør', '1981-11-06'),
 ('Kirstine', 'Female', 'Herlev', '1978-08-09'),
 ('Nikolaj', 'Male', 'Lyngby', '1974-04-15'),
 ('Mathias', 'Male', 'Helsingør', '1975-02-19'),
 ('Patrick', 'Male', 'Måløv', '1993-02-19'),
 ('Christian', 'Male', 'Søbørg', '1992-02-19'),
 ('Jan', 'Male', 'Hellerup', '1954-02-19')
go

Create procedure spGetAllEmployees
as
Begin
	Select Id, Name, Gender, City, DateOfBirth
	from tblEmployee
End
go

Create procedure spAddEmployee
@Name nvarchar(50) = null,
@Gender nvarchar(10) = null,
@City nvarchar(50) = null,
@DateOfBirth DateTime = null
as
	Begin
		Insert into tblEmployee (Name, Gender, City, DateOfBirth)
		Values (@Name, @Gender, @City, @DateOfBirth)
	End
go

CREATE procedure spSaveEmployee
@Id int,
@Name nvarchar(50),
@Gender nvarchar(10),
@City nvarchar(50),
@DateOfBirth DateTime
as
	Begin
		Update tblEmployee set Name = @Name, Gender = @Gender, City = @City, DateOfBirth = @DateOfBirth
		Where Id = @Id
	End
go

CREATE procedure spDeleteEmployee
@Id int
as
	Begin
		Delete from tblEmployee
		Where @Id = Id
	end
go