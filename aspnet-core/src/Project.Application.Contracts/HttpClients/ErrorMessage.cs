using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.HttpClients
{
    public class ErrorMessage
    {
        public Error[] Errors { get; set; }
        public ErrorMessage(string code, string des)
        {
            Errors = new Error[]
            {
                new Error(code,des)
            };
        }
        public ErrorMessage(Error[] errors)
        {
            Errors = errors;
        }
        public ErrorMessage(IEnumerable<IdentityError> errors)
        {
            List<Error> errs = new List<Error>();
            foreach (IdentityError error in errors)
            {
                errs.Add(new Error(error.Code, error.Description));
            }
            Errors = errs.ToArray();
        }
    }
    //public ErrorMessage() { }
}
public class Error
{
    public string Code { get; set; }
    public string Description { get; set; }
    public Error(string code, string des)
    {
        Code = code;
        Description = des;
    }
    public Error()
    {

    }
};
