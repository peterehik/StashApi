create table Users (
	id int identity(1,1) primary key, 
	email varchar(200) not null, 
	phone_number varchar(20) not null, 
	full_name varchar(200) not null,
	password varchar(100) not null,
	key varchar(100) not null,
	account_key varchar(100) null,
	metadata varchar(2000) not null
);