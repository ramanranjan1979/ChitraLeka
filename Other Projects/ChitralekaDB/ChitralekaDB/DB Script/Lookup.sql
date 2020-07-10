select * from CL.AcademyCenter

insert into CL.AcademyCenter values ('KEHS','King Edward VI Handsworth School, Birmingham B20 2BQ',1)
insert into CL.AcademyCenter values ('SWIN','Solihull Women''s Institute, 745 Warwick Road, Solihull, B91 3DG',1)
insert into CL.AcademyCenter values ('NORF','Allens Cross Community Center, Tinkers Farm Road, Birmingham B31 1RH',1)

select * from CL.AddressType
insert into CL.AddressType values('HOME','Home Address',1,GETDATE(),null)
insert into CL.AddressType values('WORK','WORK Address',1,GETDATE(),null)
insert into CL.AddressType values('BUSINESS','BUSINESS Address',1,GETDATE(),null)

select * from CL.ContactNumberType

insert into CL.ContactNumberType values('HOME','Home Telephone',1,GETDATE(),null)
insert into CL.ContactNumberType values('WORK','Work Telephone',1,GETDATE(),null)
insert into CL.ContactNumberType values('MOBILE','Mobile',1,GETDATE(),null)

select * from CL.Country
--insert into CL.country select Countrycode,englishname from [3weekdiet_Test].TWJ.Country

select * from CL.Grade
insert into CL.Grade values('Beginners',1,GETDATE(),null)
insert into CL.Grade values('Primary',1,GETDATE(),null)
insert into CL.Grade values('G-1',1,GETDATE(),null)
insert into CL.Grade values('G-2',1,GETDATE(),null)
insert into CL.Grade values('G-3',1,GETDATE(),null)
insert into CL.Grade values('G-4',1,GETDATE(),null)
insert into CL.Grade values('G-5',1,GETDATE(),null)
insert into CL.Grade values('G-6',1,GETDATE(),null)
insert into CL.Grade values('Intermediate Foundation',1,GETDATE(),null)
insert into CL.Grade values('Intermediate',1,GETDATE(),null)
insert into CL.Grade values('Advance 1 & Advance 2',1,GETDATE(),null)

select * from CL.LogType
insert into CL.LogType values('Application',1,GETDATE(),null)
insert into CL.LogType values('Exception',1,GETDATE(),null)

select * from CL.Salutation
insert into CL.Salutation values('Mr',1)
insert into CL.Salutation values('Mrs',1)
insert into CL.Salutation values('Miss',1)
insert into CL.Salutation values('Ms',1)
insert into CL.Salutation values('Dr',1)


select * from CL.PersonType
insert into CL.PersonType values('Student',1,GETDATE())
insert into CL.PersonType values('External Student',1,GETDATE())
insert into CL.PersonType values('Faculty',1,GETDATE())
insert into CL.PersonType values('Support Staff',1,GETDATE())
insert into CL.PersonType values('Others',1,GETDATE())
insert into CL.PersonType values('Father',1,GETDATE())
insert into CL.PersonType values('Mother',1,GETDATE())

select * from CL.EnquiryType
insert into CL.EnquiryType values('Batch Enquiry',1,GETDATE())


--select * from CL.role

insert into  CL.Role values('Admin','Admin Role',GETDATE())
insert into  CL.Role values('Student','Student Role',GETDATE())

select * from CL.InvoiceType

insert into CL.InvoiceType values ('GRDTRMFEE','Grade Term Fee',getdate(),1)
insert into CL.InvoiceType values ('EVTPARFEE','Event Participation Fee',getdate(),1)
insert into CL.InvoiceType values ('MISCSHOFEE','Miscellaneous Fee',getdate(),1)
insert into CL.InvoiceType values ('EVTDVDFEE','Event DVD Fee',getdate(),1)
insert into CL.InvoiceType values ('EVTMKPFEE','Event Makeup and costume Fee',getdate(),1)
insert into CL.InvoiceType values ('GRDBOKFEE','Any Study Material or book Fee',getdate(),1)
insert into CL.InvoiceType values ('FINE','Any Fine',getdate(),1)


select * from CL.PersonType
select * from CL.Person
insert into CL.Person values(1,'Raman','Prakash','Ranjan','Male','12/10/1980',1,GETDATE(),null,1,null,null,5)
select * from CL.Role
select * from CL.PersonRole
insert into CL.PersonRole values(1,1,GETDATE(),1)

select * from CL.Login
insert into CL.Login values('ramanranjan1979','F04949AF70B9EC13069FD8E32490445C8F17C2ED',GETDATE(),null,null,1,0,0,1)

insert into CL.EmailAddress values('ramanranjan1979@gmail.com',GETDATE(),null,1)
insert into CL.PersonEmail values(1,1,1,GETDATE(),1)


insert into CL.MailoutType values ('tmpAdmission','On student admission',1,GETDATE(),'Welcome Aboard')
insert into CL.MailoutType values ('tmpCredential','On online account setup',1,GETDATE(),'Your Login Details')

insert into CL.MailoutType values ('tmpForgotPassword','On password reset request',1,GETDATE(),'Forgotten Your Password?')
insert into CL.MailoutType values ('tmpPasswordAck','After a new password has been generated',1,GETDATE(),'Your New Password')

insert into CL.MailoutType values ('tmpInvoice','As soon as a new invoice is raised',1,GETDATE(),'Your New Invoice')

insert into CL.MailoutType values ('tmpPayment','After a payment has been initiated',1,GETDATE(),'Payment Initiated')
insert into CL.MailoutType values ('tmpPaymentAck','This is payment acknowledgement',1,GETDATE(),'Payment Confirmation')


insert into CL.MailoutType values ('tmpGrade','On grade change',1,GETDATE(),'Grade Change')
insert into CL.MailoutType values ('tmpTerm','On term change',1,GETDATE(),'New Term Enrollment')


insert into CL.MailoutType values ('tmpDeactivation','On account deactivation request',1,GETDATE(),'Are you sure?')
insert into CL.MailoutType values ('tmpDeactivationAck','This is account deactivation acknowledgement',1,GETDATE(),'Need Your Response')


insert into CL.MailoutType values ('tmpSupport','When a new support query has been received',1,GETDATE(),'Support Query')
insert into CL.MailoutType values ('tmpDefault','This is default',1,GETDATE(),'Default')


select * from CL.MailoutType
select * from CL.MailoutParameter
insert into CL.MailoutParameter values('Name','Name')
insert into CL.MailoutParameter values('Message','Message Content')
insert into CL.MailoutParameter values('ResetLink','Reset Link')
insert into CL.MailoutParameter values('SecurityCode','Security Code')


select * from CL.MailoutTypeParameter
insert into CL.MailoutTypeParameter values(1,13,1)
insert into CL.MailoutTypeParameter values(1,13,2)

insert into CL.MailoutTypeParameter values(1,3,1)
insert into CL.MailoutTypeParameter values(1,3,3)
insert into CL.MailoutTypeParameter values(1,3,4)


select * from CL.EventType
insert into CL.EventType values ('Term Classes','Term Classes',GETDATE(),null,1)
insert into CL.EventType values ('Non Term Classes','Non Term Classes',GETDATE(),null,1)
insert into CL.EventType values ('Others','Others',GETDATE(),null,1)

insert into CL.SecurityType values('FORGOTPASSWORD')