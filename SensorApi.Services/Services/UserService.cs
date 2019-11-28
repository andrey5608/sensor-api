using AutoMapper;
using SensorApi.API.Common.Settings;
using SensorApi.Services.Contracts;
using SensorApi.Services.Model;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace SensorApi.Services
{
    public class UserService : IUserService
    {
        private AppSettings _settings;
        private readonly IMapper _mapper;

        public UserService(IOptions<AppSettings> settings, IMapper mapper)
        {
            _settings = settings?.Value;
            _mapper = mapper;
        }

        public async Task<User> CreateAsync(User user)
        {            
            var path = "textFile1.txt";
            if (File.Exists(path))
            { 
                File.Delete(path);
            }
            using (StreamWriter writetext = new StreamWriter(path))
            {
                writetext.WriteLine("##################################### New user data ############################################");
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(user))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(user);

                    if (value.GetType() != typeof(string)) {
                        foreach (PropertyDescriptor descriptor1 in TypeDescriptor.GetProperties(value))
                        {
                            string name1 = descriptor1.Name;
                            object value1 = descriptor1.GetValue(value);
                            writetext.WriteLine("{0}={1}; ", name1, value1);                        
                        }                            
                    }
                    else
                    {
                      writetext.WriteLine("{0}={1}; ", name, value);
                    }
                    
                } //TODO: save values to db
                writetext.WriteLine("##################################### End user data ############################################");
            }
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
