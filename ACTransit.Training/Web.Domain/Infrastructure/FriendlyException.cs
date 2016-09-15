using System;
using System.Text;
using System.Web.Mvc;

namespace ACTransit.Training.Web.Domain.Infrastructure
{
    public class FriendlyException : Exception
    {
        public FriendlyException(FriendlyExceptionType exceptionType)
            : base(GetMessage(exceptionType))
        {
            ExceptionType = exceptionType;
        }

        public FriendlyException(string message) : base(message)
        {
            ExceptionType = FriendlyExceptionType.Other;
        }

        public FriendlyException(ModelStateDictionary modelState): base(GetMessage(modelState))
        {
            ExceptionType = FriendlyExceptionType.InvalidModelState;
        }
        public FriendlyExceptionType ExceptionType { get; private set; }

        private static string GetMessage(FriendlyExceptionType exceptionType)
        {
            string message;
            switch (exceptionType)
            {
                case FriendlyExceptionType.PsMismatch:
                    message = "PeopleSoft data has changed. please refresh your page and try again.";
                    break;
                case FriendlyExceptionType.AccessDenied:
                    message = "You don't have enough access privileges for this operation.";
                    break;
                case FriendlyExceptionType.ObjectNotFound:
                    message = "The object you are looking for could not be found.";
                    break;
                case FriendlyExceptionType.InUseCanNotDelete:
                    message = "In use, can't be deleted.";
                    break;
                case FriendlyExceptionType.InvalidModelState:
                    message = "Some of the values are not valid.";
                    break;
                case FriendlyExceptionType.NameAlreadyExist:
                    message = "The Name is taken, please choose another name.";
                    break;
                default:
                    message = "Some error occured.";
                    break;
            }
            return message;
        }
        private static string GetMessage(ModelStateDictionary modelState)
        {
            var result=new StringBuilder();
            if (modelState != null)
            {
                foreach (var key in modelState.Keys)
                {
                    var errors = modelState[key].Errors;
                    foreach (var err in errors)
                        result.Append(err.ErrorMessage).AppendLine("<br/>");
                }
            }
            return result.ToString();
        }

    }

    public enum FriendlyExceptionType
    {
        Other,

        /// <summary>
        /// PeopleSoft data mismatch
        /// </summary>
        PsMismatch,
        AccessDenied,
        ObjectNotFound,
        InUseCanNotDelete,        
        InvalidModelState,
        NameAlreadyExist
    }
}