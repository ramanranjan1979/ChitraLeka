/*CONSTRAINTS*/

select * from CL.EmailAddress
ALTER TABLE CL.EmailAddress ADD UNIQUE (Value)

select * from CL.AcademyCenter

ALTER TABLE CL.AcademyCenter ADD UNIQUE (Code)

select * from CL.Address

ALTER TABLE CL.Address ADD CONSTRAINT UNQKEY_PERSONID_ADDRESSTYPID UNIQUE(AddressTypeId, PersonId)



select * from CL.ContactNumberType
ALTER TABLE CL.ContactNumberType ADD UNIQUE (Name)

select * from CL.Country
ALTER TABLE CL.Country ADD UNIQUE (Code)
ALTER TABLE CL.Country ADD UNIQUE (EnglishName)

select * from CL.Grade
ALTER TABLE CL.Grade ADD UNIQUE (Name)

select * from CL.[Key]
ALTER TABLE CL.[Key] ADD UNIQUE (Name)

select * from CL.MailoutType
ALTER TABLE CL.MailoutType ADD UNIQUE (Name)

select * from CL.Registration
ALTER TABLE CL.Registration ADD UNIQUE (Person_Id)

select * from CL.PersonType

ALTER TABLE CL.PersonType ADD UNIQUE (Name)

select * from CL.EnquiryType

ALTER TABLE CL.PersonType ADD UNIQUE (Name)

ALTER TABLE CL.calendar ADD UNIQUE (Name)

ALTER TABLE CL.Calendar ADD CONSTRAINT UNQKEY_EVENT UNIQUE(EventTypeId,GradeId,AcademyCenterId,StartDate)
ALTER TABLE CL.EventType ADD CONSTRAINT UNQKEY_EVENTTYPE UNIQUE(Name)