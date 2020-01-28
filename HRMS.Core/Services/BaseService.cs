using HRMS.Core.Common;
using System;

namespace HRMS.Core.Services
{
    public class BaseService
    {
        protected Response HandleResponse(Action method)
        {
            try
            {
                method();
                return new Response
                {
                    IsSuccessful = true
                };
            }

            catch (ApplicationException ex)
            {
               // Logger.Error(ex);
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (ArgumentException ex)
            {
               // Logger.Error(ex);
                return new Response
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
               // Logger.Error(ex);
                return new Response
                {
                    IsSuccessful = false,
                    Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
                    Exception = new Exception(ex.Message)
                };
            }
        }
        protected Response<T> HandleResponse<T>(Func<T> method)
        {
            try
            {
                return new Response<T>
                {
                    IsSuccessful = true,
                    Result = method()
                };

            }

            catch (ApplicationException ex)
            {
               // Logger.Error(ex);
                return new Response<T>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (ArgumentException ex)
            {
                //Logger.Error(ex);
                return new Response<T>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
               // Logger.Error(ex);
                return new Response<T>
                {
                    IsSuccessful = false,
                    Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
                    Exception = new Exception(ex.Message)
                };
            }
        }


        
        #region Unused
        //protected Response HandleResponse(Func<Response> method)
        //{
        //    try
        //    {
        //        return method();
        //    }

        //    catch (ApplicationException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response
        //        {
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = ex
        //        };
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response
        //        {
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = ex
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response
        //        {
        //            IsSuccessful = false,
        //            Message =  "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
        //            Exception = new ServiceException(ex.Message, ex.InnerException, ex.StackTrace)
        //        };
        //    }
        //}

        //protected Response<T> HandleResponse<T>(Func<T> method)
        //{
        //    try
        //    {
        //        var result = method();
        //        return new Response<T> { Result = result, IsSuccessful = true };
        //    }
        //    catch (ServiceException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = ex
        //        };
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = new ServiceException(ex)
        //        };
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = new ServiceException(ex)
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
        //            Exception = new ServiceException(ex.Message, ex.InnerException, ex.StackTrace)
        //        };
        //    }
        //}

        //protected Response<T> HandleResponse<T>(Func<Response<T>> method)
        //{
        //    try
        //    {
        //        return method();
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = new ServiceException(ex)
        //        };
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = new ServiceException(ex)
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        return new Response<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
        //            Exception = new ServiceException(ex)
        //        };
        //    }
        //}
        #endregion

    }
}
