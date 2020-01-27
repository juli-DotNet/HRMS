using HRMS.Core.Common;
using System;

namespace HRMS.Core.Services
{
    public class BaseService
    {
        protected Call HandleCall(Action method)
        {
            try
            {
                method();
                return new Call
                {
                    IsSuccessful = true
                };
            }

            catch (ApplicationException ex)
            {
               // Logger.Error(ex);
                return new Call
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (ArgumentException ex)
            {
               // Logger.Error(ex);
                return new Call
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
               // Logger.Error(ex);
                return new Call
                {
                    IsSuccessful = false,
                    Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
                    Exception = new Exception(ex.Message)
                };
            }
        }
        protected Call<T> HandleCall<T>(Func<T> method)
        {
            try
            {
                return new Call<T>
                {
                    IsSuccessful = true,
                    Result = method()
                };

            }

            catch (ApplicationException ex)
            {
               // Logger.Error(ex);
                return new Call<T>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (ArgumentException ex)
            {
                //Logger.Error(ex);
                return new Call<T>
                {
                    IsSuccessful = false,
                    Message = ex.Message,
                    Exception = ex
                };
            }
            catch (Exception ex)
            {
               // Logger.Error(ex);
                return new Call<T>
                {
                    IsSuccessful = false,
                    Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
                    Exception = new Exception(ex.Message)
                };
            }
        }

        #region Unused
        //protected Call HandleCall(Func<Call> method)
        //{
        //    try
        //    {
        //        return method();
        //    }

        //    catch (ApplicationException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Call
        //        {
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = ex
        //        };
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Call
        //        {
        //            IsSuccessful = false,
        //            Message = ex.Message,
        //            Exception = ex
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        return new Call
        //        {
        //            IsSuccessful = false,
        //            Message =  "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
        //            Exception = new ServiceException(ex.Message, ex.InnerException, ex.StackTrace)
        //        };
        //    }
        //}

        //protected Call<T> HandleCall<T>(Func<T> method)
        //{
        //    try
        //    {
        //        var result = method();
        //        return new Call<T> { Result = result, IsSuccessful = true };
        //    }
        //    catch (ServiceException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Call<T>
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
        //        return new Call<T>
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
        //        return new Call<T>
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
        //        return new Call<T>
        //        {
        //            Result = default(T),
        //            IsSuccessful = false,
        //            Message = "Ndodhi një problem në përpunimin e të dhënave. Ju lutem kontaktoni me administratorin!",
        //            Exception = new ServiceException(ex.Message, ex.InnerException, ex.StackTrace)
        //        };
        //    }
        //}

        //protected Call<T> HandleCall<T>(Func<Call<T>> method)
        //{
        //    try
        //    {
        //        return method();
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        Logger.Error(ex);
        //        return new Call<T>
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
        //        return new Call<T>
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
        //        return new Call<T>
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
