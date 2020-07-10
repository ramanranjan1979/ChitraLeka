using System;
using System.Collections.Generic;
using System.Linq;
using ChitralekaDB;
using System.Data.Entity;
using ChitraLeka.ViewModel.Account;
using ChitraLeka.ViewModel.Contact;
using ChitraLeka.ViewModel.Registration;
using System.Security.Cryptography;
using ChitraLeka.ViewModel.Ledger;
using ChitraLeka.ViewModel.Shared;
using ChitraLeka.ViewModel.Student;
using System.Collections;
using ChitraLeka.ViewModel.Security;

namespace ChitraLekaDAL
{


    public class Dal : cLekaDbEntityContainer
    {

    }

    public class StudentDal : cLekaDbEntityContainer
    {
        public List<ChitraLeka.ViewModel.Student.Student> GetAllStudents(int gradeId)
        {
            List<ChitraLeka.ViewModel.Student.Student> data = new List<ChitraLeka.ViewModel.Student.Student>();
            var students = (from x in Student where x.StudentGrade.Where(g => g.IsCurrent).FirstOrDefault().GradeId == gradeId select x).ToList();
            foreach (var student in students)
            {
                ChitraLeka.ViewModel.Student.Student s = new ChitraLeka.ViewModel.Student.Student()
                {
                    Candidate = new ContactDal().searchPersonById(student.Registration.Person.Id)
                };

                data.Add(s);
            }

            return data;
        }
    }

    public class SecurityDal : cLekaDbEntityContainer
    {

        public LoginStatus ValidateUser(string userName, string password)
        {

            LoginStatus status = new LoginStatus();

            SHA1Managed sha1m = new SHA1Managed();
            var temp = sha1m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            string passwordHash = "";
            foreach (var _byte in temp)
            {
                passwordHash = passwordHash + _byte.ToString("X2");
            }

            var userLogin = (from x in Login.Include("Person") where x.UserName == userName && x.Password == passwordHash && x.IsActive select x).SingleOrDefault();


            if (userLogin == null)
            {
                status.Success = false;
                status.Message = "Invalid Username or password";
            }
            else
            {
                if (userLogin.Person.PersonRole.Where(r => r.Id == 1).Count() > 0)
                {
                    status.TargetURL = "../Admin/Index";
                }
                else if (userLogin.Person.PersonRole.Where(r => r.Id == 2).Count() > 0)
                {
                    status.TargetURL = "../Member/Index";
                }

                status.Success = true;
                status.Message = "Login attempt successful!";
                status.LoggedInPerson = new Security()
                {
                    LoginId = userLogin.Id,
                    IsActive = userLogin.IsActive,
                    Password = userLogin.Password,
                    UserName = userLogin.UserName,
                    Person = new ContactDal().searchPersonById(userLogin.Person.Id)
                };

                status.LoggedInPerson.RoleNameList = new List<string>();

                foreach (var role in userLogin.Person.PersonRole)
                {
                    status.LoggedInPerson.RoleNameList.Add(role.Role.Name.ToLower());
                }
            }

            return status;
        }


        public List<SecurityTypeCode> GetPeronSecurityCodeBySecurityType(int personId, string securityTypeName)
        {
            List<SecurityTypeCode> data = new List<SecurityTypeCode>();

            var x = (from y in SecurityCode where y.PersonId == personId && y.SecurityType.Name.ToLower() == securityTypeName.ToLower() select y).ToList();
            foreach (var item in x)
            {
                SecurityTypeCode stc = new SecurityTypeCode()
                {
                    Code = item.Code,
                    CreatedOn = item.CreatedOn,
                    ExpiredOn = item.ExpiredOn,
                    Id = item.Id,
                    Person = new ContactDal().searchPersonById(item.PersonId),
                    SecurityType = new ChitraLeka.ViewModel.Security.SecurityType()
                    {
                        Id = item.SecurityType.Id,
                        Name = item.SecurityType.Name
                    }
                };
                data.Add(stc);
            }

            return data;

        }

        public ChitraLeka.ViewModel.Contact.Person GetPersonByUserName(string userName)
        {
            ChitraLeka.ViewModel.Contact.Person p = new ChitraLeka.ViewModel.Contact.Person();
            var l = (from x in Login where x.UserName.ToLower() == userName.ToLower() select x).FirstOrDefault();
            p = l == null ? p : new ContactDal().searchPersonById(l.Person.Id);
            return p;
        }

        public void UpdateUserLogin(int loginId, string fieldName, string fieldValue = null)
        {
            var login = (from l in Login where l.Id == loginId select l).FirstOrDefault();

            if (fieldName == "LOCK")
            {
                DateTime? dt = null;
                login.IsLock = login.IsLock ? false : true;
                login.LockedOn = login.IsLock ? DateTime.Now : dt;
            }
            if (fieldName == "Password")
            {
                login.Password = fieldValue;
            }
            login.ModifiedOn = DateTime.Now;

            Login.Attach(login);
            Entry(login).State = EntityState.Modified;
            SaveChanges();


        }

        public string GenerateUserName(int personId)
        {
            string username = string.Empty;
            var p = new ContactDal().searchPersonById(personId);
            try
            {
                string tryUsername = p.FirstName.Trim() + p.LastName.Trim();
                //if (tryUsername.Length >= 10)
                //{
                //    tryUsername = tryUsername.Substring(0, 10);
                //}

                //if (tryUsername.Length < 5)
                //{
                //    tryUsername = string.Format("{0}{1}", tryUsername, "cdc");
                //}

                username = GetUniqueUserName(tryUsername);

            }
            catch (Exception ex)
            {
                username = Guid.NewGuid().ToString().Substring(0, 8);
            }

            return username.ToLower();
        }

        public ChitraLeka.ViewModel.Security.SecurityTypeCode GetPersonSecurityCodeByCode(string code)
        {
            var data = (from x in SecurityCode where x.Code.ToLower() == code.ToLower() select x).FirstOrDefault();
            ChitraLeka.ViewModel.Security.SecurityTypeCode stc = new SecurityTypeCode()
            {
                Code = data.Code,
                CreatedOn = data.CreatedOn,
                ExpiredOn = data.ExpiredOn,
                Id = data.Id,
                Person = new ContactDal().searchPersonById(data.PersonId),
                SecurityType = new ChitraLeka.ViewModel.Security.SecurityType()
                {
                    Id = data.SecurityType.Id,
                    Name = data.SecurityType.Name
                }
            };

            return stc;

        }

        public void ApplyPersonSecurityCode(int personId, string code)
        {
            var data = (from x in SecurityCode where x.Code.ToLower() == code.ToLower() && x.Person.Id == personId select x).FirstOrDefault();

            if (data == null)
                throw new Exception($"PersonId {personId} and security code {code} is not found or matching");

            data.ExpiredOn = DateTime.Now;
            SecurityCode.Attach(data);
            Entry(data).State = EntityState.Modified;
            SaveChanges();
        }

        public string GetUniqueUserName(string code)
        {
            string userName = code;
            bool isDuplicate = true;

            while (isDuplicate == true)
            {
                var m = Login.Where(x => x.UserName.ToUpper() == code.ToUpper()).ToList();
                if (m.ToList().Count > 0)
                {
                    userName = string.Format("{0}{1}", userName, m.Count() + 1);
                    code = userName;
                }
                else
                {
                    isDuplicate = false;
                }
            }

            return userName;
        }

        public void SaveUserLogin(NewPeronLogin nPL)
        {
            var p = (from x in Person where x.Id == nPL.PersonId select x).FirstOrDefault();

            var r = (from x in Role where x.Id == nPL.roleId select x).FirstOrDefault();

            ChitralekaDB.Login l = new ChitralekaDB.Login()
            {
                UserName = nPL.UserName,
                Password = nPL.Password,
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsLock = false,
                MustChangepassword = false,
                Person = p
            };

            Login.Add(l);
            SaveChanges();

            ChitralekaDB.PersonRole myrole = new ChitralekaDB.PersonRole()
            {
                Person = p,
                Role = r,
                PersonId = nPL.PersonId,
                CreatedOn = DateTime.Now,
                IsActive = true
            };

            PersonRole.Add(myrole);
            SaveChanges();


        }

        public List<ChitraLeka.ViewModel.Shared.Role> GetAllRoles()
        {
            List<ChitraLeka.ViewModel.Shared.Role> data = new List<ChitraLeka.ViewModel.Shared.Role>();
            var roles = (from x in Role select x).ToList();
            foreach (var role in roles)
            {
                ChitraLeka.ViewModel.Shared.Role r = new ChitraLeka.ViewModel.Shared.Role()
                {
                    Id = role.Id,
                    Name = role.Name
                };

                data.Add(r);
            }

            return data;
        }

        public List<ChitraLeka.ViewModel.Contact.Person> GetPendingLogin()
        {
            List<ChitraLeka.ViewModel.Contact.Person> data = new List<ChitraLeka.ViewModel.Contact.Person>();
            var people = (from x in Person where x.Login == null && x.PersonTypeId == 1 && x.Registration != null select x).ToList();
            foreach (var person in people.Where(x => x.Registration.Student.Count() > 0))
            {
                ChitraLeka.ViewModel.Contact.Person x = new ChitraLeka.ViewModel.Contact.Person()
                {
                    Id = person.Id,
                    FirstName = person.FName,
                    LastName = person.LName,
                    PrimaryEmail = person.PersonEmail.Where(y => y.IsPrimary).FirstOrDefault().EmailAddress.Value
                };

                data.Add(x);
            }
            return data;
        }

        public void LogMe(string logTypeName, string msg, int? personId)
        {
            Log log = new Log()
            {
                Description = msg,
                PersonId = personId.HasValue ? personId.Value : (int?)null,
                CreatedOn = DateTime.Now
            };
            var _type = (from _logType in LogType where _logType.Name.ToLower() == logTypeName.ToLower() select _logType).SingleOrDefault();
            log.LogType = _type;
            Log.Add(log);
            SaveChanges();
        }

        public List<Security> GetAllRolesLogin()
        {
            List<Security> login = new List<Security>();
            var logins = (from x in Login select x);
            foreach (var l in logins)
            {
                Security s = new Security()
                {
                    LoginId = l.Id,
                    UserName = l.UserName,
                    Password = l.Password,
                    IsActive = l.IsActive,
                    IsLock = l.IsLock,
                    CreatedOn = l.CreatedOn,
                    ModifiedOn = l.ModifiedOn.HasValue ? l.ModifiedOn.Value : (DateTime?)null,
                    LockedOn = l.LockedOn.HasValue ? l.LockedOn.Value : (DateTime?)null,
                    Person = new ContactDal().searchPersonById(l.Person.Id)
                };
                s.RoleNameList = new List<string>();
                foreach (var r in l.Person.PersonRole)
                {
                    s.RoleNameList.Add(r.Role.Name);
                }

                login.Add(s);

            }

            return login;

        }

        public List<SystemLog> GetSystemLog()
        {
            List<SystemLog> syslogList = new List<SystemLog>();
            var syslogs = (from x in Log select x);
            foreach (var l in syslogs)
            {
                SystemLog sLog = new SystemLog()
                {
                    Id = l.Id,
                    CreatedOn = l.CreatedOn,
                    Description = l.Description,
                    Type = new ChitraLeka.ViewModel.Security.LogType()
                    {
                        Id = l.LogType.Id,
                        Name = l.LogType.Name
                    }
                };

                if (l.PersonId.HasValue)
                {
                    sLog.Person = new ContactDal().searchPersonById(l.Person.Id);
                }

                syslogList.Add(sLog);
            }
            return syslogList;
        }

        public string RaisePersonSecurityRequest(int personId, string securityName)
        {
            var type = (from x in SecurityType where x.Name.ToLower() == securityName.ToLower() select x).FirstOrDefault();
            var p = (from x in Person where x.Id == personId select x).FirstOrDefault();

            ChitralekaDB.SecurityCode sc = new ChitralekaDB.SecurityCode()
            {
                Code = RandomNumberGenerator.Create().GetHashCode().ToString(),
                CreatedOn = DateTime.Now,
                SecurityType = type,
                Person = p,
                SecurityTypeId = type.Id,
                PersonId = personId
            };
            SecurityCode.Add(sc);
            SaveChanges();
            return sc.Code;
        }
    }

    public class ContactDal : cLekaDbEntityContainer
    {
        public void DeletePersonAddress(ChitraLeka.ViewModel.Contact.Address address)
        {
            //Address.Remove(address);
            SaveChanges();
        }

        public void DeletePersonById(int id)
        {

        }

        public void DeletePersonContactNumber(ChitraLeka.ViewModel.Contact.Contact contact)
        {
            throw new NotImplementedException();
        }

        public List<ChitraLeka.ViewModel.Contact.Person> GetAllPerson()
        {
            List<ChitraLeka.ViewModel.Contact.Person> v = new List<ChitraLeka.ViewModel.Contact.Person>();


            var people = (from x in Person select x).ToList();
            foreach (ChitralekaDB.Person person in people)
            {
                var p = new ChitraLeka.ViewModel.Contact.Person()
                {
                    Id = person.Id,
                    FirstName = person.FName,
                    MiddleName = person.MName,
                    LastName = person.LName,
                    DOB = person.DOB.Value,
                    GenderTypeId = person.Gender,
                    DateCreated = person.CreatedOn,
                    PersonTypeId = person.PersonTypeId.ToString(),
                    PrimaryEmail = person.PersonEmail.Where(e => e.IsPrimary).FirstOrDefault().EmailAddress.Value
                };


                p.PersonEmail = new List<ChitraLeka.ViewModel.Contact.EmailAddress>();
                p.PersonAddress = new List<ChitraLeka.ViewModel.Contact.Address>();
                p.PersonContactNumber = new List<ChitraLeka.ViewModel.Contact.Contact>();

                foreach (var PE in person.PersonEmail)
                {
                    var pe = new ChitraLeka.ViewModel.Contact.EmailAddress()
                    {
                        Value = PE.EmailAddress.Value,
                        DateCreated = PE.CreatedOn,
                        Id = PE.Id,
                        IsActive = PE.IsActive,
                        IsPrimary = PE.IsPrimary,
                        PersonId = PE.PersonId
                    };

                    p.PersonEmail.Add(pe);
                }

                foreach (var PA in person.Address)
                {
                    var pa = new ChitraLeka.ViewModel.Contact.Address()
                    {
                        Id = PA.Id,
                        AddressTypeId = PA.AddressTypeId,
                        //AddressType=PA.AddressType,
                        Line1 = PA.Line1,
                        Line2 = PA.Line2,
                        City = PA.City,
                        State = PA.State,
                        CreatedOn = PA.CreatedOn,
                        IsActive = PA.IsActive,
                        PostCode = PA.PostCode,
                        Landmark = PA.Landmark,
                        PersonID = PA.PersonId,
                        ModifiedOn = PA.ModifiedOn
                    };

                    p.PersonAddress.Add(pa);
                }

                foreach (var PC in person.Contact)
                {
                    var pc = new ChitraLeka.ViewModel.Contact.Contact()
                    {
                        Id = PC.Id,
                        Value = PC.Value,
                        IsActive = PC.IsActive,
                        PersonID = PC.PersonId,
                        CreatedOn = PC.CreatedOn,
                        //ContactNumberType=PC.ContactNumberType,
                        ContactNumberTypeId = PC.ContactNumberTypeId,
                        ModifiedOn = PC.ModifiedOn

                    };

                    p.PersonContactNumber.Add(pc);
                }


                v.Add(p);


            }

            return v.OrderByDescending(o => o.DateCreated).ToList();
        }

        public List<ChitraLeka.ViewModel.Contact.Person> GetMotherChildren(int motherId)
        {
            List<ChitraLeka.ViewModel.Contact.Person> people = new List<ChitraLeka.ViewModel.Contact.Person>();
            var children = (from mx in Person where (mx.MotherId == motherId) select mx);
            foreach (var child in children)
            {
                people.Add(searchPersonById(child.Id));
            }

            return people;
        }

        public List<ChitraLeka.ViewModel.Contact.Person> GetFatherChildren(int fatherId)
        {
            List<ChitraLeka.ViewModel.Contact.Person> people = new List<ChitraLeka.ViewModel.Contact.Person>();
            var children = (from mx in Person where (mx.FatherId == fatherId) select mx);
            foreach (var child in children)
            {
                people.Add(searchPersonById(child.Id));
            }

            return people;
        }

        public void SavePerson(ChitraLeka.ViewModel.Contact.Person person)
        {
            var newEmail = new ChitralekaDB.EmailAddress()
            {
                Value = person.PrimaryEmail,
                IsActive = true,
                CreatedOn = DateTime.Now

            };


            var newperson = new ChitralekaDB.Person()
            {
                FName = person.FirstName,
                MName = person.MiddleName,
                LName = person.LastName,
                SalutationId = int.Parse(person.SalutationTypeId),
                DOB = person.DOB,
                Gender = int.Parse(person.GenderTypeId) == 1 ? "Female" : "Male",
                IsActive = true,
                CreatedOn = DateTime.Now,
                GroupId = 0,
                PersonTypeId = int.Parse(person.PersonTypeId)
            };

            var newPersonEmail = new ChitralekaDB.PersonEmail()
            {
                EmailAddress = newEmail,
                Person = newperson,
                IsActive = true,
                IsPrimary = true,
                CreatedOn = DateTime.Now,
                EmailAddressId = newEmail.Id,
                PersonId = newperson.Id
            };

            PersonEmail.Add(newPersonEmail);

            SaveChanges();

        }

        public void SavePersonAddress(ChitraLeka.ViewModel.Contact.Address address)
        {
            var newaddress = new ChitralekaDB.Address()
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                CreatedOn = DateTime.Now,
                IsActive = true,
                Landmark = address.Landmark,
                PostCode = address.PostCode,
                State = address.State,
                AddressTypeId = address.AddressTypeId,
                PersonId = address.PersonID
            };

            Address.Add(newaddress);
            SaveChanges();
        }

        public void SavePersonContactNumber(ChitraLeka.ViewModel.Contact.Contact contactnumber)
        {
            var newcontactnumber = new ChitralekaDB.Contact()
            {
                ContactNumberTypeId = contactnumber.ContactNumberTypeId,
                CreatedOn = DateTime.Now,
                IsActive = true,
                Value = contactnumber.Value,
                PersonId = contactnumber.PersonID
            };

            Contact.Add(newcontactnumber);
            SaveChanges();
        }

        public void SavePersonEmailAddress(ChitraLeka.ViewModel.Contact.EmailAddress emailaddress)
        {
            var email = (from e in EmailAddress where e.Value.ToLower() == emailaddress.Value select e).FirstOrDefault();
            var person = (from p in Person where p.Id == emailaddress.PersonId select p).FirstOrDefault();
            var personEmail = (from p in PersonEmail where (p.PersonId == emailaddress.PersonId && p.EmailAddress.Value.ToLower() == emailaddress.Value.ToLower()) select p).FirstOrDefault();

            if (email == null)
            {
                //Create a new email 
                var mx = new ChitralekaDB.EmailAddress()
                {
                    Value = emailaddress.Value,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };


                var newPersonEmail = new ChitralekaDB.PersonEmail()
                {
                    EmailAddress = new ChitralekaDB.EmailAddress() { Value = emailaddress.Value, IsActive = true, CreatedOn = DateTime.Now },
                    Person = person,
                    IsActive = true,
                    IsPrimary = false,
                    CreatedOn = DateTime.Now,
                    PersonId = person.Id
                };

                PersonEmail.Add(newPersonEmail);

                SaveChanges();

            }
            else
            {
                if (personEmail == null)
                {
                    var newPersonEmail = new ChitralekaDB.PersonEmail()
                    {
                        EmailAddress = email,
                        Person = person,
                        IsActive = true,
                        IsPrimary = false,
                        CreatedOn = DateTime.Now,
                        PersonId = person.Id
                    };

                    PersonEmail.Add(newPersonEmail);

                    SaveChanges();
                }
            }


        }

        public ChitraLeka.ViewModel.Contact.EmailAddress SearchEmailAddressByEmailAddress(string mx)
        {
            ChitraLeka.ViewModel.Contact.EmailAddress email = new ChitraLeka.ViewModel.Contact.EmailAddress();

            var data = (from e in EmailAddress where e.Value.ToLower() == mx.ToLower() select e).SingleOrDefault();
            email.Id = data.Id;
            email.IsActive = data.IsActive;
            email.Value = data.Value;
            email.DateCreated = data.CreatedOn;
            email.ModifiedOn = data.ModifiedOn;

            return email;
        }


        public ChitraLeka.ViewModel.Contact.Address SearchPersonAddress(int id)
        {
            return null;
        }

        public ChitraLeka.ViewModel.Contact.Address SearchPersonAddressesByAddressId(int id)
        {
            ChitraLeka.ViewModel.Contact.Address a = new ChitraLeka.ViewModel.Contact.Address();
            var address = (from x in Address where x.Id == id select x).FirstOrDefault();

            a.Line1 = address.Line1;
            a.Line2 = address.Line2;
            a.City = address.City;
            a.State = address.State;
            a.PostCode = address.PostCode;
            a.Landmark = address.Landmark;

            return a;
        }

        public List<ChitraLeka.ViewModel.Contact.Address> SearchPersonAddressesByPersonId(int? id)
        {
            List<ChitraLeka.ViewModel.Contact.Address> personaddresses = new List<ChitraLeka.ViewModel.Contact.Address>();

            var addresses = (from x in Address where x.PersonId == id.Value select x).ToList();

            foreach (var address in addresses)
            {
                personaddresses.Add(new ChitraLeka.ViewModel.Contact.Address()
                {
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    State = address.State,
                    PostCode = address.PostCode,
                    IsActive = address.IsActive,
                    Landmark = address.Landmark,
                    AddressTypeId = address.AddressType.Id,
                    ModifiedOn = address.ModifiedOn,
                    PersonID = address.PersonId,
                    CreatedOn = address.CreatedOn,
                    AddressType = address.AddressType.Description,
                    Id = address.Id
                });
            }



            return personaddresses;

        }

        public ChitraLeka.ViewModel.Contact.Person searchPersonById(int? id)
        {
            ChitraLeka.ViewModel.Contact.Person person = null;
            var p = this.Person.Find(id);

            if (p != null)
            {
                person = new ChitraLeka.ViewModel.Contact.Person()
                {
                    Id = p.Id,
                    FirstName = p.FName,
                    MiddleName = p.MName,
                    LastName = p.LName,
                    DOB = p.DOB.Value,
                    DOBStr = p.DOB.Value.ToString(),
                    GenderTypeId = p.Gender == "Female" ? "1" : "2",
                    DateCreated = p.CreatedOn
                };

                person.PersonEmail = SearchPersonEmailAddressesByPersonId(id.Value);
                if (person.PersonEmail.Count > 0)
                {
                    person.PrimaryEmail = person.PersonEmail.Where(mx => mx.IsPrimary).FirstOrDefault().Value;
                }

                person.PersonAddress = SearchPersonAddressesByPersonId(id.Value);

                person.PersonContactNumber = SearchPersonContactNumbersByPersonId(id.Value);

            }

            return person;
        }

        public ChitraLeka.ViewModel.Contact.Contact SearchPersonContactNumbersByContactId(int id)
        {
            ChitraLeka.ViewModel.Contact.Contact personContact = new ChitraLeka.ViewModel.Contact.Contact();
            var d = (from pc in Contact where pc.Id == id select pc).SingleOrDefault();

            personContact.Id = d.Id;
            personContact.IsActive = d.IsActive;
            personContact.Value = d.Value;
            personContact.ContactNumberType = d.ContactNumberType.Description;
            personContact.ContactNumberTypeId = d.ContactNumberTypeId;
            personContact.CreatedOn = d.CreatedOn;

            return personContact;
        }

        public ChitraLeka.ViewModel.Contact.EmailAddress SearchEmailAddressByEmailAddressId(int id)
        {
            ChitraLeka.ViewModel.Contact.EmailAddress email = new ChitraLeka.ViewModel.Contact.EmailAddress();

            var data = (from e in EmailAddress where e.Id == id select e).SingleOrDefault();

            email.Id = data.Id;
            email.IsActive = data.IsActive;
            email.Value = data.Value;
            email.DateCreated = data.CreatedOn;
            email.ModifiedOn = data.ModifiedOn;

            return email;
        }

        public List<ChitraLeka.ViewModel.Contact.Contact> SearchPersonContactNumbersByPersonId(int id)
        {
            List<ChitraLeka.ViewModel.Contact.Contact> personContacts = new List<ChitraLeka.ViewModel.Contact.Contact>();

            var d = (from pc in Contact where pc.PersonId == id select pc).ToList();

            foreach (var c in d)
            {
                personContacts.Add(new ChitraLeka.ViewModel.Contact.Contact()
                {
                    Value = c.Value,
                    ContactNumberTypeId = c.ContactNumberTypeId,
                    CreatedOn = c.CreatedOn,
                    ContactNumberType = c.ContactNumberType.Description,
                    Id = c.Id
                });
            }

            return personContacts;
        }

        public List<ChitraLeka.ViewModel.Contact.EmailAddress> SearchPersonEmailAddressesByPersonId(int id)
        {
            List<ChitraLeka.ViewModel.Contact.EmailAddress> EmailAddress = new List<ChitraLeka.ViewModel.Contact.EmailAddress>();

            var d = (from pe in PersonEmail where pe.PersonId == id select pe).ToList();

            foreach (var c in d)
            {
                EmailAddress.Add(new ChitraLeka.ViewModel.Contact.EmailAddress()
                {
                    Value = c.EmailAddress.Value,
                    DateCreated = c.CreatedOn,
                    IsPrimary = c.IsPrimary,
                    Id = c.Id
                });
            }

            return EmailAddress;
        }

        public void UpdatePerson(ChitraLeka.ViewModel.Contact.Person person)
        {
            var updatePerson = (from p in Person where p.Id == person.Id select p).FirstOrDefault();

            updatePerson.FName = person.FirstName;
            updatePerson.MName = person.MiddleName;
            updatePerson.LName = person.LastName;
            updatePerson.DOB = person.DOB;


            Entry(updatePerson).State = EntityState.Modified;
            SaveChanges();
        }

        public void UpdatePersonAddress(ChitraLeka.ViewModel.Contact.Address address)
        {
            var updateAddress = (from a in Address where a.Id == address.Id select a).FirstOrDefault();
            updateAddress.Line1 = address.Line1;
            updateAddress.Line2 = address.Line2;
            updateAddress.City = address.City;
            updateAddress.State = address.State;
            updateAddress.PostCode = address.PostCode;
            updateAddress.Landmark = address.Landmark;


            Entry(updateAddress).State = EntityState.Modified;
            SaveChanges();
        }

        public void UpdatePersonContactNumber(ChitraLeka.ViewModel.Contact.Contact contactNumber)
        {
            var updateContactNumber = (from c in Contact where c.Id == contactNumber.Id select c).FirstOrDefault();
            updateContactNumber.Value = contactNumber.Value;
            Entry(updateContactNumber).State = EntityState.Modified;
            SaveChanges();

        }

        public ChitraLeka.ViewModel.Contact.EmailAddress GetEmailAddress(string emailAddress)
        {
            ChitraLeka.ViewModel.Contact.EmailAddress ex = new ChitraLeka.ViewModel.Contact.EmailAddress();
            var anyEmail = (from e in EmailAddress where e.Value.ToLower().Equals(emailAddress.Trim().ToLower()) select e).FirstOrDefault();
            if (anyEmail != null)
            {
                ex.Value = anyEmail.Value;
                ex.Id = anyEmail.Id;
            }
            return ex;
        }

        public void UpdateEmailAddress(ChitraLeka.ViewModel.Contact.EmailAddress updateMX)
        {
            var updateEmailAddress = (from mx in EmailAddress where mx.Id == updateMX.Id select mx).FirstOrDefault();
            updateEmailAddress.Value = updateMX.Value;
            Entry(updateEmailAddress).State = EntityState.Modified;
            SaveChanges();
        }
    }

    public class LookupDal : cLekaDbEntityContainer
    {
        public void SaveGradeTerm(TermCreation newTerm)
        {
            var grade = (from g in Grade where g.Id == newTerm.GradeId select g).FirstOrDefault();

            ChitralekaDB.Calendar t = new ChitralekaDB.Calendar()
            {
                Name = newTerm.Name,
                StartDate = newTerm.StartDate,
                EndDate = newTerm.EndDate,
                WeeksCount = newTerm.WeeksCount,
                DaysCount = newTerm.DaysCount,
                CreatedOn = DateTime.Now,
                Fee = newTerm.UnitPrice * newTerm.WeeksCount,
                Remarks = newTerm.Remarks,
                GradeId = newTerm.GradeId,
                UnitPrice = newTerm.UnitPrice,
                ModifiedOn = null,
                Grade = grade
            };

            Calendar.Add(t);
            SaveChanges();


        }

        public List<AcademicCenter> GetAllAcademyCenter()
        {
            List<ChitraLeka.ViewModel.Shared.AcademicCenter> lookupData = new List<ChitraLeka.ViewModel.Shared.AcademicCenter>();
            var centers = (from x in AcademyCenter select x).ToList();
            foreach (var type in centers)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Shared.AcademicCenter()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Code = type.Code
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Contact.AddressType> GetAllAddressTypes()
        {
            List<ChitraLeka.ViewModel.Contact.AddressType> lookupData = new List<ChitraLeka.ViewModel.Contact.AddressType>();
            var persontype = (from x in PersonType select x).ToList();
            foreach (var type in AddressType)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Contact.AddressType()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Contact.ContactNumberType> GetAllContactNumberTypes()
        {
            List<ChitraLeka.ViewModel.Contact.ContactNumberType> lookupData = new List<ChitraLeka.ViewModel.Contact.ContactNumberType>();
            var persontype = (from x in PersonType select x).ToList();
            foreach (var type in ContactNumberType)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Contact.ContactNumberType()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Shared.Grade> GetAllGrades()
        {
            List<ChitraLeka.ViewModel.Shared.Grade> lookupData = new List<ChitraLeka.ViewModel.Shared.Grade>();
            var grades = (from x in Grade select x).ToList();
            foreach (var type in grades)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Shared.Grade()
                {
                    Id = type.Id,
                    Name = type.Name,
                    StudentCount = type.StudentGrade.Count()
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Ledger.InvoiceType> GetAllInvoiceTypes()
        {
            List<ChitraLeka.ViewModel.Ledger.InvoiceType> lookupData = new List<ChitraLeka.ViewModel.Ledger.InvoiceType>();
            var invoiceTypes = (from x in InvoiceType select x).ToList();
            foreach (var type in invoiceTypes)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Ledger.InvoiceType()
                {
                    Id = type.Id,
                    Code = type.Code,
                    Description = type.Description
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Contact.PersonType> GetAllPersonTypes()
        {
            List<ChitraLeka.ViewModel.Contact.PersonType> lookupData = new List<ChitraLeka.ViewModel.Contact.PersonType>();
            var persontype = (from x in PersonType select x).ToList();
            foreach (var type in persontype)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Contact.PersonType()
                {
                    Id = type.Id,
                    Name = type.Name,
                    CretaedOn = type.CretaedOn,
                    IsActive = type.IsActive
                });
            }

            return lookupData;
        }

        public List<ChitraLeka.ViewModel.Contact.Salutation> GetAllSalutation()
        {
            List<ChitraLeka.ViewModel.Contact.Salutation> lookupData = new List<ChitraLeka.ViewModel.Contact.Salutation>();
            var types = (from x in Salutation select x).ToList();
            foreach (var type in types)
            {
                lookupData.Add(new ChitraLeka.ViewModel.Contact.Salutation()
                {
                    Id = type.Id,
                    Name = type.Value
                });
            }

            return lookupData;
        }

        public TermListing GetAllTerms(int? gradeId)
        {
            ChitraLeka.ViewModel.Shared.TermListing data = new ChitraLeka.ViewModel.Shared.TermListing();

            data.TermList = new List<ChitraLeka.ViewModel.Shared.Term>();

            var terms = (from t in Calendar select t).ToList();

            foreach (var item in terms.Where(x => gradeId.HasValue ? x.GradeId == gradeId.Value : 1 == 1))
            {
                var term = new ChitraLeka.ViewModel.Shared.Term()
                {
                    Id = item.Id,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    WeeksCount = item.WeeksCount,
                    DaysCount = item.DaysCount,
                    UnitPrice = item.UnitPrice,
                    Fee = item.Fee,
                    CreatedOn = item.CreatedOn,
                    ModifiedOn = item.ModifiedOn,
                    Remarks = item.Remarks,
                    GradeId = item.GradeId,
                    Grade = new ChitraLeka.ViewModel.Shared.Grade()
                    {
                        Id = item.Grade.Id,
                        Name = item.Grade.Name,
                        StudentCount = item.Grade.StudentGrade.Count()
                    }
                };

                data.TermList.Add(term);
            }

            return data;
        }

        public TermListing GetTermByGradeId(int gradeId)
        {
            ChitraLeka.ViewModel.Shared.TermListing t = new TermListing();
            t.TermList = new List<ChitraLeka.ViewModel.Shared.Term>();
            var terms = (from x in Calendar.Where(g => g.GradeId == gradeId) select x).ToList();
            foreach (var item in terms)
            {
                var term = new ChitraLeka.ViewModel.Shared.Term()
                {
                    Id = item.Id,
                    Name = item.Name,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    WeeksCount = item.WeeksCount,
                    DaysCount = item.DaysCount,
                    UnitPrice = item.UnitPrice,
                    Fee = item.Fee,
                    CreatedOn = item.CreatedOn,
                    ModifiedOn = item.ModifiedOn,
                    Remarks = item.Remarks,
                    GradeId = item.GradeId,
                    Grade = new ChitraLeka.ViewModel.Shared.Grade()
                    {
                        Id = item.Grade.Id,
                        Name = item.Grade.Name,
                        StudentCount = item.Grade.StudentGrade.Count()
                    }
                };

                t.TermList.Add(term);
            }
            return t;
        }

        public ChitraLeka.ViewModel.Shared.TermCreation GetTermDetailByTermId(int termId)
        {

            ChitraLeka.ViewModel.Shared.TermCreation t = new ChitraLeka.ViewModel.Shared.TermCreation();
            var term = (from x in Calendar.Where(x => x.Id == termId) select x).SingleOrDefault();

            t.Id = term.Id;
            t.Name = term.Name;
            t.StartDate = term.StartDate;
            t.EndDate = term.EndDate;
            t.WeeksCount = term.WeeksCount;
            t.DaysCount = term.DaysCount;
            t.UnitPrice = term.UnitPrice;
            t.Fee = term.Fee;
            t.GradeId = term.GradeId;
            return t;
        }

        public void UpdateGradeTerm(TermCreation editTerm)
        {
            var grade = (from g in Grade where g.Id == editTerm.GradeId select g).FirstOrDefault();

            ChitralekaDB.Calendar t = (from x in Calendar where x.Id == editTerm.Id select x).FirstOrDefault();

            t.Name = editTerm.Name;
            t.StartDate = editTerm.StartDate;
            t.EndDate = editTerm.EndDate;
            t.WeeksCount = editTerm.WeeksCount;
            t.DaysCount = editTerm.DaysCount;
            t.Fee = editTerm.UnitPrice * editTerm.WeeksCount;
            t.Remarks = editTerm.Remarks;
            t.GradeId = editTerm.GradeId;
            t.UnitPrice = editTerm.UnitPrice;
            t.ModifiedOn = DateTime.Now;
            t.Grade = grade;

            Calendar.Attach(t);
            Entry(t).State = EntityState.Modified;

            SaveChanges();
        }

        public List<MxType> GetAllMailoutTemplates()
        {
            List<MxType> lookupData = new List<MxType>();
            var mxTypes = (from x in MailoutType select x).ToList();
            foreach (var type in mxTypes)
            {
                lookupData.Add(new MxType()
                {
                    Id = type.Id,
                    Name = type.Name,
                    Description = type.Description,
                });
            }

            return lookupData;
        }
    }

    public class RegistrationDal : cLekaDbEntityContainer
    {
        public void SaveRegistration(ChitraLeka.ViewModel.Registration.Registration reg)
        {
            var person = (from p in Person where p.Id == reg.Candidate.Id select p).SingleOrDefault();

            var candidate = new ChitralekaDB.Registration()
            {
                CreatedOn = DateTime.Now,
                IsComplete = false,
                IsActive = true,
                Person = person
            };

            Registration.Add(candidate);

            SaveChanges();
        }

        public ChitraLeka.ViewModel.Registration.RegistrationList GetAllCandidateForRegistration()
        {
            ChitraLeka.ViewModel.Registration.RegistrationList data = new ChitraLeka.ViewModel.Registration.RegistrationList();
            data.Registrations = new List<ChitraLeka.ViewModel.Registration.Registration>();

            var people = (from p in Person where p.PersonTypeId == 1 select p).ToList();
            foreach (var person in people)
            {
                var candidate = new ChitraLeka.ViewModel.Registration.Registration();

                var p = new ChitraLeka.ViewModel.Contact.Person();

                p.Id = person.Id;
                p.FirstName = person.FName;
                p.MiddleName = person.MName;
                p.LastName = person.LName;
                p.GenderTypeId = person.Gender;
                p.DOB = person.DOB.Value;

                candidate.Candidate = p;

                candidate.IsAdult = ChitraLeka.Common.Helper.GetDifferenceInYears(p.DOB, DateTime.Now) >= 16;

                if (person.FatherId.HasValue)
                {
                    candidate.CandidateFather = new ChitraLeka.ViewModel.Contact.Person() { Id = int.Parse(person.FatherId.ToString()) };
                }

                if (person.MotherId.HasValue)
                {
                    candidate.CandidateMother = new ChitraLeka.ViewModel.Contact.Person() { Id = int.Parse(person.MotherId.ToString()) };
                }


                candidate.RegistrationId = person.Registration == null ? 0 : person.Registration.Id;

                data.Registrations.Add(candidate);
            }

            return data;
        }

        public ChitraLeka.ViewModel.Registration.RegistrationList GetAllRegisteredCandidate()
        {
            ChitraLeka.ViewModel.Registration.RegistrationList data = new ChitraLeka.ViewModel.Registration.RegistrationList();
            data.Registrations = new List<ChitraLeka.ViewModel.Registration.Registration>();

            var registrations = (from p in Registration select p).ToList();
            foreach (var r in registrations)
            {
                var candidate = new ChitraLeka.ViewModel.Registration.Registration();
                candidate.RegistrationId = r.Id;
                candidate.Grade = r.GradeId;
                candidate.StudentId = GetStudentDetailByRegistrationId(r.Id).StudentId;
                candidate.Candidate = new ChitraLeka.ViewModel.Contact.Person() { Id = r.Person.Id };
                data.Registrations.Add(candidate);
            }

            return data;
        }

        public ChitraLeka.ViewModel.Registration.Registration GetRegistrationDetailByRegistrationId(int id)
        {

            var registration = (from reg in Registration where reg.Id == id select reg).FirstOrDefault();
            var grade = (from g in Grade where g.Id == registration.GradeId select g).FirstOrDefault();

            ChitraLeka.ViewModel.Registration.Registration data = new ChitraLeka.ViewModel.Registration.Registration()
            {
                Grade = registration.GradeId,
                Remark = registration.Remark,
                Candidate = new ChitraLeka.ViewModel.Contact.Person() { Id = registration.Person.Id },
                CreatedOn = registration.CreatedOn,
                GradeName = grade.Name

            };
            return data;
        }

        public ChitraLeka.ViewModel.Student.Student GetStudentDetailByRegistrationId(int id)
        {

            var student = (from s in Student where s.RegistrationId == id select s).FirstOrDefault();
            ChitraLeka.ViewModel.Student.Student data = new ChitraLeka.ViewModel.Student.Student()
            {
                RegistrationId = id,
                Remark = student == null ? string.Empty : student.Remark,
                DateOfAdmission = student == null ? DateTime.Now : student.DateOfAdmission
            };

            var x = new StudentGradeList();

            x.StudentGrades = new List<ChitraLeka.ViewModel.Student.Student>();

            if (student != null)
            {
                foreach (var sg in StudentGrade.Where(sg => sg.StudentId == student.Id))
                {
                    x.StudentGrades.Add(new ChitraLeka.ViewModel.Student.Student()
                    {
                        DateOfAdmission = sg.Student.DateOfAdmission,
                        StartDate = sg.StartDate,
                        EndDate = sg.EndDate,
                        StudentId = sg.StudentId,
                        IsCurrent = sg.IsCurrent,
                        AcademyCenterId = sg.AcademyCenterId,
                        RegistrationId = sg.Student.RegistrationId,
                        Remark = sg.Remark,
                        Grade = sg.Grade.Name
                    });
                }

            }
            data.StudentGrades = x;

            if (data.StudentGrades.StudentGrades.Count > 0)
            {
                data.StudentId = data.StudentGrades.StudentGrades[0].StudentId;
            }

            return data;
        }



        public void SaveParentDetails(Parent parent)
        {
            var fatherEmail = (from e in EmailAddress where e.Value.ToLower() == parent.Father.PrimaryEmail select e).FirstOrDefault();
            var newEmailFather = new ChitralekaDB.EmailAddress();

            #region SAVE FATHER DETAILS

            if (fatherEmail == null)
            {

                newEmailFather.Value = parent.Father.PrimaryEmail;
                newEmailFather.IsActive = true;
                newEmailFather.CreatedOn = DateTime.Now;
            }
            else
            {
                newEmailFather = fatherEmail;

            }

            var father = new ChitralekaDB.Person()
            {
                FName = parent.Father.FirstName,
                MName = parent.Father.MiddleName,
                LName = parent.Father.LastName,
                SalutationId = 1,
                DOB = parent.Father.DOB,
                Gender = "Male",
                IsActive = true,
                CreatedOn = DateTime.Now,
                GroupId = parent.ChildPersonId,
                PersonTypeId = 6
            };

            var newPersonEmail = new ChitralekaDB.PersonEmail()
            {
                EmailAddress = newEmailFather,
                Person = father,
                IsActive = true,
                IsPrimary = true,
                CreatedOn = DateTime.Now,
                EmailAddressId = newEmailFather.Id,
                PersonId = father.Id
            };

            PersonEmail.Add(newPersonEmail);

            SaveChanges();

            #endregion


            var motherEmail = (from e in EmailAddress where e.Value.ToLower() == parent.Mother.PrimaryEmail select e).FirstOrDefault();
            var newEmailMother = new ChitralekaDB.EmailAddress();

            #region SAVE MOTHER DETAILS
            if (motherEmail == null)
            {

                newEmailMother.Value = parent.Mother.PrimaryEmail;
                newEmailMother.IsActive = true;
                newEmailMother.CreatedOn = DateTime.Now;
            }
            else
            {
                newEmailMother = motherEmail;
            }

            var mother = new ChitralekaDB.Person()
            {
                FName = parent.Mother.FirstName,
                MName = parent.Mother.MiddleName,
                LName = parent.Mother.LastName,
                SalutationId = 2,
                DOB = parent.Mother.DOB,
                Gender = "Female",
                IsActive = true,
                CreatedOn = DateTime.Now,
                GroupId = parent.ChildPersonId,
                PersonTypeId = 7
            };

            var newPersonEmail1 = new ChitralekaDB.PersonEmail()
            {
                EmailAddress = newEmailMother,
                Person = mother,
                IsActive = true,
                IsPrimary = true,
                CreatedOn = DateTime.Now,
                EmailAddressId = newEmailMother.Id,
                PersonId = mother.Id
            };

            PersonEmail.Add(newPersonEmail1);

            SaveChanges();

            #endregion

            #region UPDATE CHILD
            var Child = (from p in Person where p.Id == parent.ChildPersonId select p).FirstOrDefault();

            Child.FatherId = short.Parse(newPersonEmail.Person.Id.ToString());
            Child.MotherId = short.Parse(newPersonEmail1.Person.Id.ToString());
            Child.GroupId = parent.ChildPersonId;

            Person.Attach(Child);
            Entry(Child).State = EntityState.Modified;
            SaveChanges();

            #endregion
        }

        public void CompleteRegistration(ChitraLeka.ViewModel.Registration.Registration reg)
        {
            var candidate = (from p in Person where p.Id == reg.Candidate.Id select p).FirstOrDefault();
            var grade = (from g in Grade where g.Id == reg.Grade select g).FirstOrDefault();

            ChitralekaDB.Registration registration = new ChitralekaDB.Registration()
            {
                CreatedOn = DateTime.Now,
                IsActive = true,
                IsComplete = true,
                Person = candidate,
                Grade = grade,
                Remark = reg.Remark,
                ModifiedOn = DateTime.Now
            };

            Registration.Add(registration);

            SaveChanges();
        }

        public void CompleteAdmission(ChitraLeka.ViewModel.Student.Student newStudent)
        {
            var grade = (from g in Grade where g.Name == newStudent.Grade select g).FirstOrDefault();

            var term = (from t in Calendar where t.Id == newStudent.TermId select t).FirstOrDefault();

            ChitralekaDB.Student s = new ChitralekaDB.Student()
            {
                CreatedOn = DateTime.Now,
                DateOfAdmission = newStudent.DateOfAdmission,
                RegistrationId = newStudent.RegistrationId,
                Remark = newStudent.Remark
            };

            ChitralekaDB.StudentGrade sg = new ChitralekaDB.StudentGrade()
            {
                Student = s,
                CreatedOn = DateTime.Now,
                StartDate = newStudent.StartDate,
                EndDate = newStudent.EndDate,
                IsCurrent = true,
                Remark = newStudent.Remark,
                StudentId = s.Id,
                GradeId = grade.Id,
                AcademyCenterId = newStudent.AcademyCenterId,
                Calendar = term

            };

            StudentGrade.Add(sg);

            SaveChanges();
        }

        public ChitraLeka.ViewModel.Student.Student GetStudentDetailByStudentId(int studentId)
        {
            var student = (from s in Student where s.Id == studentId select s).FirstOrDefault();

            ChitraLeka.ViewModel.Student.Student data = new ChitraLeka.ViewModel.Student.Student()
            {
                RegistrationId = student.RegistrationId,
                StudentId = student.Id,
                Remark = student.Remark,
                DateOfAdmission = student.DateOfAdmission,
                CreatedOn = student.CreatedOn,
                Candidate = new ChitraLeka.ViewModel.Contact.Person()
                {
                    FirstName = student.Registration.Person.FName,
                    LastName = student.Registration.Person.LName,
                    DOB = student.Registration.Person.DOB.Value,
                    GenderTypeId = student.Registration.Person.Gender,
                    PrimaryEmail = student.Registration.Person.PersonEmail.Where(mx => mx.IsPrimary).FirstOrDefault().EmailAddress.Value,
                    Id = student.Registration.Person.Id

                }
            };

            var x = new StudentGradeList();

            x.StudentGrades = new List<ChitraLeka.ViewModel.Student.Student>();

            foreach (var sg in StudentGrade.Where(sg => sg.StudentId == student.Id))
            {
                x.StudentGrades.Add(new ChitraLeka.ViewModel.Student.Student()
                {
                    DateOfAdmission = sg.Student.DateOfAdmission,
                    StartDate = sg.StartDate,
                    EndDate = sg.EndDate,
                    StudentId = sg.StudentId,
                    IsCurrent = sg.IsCurrent,
                    AcademyCenterId = sg.AcademyCenterId,
                    RegistrationId = sg.Student.RegistrationId,
                    Remark = sg.Remark,
                    Grade = sg.Grade.Name,
                    Term = sg.Calendar.Name
                });
            }

            data.StudentGrades = x;

            return data;
        }

        public List<ChitraLeka.ViewModel.Student.Student> GetAllStudents()
        {
            List<ChitraLeka.ViewModel.Student.Student> allStudents = new List<ChitraLeka.ViewModel.Student.Student>();
            var students = (from s in Student select s).ToList();
            foreach (var student in students)
            {
                ChitraLeka.ViewModel.Student.Student s = new ChitraLeka.ViewModel.Student.Student()
                {
                    StudentId = student.Id,
                    Candidate = new ChitraLeka.ViewModel.Contact.Person()
                    {
                        FirstName = student.Registration.Person.FName,
                        LastName = student.Registration.Person.LName
                    }
                };

                allStudents.Add(s);
            }

            return allStudents;
        }


    }

    public class LedgerDal : cLekaDbEntityContainer
    {

        private string GenerateInvoiceNumber()
        {
            var val = GenerateId();

            if (val.Length > 20)
            {
                val = val.Substring(0, 19);
            }

            return val;
        }


        private string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public ChitraLeka.ViewModel.Ledger.Invoice CreateInvoice(ChitraLeka.ViewModel.Student.Student newStudent)
        {
            var grade = (from g in Grade where g.Name == newStudent.Grade select g).FirstOrDefault();
            var term = (from t in Calendar where t.Id == newStudent.TermId select t).FirstOrDefault();
            var invoiceType = (from i in InvoiceType where i.Id == 1 select i).FirstOrDefault();
            var candidate = (from r in Registration where r.Id == newStudent.RegistrationId select r).SingleOrDefault();
            var sgrade = candidate.Grade.StudentGrade.Where(x => x.IsCurrent && x.Student.RegistrationId == newStudent.RegistrationId).SingleOrDefault();

            ChitraLeka.ViewModel.Ledger.Invoice newInvoice = new ChitraLeka.ViewModel.Ledger.Invoice()
            {
                InvoiceAmount = term.Fee,
                InvoiceDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(5),
                IsDue = true,
                InvoiceTypeId = invoiceType.Id,
                Note = string.Format("This is system Generated Invoice - {0} for {1} {2}", invoiceType.Description, candidate.Person.FName, candidate.Person.LName),
                PersonId = candidate.Person.Id,
                InvoiceNumber = GenerateInvoiceNumber().ToUpper()

            };

            ChitralekaDB.Invoice inv = new ChitralekaDB.Invoice()
            {
                DueDate = newInvoice.DueDate,
                InvoiceAmount = newInvoice.InvoiceAmount,
                InvoiceDate = newInvoice.InvoiceDate,
                InvoiceNumber = newInvoice.InvoiceNumber,
                InvoiceTypeId = newInvoice.InvoiceTypeId,
                IsDue = newInvoice.IsDue,
                Note = newInvoice.Note,
                PersonId = newInvoice.PersonId,
                StudentGrade = sgrade,
                InvoiceType = invoiceType
            };

            Invoice.Add(inv);

            SaveChanges();

            return newInvoice;
        }

        public InvoiceListing GetAllInvoices()
        {
            ChitraLeka.ViewModel.Ledger.InvoiceListing data = new ChitraLeka.ViewModel.Ledger.InvoiceListing();

            data.InvoiceList = new List<ChitraLeka.ViewModel.Ledger.Invoice>();

            var invoices = (from inv in Invoice select inv).ToList();

            foreach (var item in invoices)
            {
                var student = (from s in Student where s.Id == item.StudentGrade.Student.Id select s).SingleOrDefault();

                var invoice = new ChitraLeka.ViewModel.Ledger.Invoice()
                {
                    DueDate = item.DueDate,
                    InvoiceDate = item.InvoiceDate,
                    InvoiceAmount = item.InvoiceAmount,
                    InvoiceNumber = string.Format("CDC-{0}", item.InvoiceNumber.ToUpper()),
                    Id = item.Id,
                    InvoiceTypeId = item.InvoiceTypeId,
                    IsDue = item.IsDue,
                    Note = item.Note,
                    PersonId = item.PersonId,
                    StudentId = student.Id,
                    InvoiceDescrition = item.InvoiceType.Description
                };

                data.InvoiceList.Add(invoice);
            }

            return data;
        }

        public void CreateInvoice(InvoiceCreation newInvoice)
        {

            var invoiceType = (from i in InvoiceType where i.Id == newInvoice.InvoiceTypeId select i).FirstOrDefault();
            var sgrade = (from sg in StudentGrade where sg.StudentId == newInvoice.StudentId && sg.IsCurrent select sg).SingleOrDefault();
            var student = (from s in Student where s.Id == newInvoice.StudentId select s).SingleOrDefault();

            ChitralekaDB.Invoice inv = new ChitralekaDB.Invoice()
            {
                DueDate = newInvoice.DueDate,
                InvoiceAmount = newInvoice.InvoiceAmount,
                InvoiceDate = DateTime.Now,
                InvoiceNumber = GenerateInvoiceNumber().ToUpper(),
                InvoiceTypeId = newInvoice.InvoiceTypeId,
                IsDue = newInvoice.IsDue,
                Note = newInvoice.Note,
                PersonId = newInvoice.PersonId,
                StudentGrade = sgrade
            };

            Invoice.Add(inv);
            SaveChanges();
        }

        public InvoiceListing GetOpenInvoice(int invoiceTypeId, int personId)
        {
            ChitraLeka.ViewModel.Ledger.InvoiceListing data = new ChitraLeka.ViewModel.Ledger.InvoiceListing();

            data.InvoiceList = new List<ChitraLeka.ViewModel.Ledger.Invoice>();

            var invoices = (from inv in Invoice where inv.PersonId == personId && inv.IsDue && inv.InvoiceTypeId == invoiceTypeId select inv).ToList();

            foreach (var item in invoices)
            {
                var student = (from s in Student where s.Id == item.StudentGrade.Student.Id select s).SingleOrDefault();

                var invoice = new ChitraLeka.ViewModel.Ledger.Invoice()
                {
                    DueDate = item.DueDate,
                    InvoiceDate = item.InvoiceDate,
                    InvoiceAmount = item.InvoiceAmount,
                    InvoiceNumber = string.Format("CDC-{0}", item.InvoiceNumber.ToUpper()),
                    Id = item.Id,
                    InvoiceTypeId = item.InvoiceTypeId,
                    IsDue = item.IsDue,
                    Note = item.Note,
                    PersonId = item.PersonId,
                    StudentId = student.Id,
                    InvoiceDescrition = item.InvoiceType.Description
                };

                data.InvoiceList.Add(invoice);
            }

            return data;
        }

    }

    public class NotificationDal : cLekaDbEntityContainer
    {
        public List<ChitraLeka.ViewModel.Shared.MailoutQueue> GetAllMailouts()
        {
            List<ChitraLeka.ViewModel.Shared.MailoutQueue> q = new List<ChitraLeka.ViewModel.Shared.MailoutQueue>();

            var data = (from x in MailoutQueue select x).ToList();
            foreach (var d in data)
            {
                var mxtype = (from y in MailoutType where y.Id == d.MailoutTypeId select y).FirstOrDefault();
                MxType t = new MxType()
                {
                    Description = mxtype.Description,
                    Id = mxtype.Id,
                    Name = mxtype.Name
                };

                ChitraLeka.ViewModel.Shared.MailoutQueue x = new ChitraLeka.ViewModel.Shared.MailoutQueue()
                {
                    CreatedOn = d.CreatedOn,
                    Email = d.EmailAddress,
                    HTML = d.HtmlBody,
                    Id = d.Id,
                    MailoutTypeId = d.MailoutTypeId,
                    Status = d.Status,
                    UpdatedOn = d.UpdateOn,
                    Person = new ContactDal().searchPersonById(d.PersonId),
                    Type = t
                };

                q.Add(x);
            }

            return q;
        }

        public ChitraLeka.ViewModel.Shared.MailoutQueue PushNotification(ChitraLeka.ViewModel.Contact.Person p, Dictionary<string, string> inputParameters, int mailoutTypeId, string email, string html)
        {
            var requiredParameters = (from x in MailoutTypeParameter where x.MailoutTypeId == mailoutTypeId select x).ToList();

            if (requiredParameters.Count != inputParameters.Count())
            {
                throw new Exception($"Numbers of parameters are not matching with mailouttypeId: {mailoutTypeId}");
            }

            ChitraLeka.ViewModel.Shared.MailoutQueue q = new ChitraLeka.ViewModel.Shared.MailoutQueue()
            {
                Person = p,
                Email = email,
                MailoutTypeId = mailoutTypeId,
                CreatedOn = DateTime.Now,
                Status = 0,
                HTML = html
            };

            var px = (from x in Person where x.Id == p.Id select x).FirstOrDefault();
            var mt = (from x in MailoutType where x.Id == mailoutTypeId select x).FirstOrDefault();

            ChitralekaDB.MailoutQueue mq = new ChitralekaDB.MailoutQueue()
            {
                Person = px,
                CreatedOn = DateTime.Now,
                HtmlBody = html,
                MailoutType = mt,
                EmailAddress = email,
                MailoutTypeId = mailoutTypeId,
                Status = 0,
                PersonId = px.Id
            };

            mq.MailoutQueueParameterValue = ParameterHandling(inputParameters, mailoutTypeId);

            MailoutQueue.Add(mq);
            SaveChanges();

            return q;

        }

        private ICollection<MailoutQueueParameterValue> ParameterHandling(Dictionary<string, string> inputParameters, int mailoutTypeId)
        {
            List<MailoutQueueParameterValue> mqpv = new List<ChitralekaDB.MailoutQueueParameterValue>();
            var requiredParameters = (from x in MailoutTypeParameter where x.MailoutTypeId == mailoutTypeId select x).ToList();

            foreach (var param in inputParameters)
            {
                mqpv.Add(new ChitralekaDB.MailoutQueueParameterValue()
                {
                    MailoutTypeParameter = requiredParameters.Where(x => x.MailoutParameter.Name == param.Key).FirstOrDefault(),
                    Value = param.Value,

                });
            }

            return mqpv;
        }

        public ChitraLeka.ViewModel.Shared.MailoutQueue GetMailoutQueueById(int id)
        {
            var data = (from x in MailoutQueue where x.Id == id select x).FirstOrDefault();
            ChitraLeka.ViewModel.Shared.MailoutQueue q = new ChitraLeka.ViewModel.Shared.MailoutQueue()
            {
                Id = data.Id,
                CreatedOn = data.CreatedOn,
                Email = data.EmailAddress,
                HTML = data.HtmlBody,
                MailoutTypeId = data.MailoutTypeId,
                Status = data.Status,
                UpdatedOn = data.UpdateOn,
                Person = new ContactDal().searchPersonById(data.PersonId),
                Type = new MxType()
                {
                    Description = data.MailoutType.Description,
                    Id = data.MailoutType.Id,
                    Name = data.MailoutType.Name
                }
            };

            return q;
        }

        public void TrackMailout(ChitraLeka.ViewModel.Shared.MailoutQueue q, string ipaddress)
        {

            var data = (from x in MailoutQueue where x.Id == q.Id select x).FirstOrDefault();
            ChitralekaDB.MailoutTracker tracker = new ChitralekaDB.MailoutTracker()
            {
                MailoutQueueId = q.Id,
                IPAddress = ipaddress,
                OpenedOn = DateTime.Now,
                MailoutQueue = data
            };

            MailoutTracker.Add(tracker);
            SaveChanges();
        }
    }
}
