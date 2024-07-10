namespace iTCShop.Constants
{
    public class ResponseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess() => Code == 1;

        public static ResponseModel SuccessResponse() => new ResponseModel { Code = 1, Message = "Success" };
        public static ResponseModel FailureResponse(string mess) => new ResponseModel { Code = 0, Message = mess };
        public static ResponseModel ExceptionResponse() => new ResponseModel { Code = -1, Message = "Exception" };
    
    }
}
